using System.Net.Sockets;
using System.Text;
class ChatClient
{
    private readonly TcpClient client;
    public ChatClient(string host, int port)
    {
        client = new TcpClient(host, port);
    }
    public void Run()
    {
        NetworkStream stream = client.GetStream();
        Thread receiveThread = new(() => ReceiveMessages(stream));
        receiveThread.Start();
        while (true)
        {
            string? message = Console.ReadLine();

            if (message != null)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);
            }
        }
    }
    private static void ReceiveMessages(NetworkStream stream2)
    {
        while (true)
        {
            byte[] buffer = new byte[1024];
            int bytesRead = stream2.Read(buffer, 0, buffer.Length);
            if (bytesRead == 0)
            {
                Console.WriteLine("Server disconnected");
                break;
            }
            string serverMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Server message: " + serverMessage);
        }
    }
    public static void Main(string[] args)
    {
        //string host = "195.88.208.36";
        string host = "127.0.0.1";
        //int port = 80;
        int port = 8000;
        if (args.Length > 0)
        {
            host = args[0];
        }
        if (args.Length > 1)
        {
            port = int.Parse(args[1]);
        }
        ChatClient client = new(host, port);
        client.Run();
    }
}
