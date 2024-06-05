namespace Terminal.Model
{
    public class Error
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? Date { get; set; }
        public string? WrongPacket { get; set; }
        public string? Endpoint { get; set; }
    }
}
