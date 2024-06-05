using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchduleServiceForCheckingMetersOfDataloggers
{
    public class DataLoggerWaterMeter
    {
        public int Id { get; set; }
        public string? DataLoggerSerial { get; set; }
        public string? WaterMeterSerial { get; set; }
        public int? NumberOfMeters { get; set; }
        public string? DecimalNumber { get; set; }
    }
}
