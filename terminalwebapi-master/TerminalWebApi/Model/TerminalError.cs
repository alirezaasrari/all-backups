namespace TerminalWebApi.Model
{
    public class TerminalError
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string WrongPacket { get; set; } = string.Empty;
        public string? Endpoint { get; set; } = string.Empty;
    }
}
