using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunicatorWpfApplication.Model
{
    public class ProductionPanel
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public string? Serial { get; set; }
        public string? Owner { get; set; }
        public string? Type { get; set; }
        public string? Operator { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
