using System.ComponentModel.DataAnnotations;

namespace ETAPI.Models
{
    public class CodeFormx
    {
        [Key]
        public int IdCodeFormX { get; set; }
        public int ID_FormUnit { get; set; }
        public int CodeFormX { get; set; }
    }
}
