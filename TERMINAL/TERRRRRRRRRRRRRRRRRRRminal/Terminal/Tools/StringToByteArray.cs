using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Tools
{
    public static class StringToByteArrayy
    {
        public static byte[] StringToByteArray(string hexString)
        {
            // Validate the hex string length (should be divisible by 2)
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException("Hex string length must be a multiple of 2");
            }
            // Create a byte array with half the length of the hex string
            byte[] bytes = new byte[hexString.Length / 2];

            // Loop through each character pair and convert to byte
            for (int i = 0; i < bytes.Length; i++)
            {
                string hexPair = hexString.Substring(i * 2, 2);
                bytes[i] = Convert.ToByte(hexPair, 16);
            }

            return bytes;
        }
    }
}
