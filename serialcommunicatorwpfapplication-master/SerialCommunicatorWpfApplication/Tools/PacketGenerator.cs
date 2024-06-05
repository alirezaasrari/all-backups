namespace SerialCommunicatorWpfApplication.Tools
{
    public static class PacketGenerator
    {
        public static string connectionsess;
        public static string PacketLengthCalculator(string data)
        {
            if(data == "")
            {
                return "000b";
            }
            else
            {
                var len = data.Length;
                var datalen = len / 2;
                var hexlen = DecimalToHex.DecimalToHexx(datalen + 11);
                return hexlen;
            }
        }
        public static string DataLengthCalculator(string data)
        {
            if (data == "")
            {
                return "0000";
            }
            else
            {
                var len = data.Length;
                var datalen = len / 2;
                var hexlen = DecimalToHex.DecimalToHexx(datalen);
                return hexlen;
            }
        }
        public static string GeneratePacket(string cmd)
        {
            var cmdcode = CmdToCommandCode.CmdToCmdCode(cmd);
            var data = DataGenerators.DataGenerator(cmdcode, null,null,null,null,null,null,null,null,null,null,null,null);
            var packetlength = PacketLengthCalculator(data);
            var connectionsession = ConnectionSessionGenerator.RandomHexGenerator();
            connectionsess = connectionsession.ToString();
            string datalength;
            if (data == "")
            {
                datalength = "0000";
            }
            else
            {
                datalength = DataLengthCalculator(data);
            }
            string crcCase;
            if(data == "")
            {
                 crcCase = $"{connectionsession}{cmdcode}{datalength}";
            }
            else
            {
                 crcCase = $"{connectionsession}{cmdcode}{datalength}{data}";
            }
            
            var crc = CrcCalculator.CalculateChecksum(crcCase);
            var packet = $"8575{packetlength}{connectionsession}{cmdcode}{datalength}{data}{crc}";
            return packet;
        }
        public static string GeneratePacket2(string cmd, string? codesetting1, string? codesetting2
            , string? codesetting3, string? codesetting4, string? codesetting5, string? codesetting6
            , string? codesetting7, string? codesetting8, string? codesetting9, string? codesetting10
            , string? codesetting11, string? codesetting12)
        {
            var cmdcode = CmdToCommandCode.CmdToCmdCode(cmd);
            var data = DataGenerators.DataGenerator(cmdcode, codesetting1, codesetting2, codesetting3, codesetting4, codesetting5,
                codesetting6, codesetting7, codesetting8, codesetting9, codesetting10, codesetting11, codesetting12);
            var packetlength = PacketLengthCalculator(data);
            var connectionsession = connectionsess;
            string datalength;
            if (data == "")
            {
                datalength = "0000";
            }
            else
            {
                datalength = DataLengthCalculator(data);
            }
            string crcCase;
            if (data == "")
            {
                crcCase = $"{connectionsession}{cmdcode}{datalength}";
            }
            else
            {
                crcCase = $"{connectionsession}{cmdcode}{datalength}{data}";
            }

            var crc = CrcCalculator.CalculateChecksum(crcCase);
            var packet = $"8575{packetlength}{connectionsession}{cmdcode}{datalength}{data}{crc}";
            return packet;
        }
    }
}
