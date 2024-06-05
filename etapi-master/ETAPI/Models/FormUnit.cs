using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETAPI.Models
{
    public class FormUnit
    {
        [Key]
        public int ID_FormUnit { get; set; }
        public int ID_Unit { get; set; }
        public string TittleForm { get; set; } = string.Empty;
        public int ID_Form { get; set; }
        public virtual Form? Form { get; set; }
    }
}
