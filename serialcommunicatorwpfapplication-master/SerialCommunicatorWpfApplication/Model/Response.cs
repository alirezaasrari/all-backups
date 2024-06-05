﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace SerialCommunicatorWpfApplication.Model
{
    public class Response
    {
            public int error { get; set; }
            public Data data { get; set; }      
    }

    public class LoginRequest
    {
        public string credential { get; set; }
    }
}
