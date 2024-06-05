using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Sockets;
using System.Text;
using Terminal.Context;
using Terminal.Convertors;
using Terminal.Model;

namespace Terminal.Tools
{
    public static class ResponseMaker
    {
        public static byte[] ResponseGenerator(string message, TcpClient senderClient)
        {
            try
            {
                var services = new ServiceCollection();
                var connectionString = "server=185.7.212.184;port=3306;database=talayieh-test;user=root;password=1372328$oheiL!@#$!;";
                services.AddDbContext<DataContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetService<DataContext>();
                ////////////////////////////////////////////////////////////////DATABASE CONNECTION ESTABLISHED//////////////////////////////
                Log TranslatedMessage = PacketTranslator.Convertors(message);
                byte[] responseBuffer = Array.Empty<byte>();
                string response;
                var serialnumber = message.Substring(8, 8);
                var tag = message.Substring(16, 2);
                var version = message.Substring(18, 2);
                var commandcode = message.Substring(20, 2);
                var code = CompareCrc.YesOrNo(message, TranslatedMessage.Crc) == "yes" ? 30 : 31;
                var commandqueue = db?.CommandQueue.Where(x => x.DcuSerial == serialnumber && x.Done == 0).FirstOrDefault();
                var UID = commandqueue?.UID;
                var id = commandqueue?.Id;
                var day = commandqueue?.Date;
                var SendResponseCondition = ("A5").Equals(message.Substring(20, 2), StringComparison.OrdinalIgnoreCase);
                //////////////////////////////////////////////////////////////NECESSARY VALUES CATCHED///////////////////////////////////////
                if (Equals(TranslatedMessage, "پکت تعریف نشده"))
                {
                    // if the received packet is not defined and out of the category we must answer back it by nothing!!!
                    responseBuffer = Encoding.UTF8.GetBytes("");
                    return responseBuffer;
                }
                else if (code == 31)
                {
                    // if the received packet crc is invalid we send the report back
                    response = $"ABCD000F" +
                    $"{serialnumber}{tag}{version}{code}0000" +
                    $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}310000")}";
                    byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                    responseBuffer = Encoding.ASCII.GetBytes(response);
                    try
                    {
                        senderClient.GetStream().Write(bytes, 0, bytes.Length);
                    }
                    catch (Exception ex)
                    {

                        var error = new SocketErrors()
                        {
                            Message = ex.Message,
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

                    }
                    return responseBuffer;
                }
                else if (SendResponseCondition == false)
                {

                    response = $"ABCD000F" +
                    $"{serialnumber}{tag}{version}{code}0000" +
                    $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{code}0000")}";
                    byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                    responseBuffer = Encoding.ASCII.GetBytes(response);
                    try
                    {
                        senderClient.GetStream().Write(bytes, 0, bytes.Length);
                    }
                    catch (Exception ex)
                    {

                        var error = new SocketErrors()
                        {
                            Message = ex.Message,
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
                    }

                    return responseBuffer;
                }
                else if (commandqueue == null)
                {
                    response = $"ABCD000F" +
                    $"{serialnumber}{tag}{version}{code}0000" +
                    $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{code}0000")}";
                    byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                    responseBuffer = Encoding.ASCII.GetBytes(response);
                    try
                    {
                        senderClient.GetStream().Write(bytes, 0, bytes.Length);
                    }
                    catch (Exception ex)
                    {

                        var error = new SocketErrors()
                        {
                            Message = ex.Message,
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
                    }
                    return responseBuffer;
                }
                else if (SendResponseCondition == true)
                {
                    if (commandqueue.Code == "40")
                    {
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{0015}{serialnumber}{tag}{version}{40}{0006}{newUID}{commandqueue.MeterSerial}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{40}{0006}{newUID}{commandqueue.MeterSerial}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else if (commandqueue.Code == "41")
                    {
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{0016}{serialnumber}{tag}{version}{41}{0007}{newUID}{commandqueue.MeterSerial}{DayCounter.Day(commandqueue.Date)}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{41}{0007}{newUID}{commandqueue.MeterSerial}{DayCounter.Day(commandqueue.Date)}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else if (commandqueue.Code == "42")
                    {
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{0022}{serialnumber}{tag}{version}{42}{0007}{newUID}{commandqueue.MeterSerial}{DayCounter.Day(commandqueue.Date)}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{42}{0007}{newUID}{commandqueue.MeterSerial}{DayCounter.Day(commandqueue.Date)}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else if (commandqueue.Code == "43")
                    {
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{0022}{serialnumber}{tag}{version}{43}{0007}{newUID}{commandqueue.MeterSerial}{DayCounter.Day(commandqueue.Date)}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{43}{0007}{newUID}{commandqueue.MeterSerial}{DayCounter.Day(commandqueue.Date)}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else if (commandqueue.Code == "44")
                    {
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{0021}{serialnumber}{tag}{version}{44}{0006}{newUID}{commandqueue.MeterSerial}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{44}{0006}{newUID}{commandqueue.MeterSerial}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else if (commandqueue.Code == "45")
                    {
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{0011}{serialnumber}{tag}{version}{45}{0004}{newUID}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{45}{0004}{newUID}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else if (commandqueue.Code == "46")
                    {
                        var command = StringToHex.ToHexString($"{commandqueue.setting_type}:{commandqueue.setting_value}");
                        var length = command.Length;
                        var datalength = DecimalToHexCalculator.DecimalToHex(length + 4);
                        var packetlength = DecimalToHexCalculator.DecimalToHex((int)(Convert.ToDecimal(datalength) + 15));
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{packetlength}{serialnumber}{tag}{version}{46}{datalength}{newUID}{commandqueue.MeterSerial}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{46}{datalength}{newUID}{commandqueue.MeterSerial}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else if (commandqueue.Code == "47")
                    {
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{0}{serialnumber}{tag}{version}{47}{0}{newUID}{commandqueue.MeterSerial}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{47}{0}{newUID}{commandqueue.MeterSerial}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else if (commandqueue.Code == "48")
                    {
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{0011}{serialnumber}{tag}{version}{48}{0004}{newUID}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{48}{0004}{newUID}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else if (commandqueue.Code == "49")
                    {
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{0}{serialnumber}{tag}{version}{49}{0}{newUID}{commandqueue.MeterSerial}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{49}{0}{newUID}{commandqueue.MeterSerial}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else { return responseBuffer; }
                }
                else if (Equals(TranslatedMessage.CommandCode, "40") && TranslatedMessage.Data?[..4] == UID
                    || Equals(TranslatedMessage.CommandCode, "41") && TranslatedMessage.Data?[..4] == UID
                    || Equals(TranslatedMessage.CommandCode, "42") && TranslatedMessage.Data?[..4] == UID
                    || Equals(TranslatedMessage.CommandCode, "43") && TranslatedMessage.Data?[..4] == UID
                    || Equals(TranslatedMessage.CommandCode, "44") && TranslatedMessage.Data?[..4] == UID
                    || Equals(TranslatedMessage.CommandCode, "45") && TranslatedMessage.Data?[..4] == UID
                    || Equals(TranslatedMessage.CommandCode, "46") && TranslatedMessage.Data?[..4] == UID
                    || Equals(TranslatedMessage.CommandCode, "47") && TranslatedMessage.Data?[..4] == UID
                    || Equals(TranslatedMessage.CommandCode, "48") && TranslatedMessage.Data?[..4] == UID
                    || Equals(TranslatedMessage.CommandCode, "49") && TranslatedMessage.Data?[..4] == UID)
                {

                    if (commandqueue.Code == "40")
                    {
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{0015}{serialnumber}{tag}{version}{40}{0006}{newUID}{commandqueue.MeterSerial}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{40}{0006}{newUID}{commandqueue.MeterSerial}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else if (commandqueue.Code == "41")
                    {
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{0016}{serialnumber}{tag}{version}{41}{0007}{newUID}{commandqueue.MeterSerial}{DayCounter.Day(commandqueue.Date)}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{41}{0007}{newUID}{commandqueue.MeterSerial}{DayCounter.Day(commandqueue.Date)}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else if (commandqueue.Code == "42")
                    {
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{0022}{serialnumber}{tag}{version}{42}{0007}{newUID}{commandqueue.MeterSerial}{DayCounter.Day(commandqueue.Date)}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{42}{0007}{newUID}{commandqueue.MeterSerial}{DayCounter.Day(commandqueue.Date)}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else if (commandqueue.Code == "43")
                    {
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{0022}{serialnumber}{tag}{version}{43}{0007}{newUID}{commandqueue.MeterSerial}{DayCounter.Day(commandqueue.Date)}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{43}{0007}{newUID}{commandqueue.MeterSerial}{DayCounter.Day(commandqueue.Date)}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else if (commandqueue.Code == "44")
                    {
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{0021}{serialnumber}{tag}{version}{44}{0006}{newUID}{commandqueue.MeterSerial}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{44}{0006}{newUID}{commandqueue.MeterSerial}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else if (commandqueue.Code == "45")
                    {
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{0011}{serialnumber}{tag}{version}{45}{0004}{newUID}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{45}{0004}{newUID}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else if (commandqueue.Code == "46")
                    {
                        var command = StringToHex.ToHexString($"{commandqueue.setting_type}:{commandqueue.setting_value}");
                        var length = command.Length;
                        var datalength = DecimalToHexCalculator.DecimalToHex(length + 4);
                        var packetlength = DecimalToHexCalculator.DecimalToHex((int)(Convert.ToDecimal(datalength) + 15));
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{packetlength}{serialnumber}{tag}{version}{46}{datalength}{newUID}{commandqueue.MeterSerial}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{46}{datalength}{newUID}{commandqueue.MeterSerial}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else if (commandqueue.Code == "47")
                    {
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{0}{serialnumber}{tag}{version}{47}{0}{newUID}{commandqueue.MeterSerial}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{47}{0}{newUID}{commandqueue.MeterSerial}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else if (commandqueue.Code == "48")
                    {
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{0011}{serialnumber}{tag}{version}{48}{0004}{newUID}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{48}{0004}{newUID}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else if (commandqueue.Code == "49")
                    {
                        var newUID = UidGenerator.RandomUidGenerator(4);
                        response = $"ABCD" + $"{0}{serialnumber}{tag}{version}{49}{0}{newUID}{commandqueue.MeterSerial}" +
                        $"{CrcCalculator.CalculateChecksum($"{serialnumber}{tag}{version}{49}{0}{newUID}{commandqueue.MeterSerial}")}";
                        byte[] bytes = StringToByteArrayy.StringToByteArray(response);
                        responseBuffer = Encoding.ASCII.GetBytes(response);
                        try
                        {
                            senderClient.GetStream().Write(bytes, 0, bytes.Length);
                            var selected = db?.CommandQueue.Where(x => x.Id == id).FirstOrDefault();
                            db?.CommandQueue.Remove(selected);
                            selected.Done = 1;
                            selected.UID = UID;
                            selected.Completed = 0;
                            db?.CommandQueue.Add(selected);
                        }
                        catch (Exception ex)
                        {

                            var error = new SocketErrors()
                            {
                                Message = ex.Message,
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
                        }
                        return responseBuffer;
                    }
                    else { return responseBuffer; }
                }
                else
                {
                    // otherwise we will answer the received packet here by nothing!!!
                    responseBuffer = Encoding.ASCII.GetBytes("");
                    senderClient.GetStream().Write(responseBuffer, 0, responseBuffer.Length);
                    return responseBuffer;
                }
                ///////////////////////////////////////////////////////////////////ALL CONDITIONS CHECKED////////////////////////////////////
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return Array.Empty<byte>();
            }
        }
    }
}
