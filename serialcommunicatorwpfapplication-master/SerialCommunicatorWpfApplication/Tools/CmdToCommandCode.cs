using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunicatorWpfApplication.Tools
{
    public static class CmdToCommandCode
    {
        public static string CmdToCmdCode(string cmd)
        {
            if (cmd == "Open Session")
            {
                return "01";
            }
            else if (cmd == "Close Session")
            {
                return "02";
            }
            else if (cmd == "Get DCU Setting")
            {
                return "03";
            }
            else if (cmd == "Set DCU Setting")
            {
                return "04";
            }
            else if (cmd == "Get Meter Setting")
            {
                return "05";
            }
            else if (cmd == "Set Meter Setting")
            {
                return "06";
            }
            else if (cmd == "Get Valve Setting")
            {
                return "07";
            }
            else if (cmd == "Set Valve Setting")
            {
                return "08";
            }
            else if (cmd == "Get DateTime Setting")
            {
                return "09";
            }
            else if (cmd == "Set DateTime Setting")
            {
                return "0A";
            }
            else if (cmd == "Set FWU Parameters")
            {
                return "0B";
            }
            else if (cmd == "FWU Chunk")
            {
                return "0C";
            }
            else if (cmd == "FWU Verify")
            {
                return "0D";
            }
            else if (cmd == "Get DCU Data")
            {
                return "0E";
            }
            else if (cmd == "FWU Complete")
            {
                return "0F";
            }
            else
            {
                return "";
            }
        }
    }
}
