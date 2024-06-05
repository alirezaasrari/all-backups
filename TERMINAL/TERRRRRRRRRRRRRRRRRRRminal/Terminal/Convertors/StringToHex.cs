using System.Text;

namespace Terminal.Convertors
{
    public static class StringToHex
    {
        public static string ToHexString(string inputString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            StringBuilder hexString = new();

            foreach (byte b in bytes)
            {
                hexString.Append(b.ToString("X2"));
            }

            return hexString.ToString();
        }
    }
}