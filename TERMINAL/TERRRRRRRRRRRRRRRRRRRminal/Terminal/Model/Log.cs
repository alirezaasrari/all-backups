namespace Terminal.Model
{
    public class Log
    {
        public int Id { get; set; }
        public string? StartByte { get; set; }
        public string? PacketLength { get; set; }
        public string? DataLoggerSerialNumber { get; set; }
        public string? PacketTag { get; set; }
        public string? CommandVersion { get; set; }
        public string? CommandCode { get; set; }
        public string? DataLength { get; set; }
        public string? Data { get; set; }
        public string? Crc { get; set; }
        public string? Date { get; set; }
        public long? DataLoggerSerialInDecimal { get; set; }
        public string? CommandCodeMeaning { get; set; }
        public string? CommandVersionMeaning { get; set; }
        public string? DataContent { get; set; }
        public string? IpAndPort { get; set; }
        public string? IsCrcValid { get; set; }
        public byte[]? Response { get; set; }
        public int? IsDataLoggerValid { get; set; }
    }
}
