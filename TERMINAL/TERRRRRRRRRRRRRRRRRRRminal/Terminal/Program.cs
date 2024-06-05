using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using Terminal;
using Terminal.Context;
using Terminal.Convertors;
using Terminal.Dictionaries;
using Terminal.Model;
using Terminal.Tools;
using static System.Runtime.InteropServices.JavaScript.JSType;

class ChatServer
{
    // we defien our properties for the chatserve class 
    // a listener for receiving stream from the tcp clients
    private readonly TcpListener listener;

    // a dictionary for saving the received clients endpoints
    private readonly Dictionary<string, TcpClient> clientMap = new();
    public ChatServer(string host, int port)
    {
            IPAddress[] ipAddresses = Dns.GetHostAddresses(host);
            if (ipAddresses.Length == 0)
            {
                Console.WriteLine($"Failed to resolve IP address for {host}");
                return;
            }

            //// Use the first resolved IP address
            IPAddress ipAddress = ipAddresses[0];

            // Create a TcpListener that listens for incoming connections on the specified IP address and port
            listener = new TcpListener(ipAddress, port);
            // the listener start working
            listener.Start();

            // a prompt on the console show which ip and port is being used for listenning
            Console.WriteLine("Chat server started on " + host + ":" + port);
        //listener = new TcpListener(IPAddress.Parse(host), port);    
    }
    public void Run()
    {
        try
        {
            while (true)
            {
                try
                {
                    // our tcp listener starts accepting the client
                    TcpClient client = listener.AcceptTcpClient();

                    // the client stream is saving as the strem as networkstream object
                    NetworkStream stream = client.GetStream();

                    ThreadPool.QueueUserWorkItem(state => HandleClient(client, clientMap));
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }               
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }      
    }
    private static void HandleClient(TcpClient client, Dictionary<string, TcpClient> clientMap)
    {
        try
        {
            // now we save the accepted client ip and port
            var track = client.Client.RemoteEndPoint;

            // in these series of code we create a communication with the database and its provider to use in future
            var services = new ServiceCollection();

            // this is the connection string for communication with the database
            var connectionString = "server=185.7.212.184;port=3306;database=talayieh-test;user=root;password=1372328$oheiL!@#$!;";

            // in this part we prepare for using entity framework core context as a connectin bridge with the table and models
            services.AddDbContext<DataContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetService<DataContext>();

            while (true)
            {
                //here we start getting the stream from our client that we have accepted that
                NetworkStream stream = client.GetStream();
                try
                {
                    // here we define a buffer for saving the received packet in that
                    byte[] buffer = new byte[4096];

                    // here we charge our buffer with the stream received to us and fill that array of bytes
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    // if the connection with the client end we break the connection
                    if (bytesRead == 0)
                    {
                        client.Close();
                        break;
                    }

                    string? senderIpAddressAndPort = client?.Client?.RemoteEndPoint?.ToString();
                    // we add the client that is sending us the packet to the list
                    if (senderIpAddressAndPort != null && !clientMap.ContainsKey(senderIpAddressAndPort))
                    {
                        clientMap.Add(senderIpAddressAndPort, client);
                    }
                    // here we get the string equivalence of the received buffer from the client
                    //string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    //string message = BitConverter.ToString(buffer).Replace("-", "");
                    string message = BitConverter.ToString(buffer, 0, bytesRead).Replace("-", "");
                    // in this section we translate the received packet and split it to its parts
                    var TranslatedMessage = PacketTranslator.Convertors(message);

                    // if we have such a client we define a tcp client for that to use for answering
                    TcpClient senderClient = clientMap[senderIpAddressAndPort];

                    byte[] responseBuffer = ResponseMaker.ResponseGenerator(message, senderClient);
                    // just for a visulaized result on the console we use this log         
                    Console.WriteLine("Client message: " + message + "and the packet translator will do the rest for you ...");

                    try
                    {
                        string DataLoggerSerialInDecimalForCheck = HexToDecimalConvertor.HexToDecimal(TranslatedMessage.DataLoggerSerialNumber).ToString();
                        var isdataloggervalid = SerialNumberCheck.SerialCheck(DataLoggerSerialInDecimalForCheck);
                        if (isdataloggervalid == 1)
                        {
                            var Log = new Log()
                            {
                                StartByte = TranslatedMessage.StartByte,
                                PacketTag = TranslatedMessage.PacketTag,
                                DataLoggerSerialNumber = TranslatedMessage.DataLoggerSerialNumber,
                                DataLength = TranslatedMessage.DataLength,
                                Data = TranslatedMessage.Data,
                                Crc = TranslatedMessage.Crc,
                                CommandVersion = TranslatedMessage.CommandVersion,
                                CommandCode = TranslatedMessage.CommandCode,
                                PacketLength = TranslatedMessage.PacketLength,
                                Date = TimeCalculator.ConvertMiladiToshamsi(),
                                CommandCodeMeaning = CommandManager.GetCommandDescription(TranslatedMessage.CommandCode),
                                CommandVersionMeaning = CommandManager.GetCommandDescription(TranslatedMessage.CommandVersion),
                                DataLoggerSerialInDecimal = HexToDecimalConvertor.HexToDecimal(TranslatedMessage.DataLoggerSerialNumber),
                                DataContent = PacketDataAnalizer.DataAnalizer(TranslatedMessage.Data, TranslatedMessage.CommandCode, DataLoggerSerialInDecimalForCheck),
                                IpAndPort = track != null ? track.ToString() : "بدون ip and port!!!",
                                IsCrcValid = CompareCrc.YesOrNo(message, TranslatedMessage.Crc),
                                Response = responseBuffer,
                                IsDataLoggerValid = isdataloggervalid,
                            };


                            if (db != null)
                            {
                                db.NewTerminal.Add(Log);
                                db.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine("db is null");
                            }
                        }
                        else
                        {
                            var error = new Terminal.Model.Error()
                            {
                                Message = "دیتالاگر نامعتبر",
                                Date = TimeCalculator.ConvertMiladiToshamsi(),
                                WrongPacket = message,
                                Endpoint = track != null ? track.ToString() : "بدون ip and port!!!",
                            };

                            if (db != null)
                            {
                                db.NewTerminalError.Add(error);
                                db.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine("db is null");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // certainly execptions will be moderated here
                        var ineerexception = ex.InnerException;
                        if (ineerexception != null)
                        {
                            var error = new Terminal.Model.Error()
                            {
                                Message = ex.Message,
                                Date = TimeCalculator.ConvertMiladiToshamsi(),
                                WrongPacket = message,
                                Endpoint = track != null ? track.ToString() : "بدون ip and port!!!",
                            };

                            if (db != null)
                            {
                                db.NewTerminalError.Add(error);
                                db.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine("db is null");
                            }
                        }
                        else
                        {
                            var error = new Terminal.Model.Error()
                            {
                                Message = "پکت نامعتبر",
                                Date = TimeCalculator.ConvertMiladiToshamsi(),
                                WrongPacket = message,
                                Endpoint = senderIpAddressAndPort
                            };

                            if (db != null)
                            {
                                db.NewTerminalError.Add(error);
                                db.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine("db is null");
                            }
                        };
                    }
                }
                catch (SocketException ex)
                {
                    var ineerexception = ex.InnerException;
                    if (ineerexception != null)
                    {
                        var error = new SocketErrors()
                        {
                            Message = ineerexception.ToString()[..255],
                            Date = TimeCalculator.ConvertMiladiToshamsi(),
                        };

                        if (db != null)
                        {
                            db.NewTerminalSocketError.Add(error);
                            db.SaveChanges();
                            client.Close();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("db is null");
                        }
                    }
                    else
                    {
                        var error = new SocketErrors()
                        {
                            Message = "an unhandelled exception",
                            Date = TimeCalculator.ConvertMiladiToshamsi(),
                        };

                        if (db != null)
                        {
                            db.NewTerminalSocketError.Add(error);
                            db.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine("db is null");
                        }
                    };
                }
                catch (IOException ex)
                {
                    var ineerexception = ex.InnerException;
                    if (ineerexception != null)
                    {
                        var error = new IoError()
                        {
                            Message = ineerexception.ToString(),
                            Date = TimeCalculator.ConvertMiladiToshamsi(),
                        };

                        if (db != null)
                        {
                            db.NewTerminalIoError.Add(error);
                            db.SaveChanges();
                            client.Close();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("db is null");
                        }
                    }
                    else
                    {
                        var error = new IoError()
                        {
                            Message = "پکت نامعتبر",
                            Date = TimeCalculator.ConvertMiladiToshamsi(),
                        };
                        if (db != null)
                        {
                            db.NewTerminalIoError.Add(error);
                            db.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine("db is null");
                        }

                    };
                }
                catch (Exception ex)
                {
                    // certainly execptions will be moderated here
                    var ineerexception = ex.InnerException;
                    if (ineerexception != null)
                    {
                        var error = new Terminal.Model.Error()
                        {
                            Message = ex.Message,
                            Date = TimeCalculator.ConvertMiladiToshamsi(),
                            Endpoint = track != null ? track.ToString() : "بدون ip and port!!!",
                        };
                        if (db != null)
                        {
                            db.NewTerminalError.Add(error);
                            db.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine("db is null");
                        }

                    }
                    else
                    {
                        var error = new Terminal.Model.Error()
                        {
                            Message = "پکت نامعتبر",
                            Date = TimeCalculator.ConvertMiladiToshamsi(),
                        };
                        if (db != null)
                        {
                            db.NewTerminalError.Add(error);
                            db.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine("db is null");
                        }
                    };
                }
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
        
    }
    public static void Main(string[] args)
    {
        try
        {
            //string host = "172.16.1.38";
            //string host = "195.88.208.36";
            //string host = "127.0.0.1";
            //int port = 8082;
            string host = "terminal-sahmab.ir";
            int port = 8000;

            if (args.Length > 0)
            {
                host = args[0];
            }

            if (args.Length > 1)
            {
                port = int.Parse(args[1]);
            }

            ChatServer server = new(host, port);
            server.Run();
            // Sends a FIN segment to the
            // other end of the TCP connection, indicating that the
            // sender has no more data to send.
            // The stream remains open for receiving data. 
            // we use:
            // stream.Shutdown(SocketShutdown.Send)

            //Sends a FIN segment to the other end of the
            //TCP connection, indicating that the sender has no
            //more data to send. The stream is then closed and
            //all resources associated with the stream are released.
            // we use: 
            // stream.Close()
            /*
             * this code can be added to the client constructor to automatically
             client.NoDelay = true;
            client.ReceiveTimeout = 3000;  3 seconds in milliseconds 
             */
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
        
    }
}
