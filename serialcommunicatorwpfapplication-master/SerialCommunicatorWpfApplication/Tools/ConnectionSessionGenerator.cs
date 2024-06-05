using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunicatorWpfApplication.Tools
{
    public static class ConnectionSessionGenerator
    {
        public static string RandomHexGenerator()
        {
            // Create a Random object
            Random random = new Random();

            // Convert minimum and maximum values (1000 and FFFF) from hex to integers
            int minInt = int.Parse("1000", System.Globalization.NumberStyles.HexNumber);
            int maxInt = int.Parse("FFFF", System.Globalization.NumberStyles.HexNumber);

            // Generate a random integer within the specified range (inclusive)
            int randomInt = random.Next(minInt, maxInt + 1);

            // Convert the random integer back to a hex string with uppercase characters
            return randomInt.ToString("X");
        }
    }      
}
