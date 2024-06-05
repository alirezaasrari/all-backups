using System.ComponentModel.DataAnnotations;

namespace ETAPI.Models
{
    public class If
    {
        [Key]
        public int IdIf { get; set; }
        public int IdVariable { get; set; }
        public string ValueVariable { get; set; } = string.Empty;
        public int Next_ID_FormChart { get; set; }
    }
}
