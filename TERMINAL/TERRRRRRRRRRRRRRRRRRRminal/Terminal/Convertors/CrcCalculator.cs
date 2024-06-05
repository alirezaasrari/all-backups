namespace Terminal.Convertors
{
    public static class CrcCalculator
    {
        public static string CalculateChecksum(string packet)
        {
            if (!IsValidHexadecimalString(packet))
            {
                return("فرمت عدد هگز اشتباه است");
            }
            byte[] byteArray = Convert.FromHexString(packet);
            ushort crc = 0xFFFF;
            for (int i = 0; i < byteArray.Length; ++i)
            {
                byte byteValue = byteArray[i];
                crc ^= (ushort)(byteValue << 8);
                for (int bit = 0; bit < 8; ++bit)
                {
                    if ((crc & 0x8000) != 0)
                    {
                        crc = (ushort)(crc << 1 ^ 0x1021); // Polynomial: x^16 + x^12 + x^5 + 1
                    }
                    else
                    {
                        crc = (ushort)(crc << 1);
                    }
                }
            }
            ushort rawCRC = (ushort)(crc & 0xFFFF);
            string hexCRC = rawCRC.ToString("X4");
           // Console.WriteLine("Generated CRC: " + hexCRC.ToUpper());

            return hexCRC.ToUpper();
        }
        public static bool IsValidHexadecimalString(string input)
        {
            if (input.Length % 2 != 0)
            {
                return false;
            }
            foreach (char c in input)
            {
                if (!IsHexadecimalDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsHexadecimalDigit(char c)
        {
            return c >= '0' && c <= '9' || c >= 'A' && c <= 'F' || c >= 'a' && c <= 'f';
        }
    }
}