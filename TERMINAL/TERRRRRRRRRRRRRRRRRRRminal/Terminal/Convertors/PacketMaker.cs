namespace Terminal.Convertors
{
    public static class PacketMaker
    {
        /* here we make our packet   
           our packets are in the form of:
           ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
           start bytes|packet length|datalogger serial number| packet tag             |command version|command code|data length      |data            |crc-16

             2 bytes  |   2 bytes   |         4 bytes        | 1 bytes                | 1 bytes       |    1 bytes |   2 bytes       |      n         |  2 bytes

           ( ABCD )   |( 15 + n )   |acording to the machine |( 0-255 outo increment )|(01-02-03-04)  |    30-A8   |based on commands|based on command|calculatable
           ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
      here we define our data field in our packets that must be send from the terminal back to the datalogger in the response
        current usage:  usage of the current day
       packet sent data field :     unique id | meter serial number 
                                      2 bytes |   4 bytes 
       ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
       hourly usage:  usage of the current day in houres
       packet sent data field :     unique id | meter serial number| day of recent 62 from 0 to 62
                                      2 bytes |   4 bytes          |  1 byte
       ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        daily usage:  usage of an especific day
       packet sent data field :     unique id | meter serial number| day of recent 62
                                      2 bytes |   4 bytes          |  1 byte
       ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        monthly usage:  usage of an especific day
       packet sent data field :     unique id | meter serial number| month number 0 to 11
                                      2 bytes |   4 bytes          |  1 byte
       ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        total usage:  total usage
       packet sent data field:      unique id | meter serial number
                                      2 bytes | 4 bytes            
       ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        */

        //public static string GetRandomHexUniqueId()
        //{
        //    Random random = new();
        //    byte[] bytes = new byte[2];
        //    random.NextBytes(bytes);

        //    return BitConverter.ToString(bytes).Replace("-", string.Empty);
        //}
        //public static string PacketTagProvider()
        //{
        //    Random random = new();
        //    byte randomByte = (byte)random.Next(0, 256);

        //    return randomByte.ToString("X");
        //}
        //public static string ResponsePacketMaker(int responsecode)
        //{
        //    return $"to be completed{responsecode}";
        //}
        //public static string CommandedPacketMaker(int requestcode, string meterserialnumber, string dataloggerserialnumber)
        //{
        //    /* 
        //     all of the usage data are based on the 0.1 liter but in some cases:
        //     such as switch and pulse based meters are in the following form:

        //     meter size < 1 inch ==> 0.1 liter
        //     1 =< meter size <= 2 inch ==> 10 liter
        //     meter size >= 3 inch  ==> 100 liter  
        //    */
        //    if (requestcode == 40)
        //    {
        //        // this is the packet that is sent from terminal to obtain datalogger current usage data 
        //        // this packet contain data field by 12 hex charactor or 6 bytes
        //        return $"start byte is ABCD\n" +
        //               $"packet length is :{0015}\n" +
        //               $"datalogger serial is :{dataloggerserialnumber}\n" +
        //               $"packet tag is :{PacketTagProvider} \n" +
        //               $"command version is :{01} \n" +
        //               $"command code is :{40} \n" +
        //               $"data length is :000c \n" +
        //               $"data is :{GetRandomHexUniqueId}{meterserialnumber}\n" +
        //               $"crc is :{CrcCalculator.CalculateChecksum(dataloggerserialnumber + PacketTagProvider() + 01 + 40
        //               + "000c" + GetRandomHexUniqueId() + meterserialnumber)}";
        //    }
        //    else if(requestcode == 41)
        //    {
        //        // this is the packet that is sent from the terminal to obtain datalogger hourly usage data
        //        // this packet contain data field by 14 hex charactor or 7 bytes
        //        return $"start byte is ABCD\n" +
        //               $"packet length is :{0016}\n" +
        //               $"datalogger serial is :{dataloggerserialnumber}\n" +
        //               $"packet tag is :{PacketTagProvider} \n" +
        //               $"command version is :{01} \n" +
        //               $"command code is :{41} \n" +
        //               $"data length is :000e \n" +
        //               $"data is :{GetRandomHexUniqueId}{meterserialnumber}\n" +
        //               $"crc is :{CrcCalculator.CalculateChecksum(dataloggerserialnumber + PacketTagProvider() + 01 + 40
        //               + "000c" + GetRandomHexUniqueId() + meterserialnumber)}";
        //    }
        //    else if (requestcode == 42)
        //    {
        //        // this is the packet that is sent from the terminal to obtain datalogger daily usage data
        //        // this packet contian of data field by 14 hex charactor or 7 bytes 
        //        return $"start byte is ABCD\n" +
        //               $"packet length is :{4}\n" +
        //               $"datalogger serial is :{dataloggerserialnumber}\n" +
        //               $"packet tag is :{PacketTagProvider} \n" +
        //               $"command version is :{01} \n" +
        //               $"command code is :{42} \n" +
        //               $"data length is :000e \n" +
        //               $"data is :{GetRandomHexUniqueId}{meterserialnumber}\n" +
        //               $"crc is :{CrcCalculator.CalculateChecksum(dataloggerserialnumber + PacketTagProvider() + 01 + 40
        //               + "000c" + GetRandomHexUniqueId() + meterserialnumber)}";
        //    }
        //    else if (requestcode == 43)
        //    {
        //        // this is the packet that is sent from the terminal to obtain datalogger monthly usage data
        //        // this packet contain of data field by 14 charactor or 7 bytes
        //        return $"start byte is ABCD\n" +
        //               $"packet length is :{4}\n" +
        //               $"datalogger serial is :{4}\n" +
        //               $"packet tag is :{PacketTagProvider} \n" +
        //               $"command version is :{01} \n" +
        //               $"command code is :{43} \n" +
        //               $"data length is :000e \n" +
        //               $"data is :{GetRandomHexUniqueId}{meterserialnumber}\n" +
        //               $"crc is :{CrcCalculator.CalculateChecksum(dataloggerserialnumber + PacketTagProvider() + 01 + 40
        //               + "000c" + GetRandomHexUniqueId() + meterserialnumber)}";
        //    }
        //    else if (requestcode == 44)
        //    {
        //        // this is the packet that is sent from the terminal to obtain datalogger total usage data
        //        // this packet contain of data field by 12 charactor or 6 bytes 
        //        return $"start byte is ABCD\n" +
        //               $"packet length is :{4}\n" +
        //               $"datalogger serial is :{4}\n" +
        //               $"packet tag is :{PacketTagProvider} \n" +
        //               $"command version is :{01} \n" +
        //               $"command code is :{44} \n" +
        //               $"data length is :000e \n" +
        //               $"data is :{GetRandomHexUniqueId}{meterserialnumber}\n" +
        //               $"crc is :{CrcCalculator.CalculateChecksum(dataloggerserialnumber + PacketTagProvider() + 01 + 40
        //               + "000c" + GetRandomHexUniqueId() + meterserialnumber)}";
        //    }
        //    else
        //    {
        //        return "command compeletly out of definition";
        //    }
        //}
    }
}
