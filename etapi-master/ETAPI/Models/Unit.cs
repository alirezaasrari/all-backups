using System.ComponentModel.DataAnnotations;

namespace ETAPI.Models
{
    public class Unit
    {
        [Key]
        public int IdUnit { get; set; }
        public string NameUnit { get; set; } = string.Empty;
        public bool ActiveUnit { get; set; }
    }
}
