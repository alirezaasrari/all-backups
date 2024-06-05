namespace TerminalWebApi.Model
{
    public class PostDataForTcp
    {
        public string Packet { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
    }
}
