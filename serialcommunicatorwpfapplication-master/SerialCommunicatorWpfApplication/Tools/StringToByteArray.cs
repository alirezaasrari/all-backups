using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunicatorWpfApplication.Tools
{
    public static class StringToByteArray
    {
        public static byte[] StringToByteArrayy(string hexString)
        {
            int length = hexString.Length;
            if (length % 2 != 0)
            {
                throw new ArgumentException("Invalid hex string length (must be even)");
            }
            byte[] data = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                data[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return data;
        }
    }
}
