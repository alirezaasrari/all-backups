namespace Terminal.Convertors
{
    public class DecimalToHexCalculator
    {
        public static string DecimalToHex(int decimalNumber)
        {
            string hexString = decimalNumber.ToString("X");
            return hexString;
        }
    }
}
