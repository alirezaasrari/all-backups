namespace Terminal.Convertors
{
    public static class CompareCrc
    {
        public static string YesOrNo(string packet, string crc)
        {
            if (packet.Length == 30 || packet.Length == 146 || packet.Length == 74 || packet.Length == 58 || packet.Length == 48
                || packet.Length == 64 || packet.Length == 96 || packet.Length == 128 || packet.Length == 160
                || packet.Length == 192 || packet.Length == 224 || packet.Length == 256 || packet.Length == 288
                || packet.Length == 144 || packet.Length == 256 || packet.Length == 368
                || packet.Length == 480 || packet.Length == 592 || packet.Length == 704 || packet.Length == 816
                || packet.Length == 928 || packet.Length == 66 || packet.Length == 98
                || packet.Length == 130 || packet.Length == 162 || packet.Length == 194
                || packet.Length == 226 || packet.Length == 258 || packet.Length == 290 || packet.Length == 320
                || packet.Length == 352 || packet.Length == 384 || packet.Length == 416 || packet.Length == 448 
                || packet.Length == 480 || packet.Length == 512 || packet.Length == 544 || packet.Length == 576
                || packet.Length == 608 || packet.Length == 640 || packet.Length == 672 || packet.Length == 1040
                || packet.Length == 1152 || packet.Length == 1264 || packet.Length == 1376 || packet.Length == 1488
                || packet.Length == 1600 || packet.Length == 1712 || packet.Length == 1824 || packet.Length == 1936
                || packet.Length == 2048 || packet.Length == 2160 || packet.Length == 2272
                || packet.Length == 322 || packet.Length == 354 || packet.Length == 386 || packet.Length == 418
                || packet.Length == 450 || packet.Length == 482 || packet.Length == 514 || packet.Length == 546
                || packet.Length == 578 || packet.Length == 610 || packet.Length == 642 || packet.Length == 674
                )
            {
                if (packet.Length == 30)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 18));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 146)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 134));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 74)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 62));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 58)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 46));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 48)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 36));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 64)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 52));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 96)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 84));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 128)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 116));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 160)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 148));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 192)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 180));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 224)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 212));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 256)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 244));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 288)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 276));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 144)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 132));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 256)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 244));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 368)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 356));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 480)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 468));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 592)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 580));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 704)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 692));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 816)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 804));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 928)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 916));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 66)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 54));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 98)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 86));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 130)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 118));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 162)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 150));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 194)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 182));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 226)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 214));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 258)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 246));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 290)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 278));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 320)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 308));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 352)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 340));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 384)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 372));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 416)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 404));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 448)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 436));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 480)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 468));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 512)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 500));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 544)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 532));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 576)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 564));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 608)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 596));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 640)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 628));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 672)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 660));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 1040)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 1028));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 1152)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 1140));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 1264)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 1252));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 1376)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 1364));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 1488)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 1476));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 1600)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 1588));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 1712)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 1700));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 1824)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 1812));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 1936)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 1924));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 2048)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 2036));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 2160)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 2148));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 2272)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 2260));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 322)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 310));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 354)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 342));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 386)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 374));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 418)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 406));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 450)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 438));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 482)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 470));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 514)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 502));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 546)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 534));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 578)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 566));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 610)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 598));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 642)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 630));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else if (packet.Length == 674)
                {
                    var CalculatedCrc = CrcCalculator.CalculateChecksum(packet.Substring(8, 662));
                    return CalculatedCrc.Equals(crc, StringComparison.OrdinalIgnoreCase) ? "yes" : "no";
                }
                else
                {
                    return "تعریف نشده";                
                }
            }
            else
            {
                return "no";
            }
        }
    }
}
