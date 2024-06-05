using System.ComponentModel.DataAnnotations;

namespace ETAPI.Models
{
    public class FormChart
    {
        [Key]
        public int ID_FormChart { get; set; }
        public int ID_FormUnit { get; set; }
        public int? ID_PersonelChart { get; set; }
        public int? ID_FormChartNext { get; set; }
        public int LevelTaeed { get; set; }
        public int? ID_Chart_Janeshin { get; set; }
        public bool? IsJaneshin { get; set; }
        public bool IsIf { get; set; }
        public bool Ronevesht { get; set; }
        public string? NameMarhale { get; set; } = string.Empty;
        public int? IdMarhale { get; set; }
        public short? RowMarhale { get; set; }
        public bool? IsEnd { get; set; }
        public bool DynamicIdChart { get; set; }

    }
}
