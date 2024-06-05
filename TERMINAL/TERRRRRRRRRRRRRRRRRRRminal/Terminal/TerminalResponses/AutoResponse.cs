using Terminal.Convertors;
using Terminal.Tools;

namespace Terminal.TerminalResponses
{
    public static class AutoResponse
    {
        public static string Response(string request,string dataloggerserialnumber, string meternumber = "0000", int day = 0, int month = 0)
        {
            if(request == "simcard")
            {
                // this is a request that is
                // sent from server to the datalogger
                // to receive response 
                // about the simcard balance
                var PacketTag = RandomHexbetweenZeroAnd255.GetRandomByte();
                return $"ABCD0011{dataloggerserialnumber}{PacketTag}0145000200fc+" +
                    $"{CrcCalculator.CalculateChecksum($"{dataloggerserialnumber}{PacketTag}0145000200fc")}";
            }
            else if(request == "currentusage")
            {
                // this is a request that is 
                // sent from server to the dataloggeer 
                // to set settings
                // for the datalogger
                var PacketTag = RandomHexbetweenZeroAnd255.GetRandomByte();
                return $"ABCD0011{dataloggerserialnumber}{PacketTag}0140000200fc +" +
                    $" {CrcCalculator.CalculateChecksum($"{dataloggerserialnumber}{PacketTag}0140000200fc")}";
            }
            else if (request == "hourlyprofile")
            {
                // this is a request that is 
                // sent from server to the dataloggeer 
                // to receive hourly profile data
                // for the datalogger
                var PacketTag = RandomHexbetweenZeroAnd255.GetRandomByte();
                var uniqueid = RandomHexbetweenZeroAnd255.GetRandomByte();
                return $"ABCD003A{dataloggerserialnumber}{PacketTag}01410002{uniqueid}{meternumber}{day} +" +
                    $" {CrcCalculator.CalculateChecksum($"{dataloggerserialnumber}{PacketTag}01410002{uniqueid}{meternumber}{day}")}";
            }
            else if (request == "dailyusage")
            {
                // this is a request that is 
                // sent from server to the dataloggeer 
                // to receive  daily data
                // for the datalogger
                var PacketTag = RandomHexbetweenZeroAnd255.GetRandomByte();
                var uniqueid = RandomHexbetweenZeroAnd255.GetRandomByte();
                return $"ABCD003A{dataloggerserialnumber}{PacketTag}01420002{uniqueid}{meternumber}{day} +" +
                    $" {CrcCalculator.CalculateChecksum($"{dataloggerserialnumber}{PacketTag}01420002{uniqueid}{meternumber}{day}")}";
            }
            else if (request == "monthlyusage")
            {
                // this is a request that is 
                // sent from server to the dataloggeer 
                // to receive  monthly data
                // for the datalogger
                var PacketTag = RandomHexbetweenZeroAnd255.GetRandomByte();
                var uniqueid = RandomHexbetweenZeroAnd255.GetRandomByte();
                return $"ABCD003A{dataloggerserialnumber}{PacketTag}01420002{uniqueid}{meternumber}{day} +" +
                    $" {CrcCalculator.CalculateChecksum($"{dataloggerserialnumber}{PacketTag}01420002{uniqueid}{meternumber}{month}")}";
            }
            else if (request == "totalusage")
            {
                // this is a request that is 
                // sent from server to the dataloggeer 
                // to receive total data
                // for the datalogger
                var PacketTag = RandomHexbetweenZeroAnd255.GetRandomByte();
                var uniqueid = RandomHexbetweenZeroAnd255.GetRandomByte();
                return $"ABCD003A{dataloggerserialnumber}{PacketTag}01440002{uniqueid}{meternumber} +" +
                    $" {CrcCalculator.CalculateChecksum($"{dataloggerserialnumber}{PacketTag}01440002{uniqueid}{meternumber}")}";
            }
            else if (request == "fota")
            {
                // this is a request that is 
                // sent from server to the dataloggeer 
                // for fota to the datalogger
                var PacketTag = RandomHexbetweenZeroAnd255.GetRandomByte();
                var uniqueid = RandomHexbetweenZeroAnd255.GetRandomByte();
                var firmwaredata = "";
                return $"ABCD003A{dataloggerserialnumber}{PacketTag}01490002{uniqueid}{firmwaredata} +" +
                    $" {CrcCalculator.CalculateChecksum($"{dataloggerserialnumber}{PacketTag}01490002{uniqueid}{firmwaredata}")}";
            }
            else if (request == "setsetting")
            {
                // this is a request that is 
                // sent from server to the dataloggeer 
                // for set setting to the datalogger
                var PacketTag = RandomHexbetweenZeroAnd255.GetRandomByte();
                var uniqueid = RandomHexbetweenZeroAnd255.GetRandomByte();
                return $"ABCD003A{dataloggerserialnumber}{PacketTag}01460002{uniqueid} +" +
                    $" {CrcCalculator.CalculateChecksum($"{dataloggerserialnumber}{PacketTag}01460002{uniqueid}")}";
            }
            else if (request == "getsetting")
            {
                // this is a request that is 
                // sent from server to the dataloggeer 
                // for get setting to the datalogger
                var PacketTag = RandomHexbetweenZeroAnd255.GetRandomByte();
                var uniqueid = RandomHexbetweenZeroAnd255.GetRandomByte();
                return $"ABCD003A{dataloggerserialnumber}{PacketTag}01480002{uniqueid} +" +
                    $" {CrcCalculator.CalculateChecksum($"{dataloggerserialnumber}{PacketTag}01480002{uniqueid}")}";
            }
            else if (request == "getsettingerror")
            {
                // this is a request that is 
                // sent from server to the dataloggeer 
                // for get setting error to the datalogger
                var PacketTag = RandomHexbetweenZeroAnd255.GetRandomByte();
                var uniqueid = RandomHexbetweenZeroAnd255.GetRandomByte();
                return $"ABCD003A{dataloggerserialnumber}{PacketTag}01470002{uniqueid} +" +
                    $" {CrcCalculator.CalculateChecksum($"{dataloggerserialnumber}{PacketTag}01470002{uniqueid}")}";
            }
            return "wrong";
        }
    }
}
