using System.ComponentModel.DataAnnotations;

namespace ETAPI.Models
{
    public class Taeed
    {
        [Key]
        public int IdTaeed { get; set; }
        public int IdCodeFormX { get; set; }
        public int? IdFormChart { get; set; }
        public short CheckTaeed { get; set; }
        public DateTime DateInsert { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime? DateTaeed { get; set; }
        public int IdUserInsert { get; set; }
        public int? IdUserTaeed { get; set; }
        public string? DescPrev { get; set; }
        public string? DescTaeed { get; set; }
        public short? HasOpen { get; set; }
        public short? RoneveshtType { get; set; }
        public short? IsActive { get; set; }
        public bool? IsSendSms { get; set; }
        public int? IdPersonelChart { get; set; }

    }
}
