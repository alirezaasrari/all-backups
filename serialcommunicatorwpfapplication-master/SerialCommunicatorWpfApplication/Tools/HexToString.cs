using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunicatorWpfApplication.Tools
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
