using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSchedule
{
    public class CommandQueue
    {
        public int Id { get; set; }
        public string? DcuSerial { get; set; }
        public string? Code { get; set; }
        public string? setting_type { get; set; }
        public string? setting_value { get; set; }
        public string? Date {  get; set; }
        public int Done { get; set; }
        public string? MeterSerial { get; set; }
    }
}
