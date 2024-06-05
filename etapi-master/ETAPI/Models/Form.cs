using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ETAPI.Models
{
    public class Form
    {
        [Key]
        //[Schema("order")]
        public int ID_Form { get; set; }
        public string? FormName { get; set; } = string.Empty;
        public string? Url { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual ICollection<FormUnit>? FormUnits { get; set; }
    }
    
}
