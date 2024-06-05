using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunicatorWpfApplication.Tools
{
    public static class StringToHexConvertor
    {
        public static string StringToHex(string input)
        {
            // Use StringBuilder for efficient string concatenation
            StringBuilder hex = new StringBuilder(input.Length * 2);

            // Iterate through each character in the input string
            foreach (char c in input)
            {
                // Convert the character to its hexadecimal representation
                hex.AppendFormat("{0:X2}", (int)c);
            }

            // Return the resulting hexadecimal string
            return hex.ToString();
        }
    }
}
