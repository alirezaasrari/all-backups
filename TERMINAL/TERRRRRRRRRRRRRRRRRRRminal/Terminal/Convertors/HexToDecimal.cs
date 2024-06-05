namespace Terminal
{
    public static class HexToDecimalConvertor
    {
        public static long HexToDecimal(string hexString)
        {
            string hexNumber = hexString;
            long decimalEquivalent = Convert.ToInt64(hexNumber, 16);
            return decimalEquivalent;
        }
    }
}
