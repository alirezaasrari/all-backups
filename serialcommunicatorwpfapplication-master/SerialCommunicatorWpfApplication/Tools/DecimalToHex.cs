using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunicatorWpfApplication.Tools
{
    public static class DecimalToHex
    {
        public static string DecimalToHexx(int decimalNumber)
        {
            string hexString = decimalNumber.ToString("X4");
            return hexString;
        }
    }
}
