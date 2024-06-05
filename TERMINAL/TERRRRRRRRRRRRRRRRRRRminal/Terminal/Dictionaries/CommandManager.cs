namespace Terminal.Dictionaries
{
    public static class CommandManager
    {
        private static readonly Dictionary<string, string> commands = new();
        static CommandManager()
        {
            commands.Add("pulse", "01");
            commands.Add("MBUS", "02");
            commands.Add("ultrasonic", "03");
            commands.Add("electromagnetic", "04");

            commands.Add("data ok", "30");
            commands.Add("crc error", "31");
            commands.Add("current usage", "40");
            commands.Add("hourly profile usage", "41");
            commands.Add("daily usage", "42");
            commands.Add("monthly usage", "43");
            commands.Add("total usage", "44");
            commands.Add("simcard balance", "45");
            commands.Add("set setting", "46");
            commands.Add("set setting error", "47");
            commands.Add("get setting", "48");
            commands.Add("FOTA", "49");
            commands.Add("FOTA updating", "4A");
            commands.Add("FOTA updating.", "4a");
            commands.Add("FOTA ok", "4B");
            commands.Add("FOTA ok.", "4b");
            commands.Add("FOTA error", "4C");
            commands.Add("FOTA error.", "4c");

            commands.Add("midnight data", "A0");
            commands.Add("midnight data.", "a0");
            commands.Add("hourly profile data", "A1");
            commands.Add("hourly profile data.", "a1");
            commands.Add("month data", "A2");
            commands.Add("month data.", "a2");
            commands.Add("queue data", "A3");
            commands.Add("queue data.", "a3");
            commands.Add("hourly queue data", "A4");
            commands.Add("hourly queue data.", "a4");
            commands.Add("check request data", "A5");
            commands.Add("check request data.", "a5");
            commands.Add("alarm data", "A6");
            commands.Add("alarm data.", "a6");
            commands.Add("test primary server", "A7");
            commands.Add("test primary server.", "a7");
            commands.Add("test backup server", "A8");
            commands.Add("test backup server.", "a8");

            commands.Add("firmware crc error", "10");
            commands.Add("firmware file size not match", "11");
            commands.Add("firmware file size too big", "12");
            commands.Add("firmware file not found", "13");
            commands.Add("firmware file not saved", "14");
            commands.Add("firmware FTP server error", "15");

            commands.Add("power down", "50");
            commands.Add("power up", "51");
            commands.Add("rs485 configured", "52");
            commands.Add("EEPROM write error", "53");
            commands.Add("magnetic field", "54");
            commands.Add("leak", "55");
            commands.Add("burest", "56");
            commands.Add("displacement", "57");
            commands.Add("reverse", "58");
            commands.Add("meter write disconnected", "59");

            commands.Add("box tamper open", "5A");
            commands.Add("box tamper open.", "5a");
            commands.Add("box tamper close", "5B");
            commands.Add("box tamper close.", "5b");
            commands.Add("meter open", "5C");
            commands.Add("meter open.", "5c");
            commands.Add("electrical current connected", "5D");
            commands.Add("electrical current connected.", "5d");
            commands.Add("tempered water flow detected", "5E");
            commands.Add("tempered water flow detected.", "5e");
            commands.Add("master key changed", "5F");
            commands.Add("master key changed.", "5f");

            commands.Add("empty pipe", "60");
            commands.Add("credit simcard value", "61");
            commands.Add("remaining water charge", "62");
            commands.Add("successfull authentication", "63");
            commands.Add("authentication failed", "64");
            commands.Add("operation key changed", "65");
            commands.Add("secrete 1 for secure algorithm has changed", "66");
            commands.Add("secrete 2 for secure algorithm has changed", "67");
            commands.Add("clock adjusted", "68");
            commands.Add("replace meter battery", "69");

            commands.Add("battery life", "6A");
            commands.Add("battery life.", "6a");
            commands.Add("firmware activated", "6B");
            commands.Add("firmware activated.", "6b");
            commands.Add("application error", "6C");
            commands.Add("application error.", "6c");
            commands.Add("even log cleared", "6D");
            commands.Add("even log cleared.", "6d");
            commands.Add("EEPROM read error", "6E");
            commands.Add("EEPROM read error.", "6e");
            commands.Add("EEPROM checksum error", "6F");
            commands.Add("EEPROM checksum error.", "6f");

            commands.Add("alarm valve open", "70");
            commands.Add("alarm valve close", "71");
            commands.Add("alarm valve not work", "72");
            commands.Add("alarm temperature error (frozen)", "73");
            commands.Add("credit assignment", "74");
            commands.Add("flow rate exceeded", "75");
            commands.Add("permitted volume threshold exceeded", "76");
            commands.Add("electrical current disconnected", "77");
            commands.Add("no response from digital meter", "78");
            commands.Add("replace datalogger-battery", "79");
        }
        public static void AddCommand(string command, string description)
        {
            commands.Add(command, description);
        }
        public static string GetCommandDescription(string command)
        {
            foreach (var entry in commands)
            {
                if (entry.Value == command)
                {
                    return entry.Key;
                }
            }

            return "کد اشتباه";
        }
        public static bool RemoveCommand(string command)
        {
            return commands.Remove(command);
        }
    }

}
