using System.Text;

namespace Terminal.Convertors
{
    public static class HexToString
    {
        public static string HexToStringConvert(string hexMessage)
        {
            byte[] hexBytes = new byte[hexMessage.Length / 2];

            for (int i = 0; i < hexMessage.Length; i += 2)
            {
                hexBytes[i / 2] = byte.Parse(hexMessage.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
            }

            return Encoding.UTF8.GetString(hexBytes);

        }

    }
}
