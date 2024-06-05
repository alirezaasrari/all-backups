using System;
using System.Net.Sockets;
using System.Text;
using TerminalWebApi.Model;

namespace TerminalWebApi.Service
{
    public class TcpClientService
    {
        public async Task SendMessageToServer(PostDataForTcp data)
        {
            try
            {
                var port = data.Port;
                var host = data.Host;
                var message = data.Packet;
                using TcpClient client = new(host, port);
                using NetworkStream stream = client.GetStream();
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(bytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending message:", ex);
            }
        }
    }
}
