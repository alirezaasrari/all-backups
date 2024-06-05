using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Convertors
{
    public static class DecoderFromAsciToHex
    {
        public static string Decoder(string inp) 
        {
            char[] charValues = inp.ToCharArray();
            string hexOutput = "";
            foreach (char _eachChar in charValues)
            {
                int value = Convert.ToInt32(_eachChar);
                hexOutput += "0x" + value.ToString("X") + " ";
            }

            return hexOutput; 

        }
    }
}
