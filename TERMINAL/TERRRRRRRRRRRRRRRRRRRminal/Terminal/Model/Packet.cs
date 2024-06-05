using Terminal.Convertors;

namespace Terminal.Model
{
    public class Packet
    {
        public string StartByte { get; set; } = string.Empty;
        public string PacketLength { get; set; } = string.Empty;
        public string DataLoggerSerialNumber { get; set; } = string.Empty;
        public string PacketTag { get; set; } = string.Empty;
        public string CommandVersion { get; set; } = string.Empty;
        public string CommandCode { get; set; } = string.Empty;
        public string DataLength { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
        public string Crc { get; set; } = string.Empty;
    }
}
