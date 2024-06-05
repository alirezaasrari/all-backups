using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Model
{
    public class CommandQueue
    {
        public int Id { get; set; }
        public string? DcuSerial { get; set; }
        public string? Code { get; set; }
        public string? setting_type { get; set; }
        public string? setting_value { get; set; }
        public string? Date { get; set; }
        public string? MeterSerial { get; set; }
        public int? Done { get; set; }
        public int? Completed { get; set; }
        public string? UID { get; set; }
    }
}
