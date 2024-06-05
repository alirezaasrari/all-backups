using Terminal.Model;

namespace Terminal.Convertors
{
    public static class PacketTranslator
    {
        /* 
        here we make our packet   
        our packets are in the form of:

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        start bytes|packet length|datalogger serial number| packet tag             |command version|command code|data length      |data            |crc-16

          2 bytes  |   2 bytes   |         4 bytes        | 1 bytes                | 1 bytes       |    1 bytes |   2 bytes       |      n         |  2 bytes

        ( ABCD )   |( 15 + n )   |acording to the machine |( 0-255 outo increment )|(01-02-03-04)  |    30-A8   |based on commands|based on command|calculatable

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        current usage:  usage of the current day
        packet sent data field :     unique id | meter serial number 
                                       2 bytes |   4 bytes 

        packet received data field: unique id | serial number meter | daily usage | timeunix
                                     2 bytes  |  4 bytes            | 4 bytes     | 4 bytes

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        hourly usage:  usage of the current day in houres
        packet sent data field :     unique id | meter serial number| day of recent 62
                                       2 bytes |   4 bytes          |  1 byte
 
        packet received data field:  unique id | meter serial number| end of the day timeunix | hour 0 | ... | hour 23
                                       2 bytes |   4 bytes          |  4 bytes                | 2 bytes| ... | 2 bytes

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        daily usage:  usage of an especific day
        packet sent data field :     unique id | meter serial number| day of recent 62
                                       2 bytes |   4 bytes          |  1 byte

        packet received data field:  unique id | meter serial number| day usage x | timeunix x
                                       2 bytes | 4 bytes            | 4 bytes     | 4 bytes

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        monthly usage:  usage of an especific day
        packet sent data field :     unique id | meter serial number| day of recent 62
                                       2 bytes |   4 bytes          |  1 byte

        monthly usage:  total usage
        packet received data field:  unique id | meter serial number| day usage x | total usage  |  monthly usage |  timeunix 
                                       2 bytes | 4 bytes            | 4 bytes     | 4 bytes      |  4 bytes       |   4 bytes

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
         */
        public static dynamic Convertors(string packet)
        {
            if (packet.Length < 4)
            {
                return "پکت تعریف نشده";
            }
            else
            {
                if (packet[..4].Equals("ABCD", StringComparison.OrdinalIgnoreCase))
                {
                    if (packet.Length == 30)
                    {
                        // this packets contain no data information
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = "0000",
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 146)
                    {
                        // this packets contain hourly usage information
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(27, 116),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 74)
                    {
                        // this packets contain total usage information
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 44),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 58)
                    {
                        // this packets contain monthly usage information
                        // this packets contain current day usage data 
                        // this packets contain daily usage information
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(27, 28),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 48)
                    {
                        // this packets contain alarm information
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 18),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 64)
                    {
                        // this packets contain midnight data for one meter information
                        // also data for month data with one meter
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 34),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 96)
                    {
                        // this packets contain midnight data for two meter information
                        // also data for month data with two meter
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 66),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 128)
                    {
                        // this packets contain midnight data for three meter information
                        // also data for month data with three meter
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 98),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 160)
                    {
                        // this packets contain midnight data for four meter information
                        // also data for month data with four meter
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 130),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 192)
                    {
                        // this packets contain midnight data for five meter information
                        // also data for month data with five meter
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 162),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 224)
                    {
                        // this packets contain midnight data for six meter information
                        // also data for month data with six meter
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 194),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 288)
                    {
                        // this packets contain midnight data for eight meter information
                        // also data for month data with eight meter
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 258),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 144)
                    {
                        // this packets contain hourly data for one meter information
                        // also data for hourly queue data with one meter
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 114),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 256)
                    {
                        // this packets contain hourly data for two meter information
                        // also data for hourly queue data with two meter
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 226),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 368)
                    {
                        // this packets contain hourly data for three meter information
                        // also data for hourly queue data with three meter
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 338),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 480)
                    {
                        // this packets contain hourly data for four meter information
                        // also data for hourly queue data with four meter
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 450),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 592)
                    {
                        // this packets contain hourly data for five meter information
                        // also data for hourly queue data with five meter
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 562),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 704)
                    {
                        // this packets contain hourly data for six meter information
                        // also data for hourly queue data with six meter
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 674),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 816)
                    {
                        // this packets contain hourly data for seven meter information
                        // also data for hourly queue data with seven meter
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 784),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 928)
                    {
                        // this packets contain hourly data for eight meter information
                        // also data for hourly queue data with eight meter
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 898),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 66)
                    {
                        // this packets contain queue data for one meter information=
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 36),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 98)
                    {
                        // this packets contain queue data for two meter information=
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 68),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 130)
                    {
                        // this packets contain queue data for three meter information=
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 100),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 162)
                    {
                        // this packets contain queue data for four meter information=
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 132),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 194)
                    {
                        // this packets contain queue data for five meter information=
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 164),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 226)
                    {
                        // this packets contain queue data for six meter information=
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 196),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 258)
                    {
                        // this packets contain queue data for seven meter information=
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 228),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 290)
                    {
                        // this packets contain queue data for eight meter information=
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 260),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 42)
                    {
                        // this packets contain simcard balance data
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 12),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 320)
                    {
                        // this packets contain midnight data for 9 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 290),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 352)
                    {
                        // this packets contain midnight data for 10 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 322),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 384)
                    {
                        // this packets contain midnight data for 11 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 354),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 416)
                    {
                        // this packets contain midnight data for 12 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 386),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 448)
                    {
                        // this packets contain midnight data for 13 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 418),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 480)
                    {
                        // this packets contain midnight data for 14 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 450),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 512)
                    {
                        // this packets contain midnight data for 15 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 482),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 544)
                    {
                        // this packets contain midnight data for 16 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 514),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 576)
                    {
                        // this packets contain midnight data for 17 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 546),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 608)
                    {
                        // this packets contain midnight data for 18 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 578),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 640)
                    {
                        // this packets contain midnight data for 19 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 610),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 672)
                    {
                        // this packets contain midnight data for 20 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 642),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 1040)
                    {
                        // this packets contain hourly data for 9 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 1010),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 1152)
                    {
                        // this packets contain hourly data for 10 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 1122),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 1264)
                    {
                        // this packets contain hourly data for 11 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 1234),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 1376)
                    {
                        // this packets contain hourly data for 12 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 1346),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 1488)
                    {
                        // this packets contain hourly data for 13 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 1458),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 1600)
                    {
                        // this packets contain hourly data for 14 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 1570),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 1712)
                    {
                        // this packets contain hourly data for 15 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 1682),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 1824)
                    {
                        // this packets contain hourly data for 16 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 1794),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 1936)
                    {
                        // this packets contain hourly data for 17 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 1906),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 2048)
                    {
                        // this packets contain hourly data for 18 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 2018),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 2160)
                    {
                        // this packets contain hourly data for 19 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 2130),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 2272)
                    {
                        // this packets contain hourly data for 20 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 2242),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 322)
                    {
                        // this packets contain queue data for 9 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 292),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 354)
                    {
                        // this packets contain queue data for 10 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 324),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 386)
                    {
                        // this packets contain queue data for 11 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 356),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 418)
                    {
                        // this packets contain queue data for 12 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 388),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 450)
                    {
                        // this packets contain queue data for 13 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 420),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 482)
                    {
                        // this packets contain queue data for 14 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 452),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 514)
                    {
                        // this packets contain queue data for 15 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 484),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 546)
                    {
                        // this packets contain queue data for 16 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 516),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 578)
                    {
                        // this packets contain queue data for 17 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 548),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 610)
                    {
                        // this packets contain queue data for 18 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 580),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 642)
                    {
                        // this packets contain queue data for 19 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 612),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else if (packet.Length == 674)
                    {
                        // this packets contain queue data for 20 meters
                        var range = packet.Length;
                        Log log = new()
                        {
                            StartByte = "ABCD",
                            PacketLength = packet.Substring(4, 4),
                            DataLoggerSerialNumber = packet.Substring(8, 8),
                            PacketTag = packet.Substring(16, 2),
                            CommandVersion = packet.Substring(18, 2),
                            CommandCode = packet.Substring(20, 2),
                            DataLength = packet.Substring(22, 4),
                            Data = packet.Substring(26, 644),
                            Crc = packet.Substring(range - 4, 4),
                            Date = "",
                        };
                        return log;
                    }
                    else
                    {
                        return "پکت تعریف نشده";
                    }
                }
                else
                {
                    return "پکت تعریف نشده";
                }
            }
        }
        }
}