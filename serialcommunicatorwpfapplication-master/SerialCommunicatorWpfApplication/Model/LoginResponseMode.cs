using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunicatorWpfApplication.Model
{
    public class LoginResponseMode
    {
        public int error { get; set; }
        public string message { get; set; }
    }
    public class LoginResponseOtp
    {
        public string credential { get; set; }
        public string code { get; set; }
    }
}
