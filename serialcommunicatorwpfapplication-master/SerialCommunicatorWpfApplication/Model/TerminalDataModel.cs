using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunicatorWpfApplication.Model
{
    public class TerminalDataModel
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
        public long DataLoggerSerialInDecimal { get; set; }
        public string? CommandCodeMeaning { get; set; }
        public string? CommandVersionMeaning { get; set; }
        public string? DataContent { get; set; }
        public string? IpAndPort { get; set; }
        public string? IsCrcValid { get; set; }
        public string? Response { get; set; }
        public int? IsDataLoggerValid { get; set; }
        public int? NumberOfMeters { get; set; }
        public string? ListOfMeters { get; set; }
    }
}
