using System.ComponentModel.DataAnnotations;

namespace Automation.Model
{
    public class AutomationModel
    {
        [Key]
        public int IdVariable { get; set; }
        public int ID_FormChart { get; set; }
        public string NameVariable { get; set; } = string.Empty;
        public string ValueVariable { get; set; } = string.Empty;
    }
}
