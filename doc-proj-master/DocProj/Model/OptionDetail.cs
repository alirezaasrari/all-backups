using System.Text.Json.Serialization;

namespace DocProj.Model
{
    public class OptionDetail
    {
        public int Id { get; set; }
        public int ListOfOptionId { get; set; }
        public int? Row { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Code { get; set; } = string.Empty;
        public string? Color { get; set; } = string.Empty;
        public string? Position { get; set; } = string.Empty;
        [JsonIgnore]
        public byte[]? PdfFile { get; set; }
        [JsonIgnore]
        public virtual ListOfOption? ListOfOption { get; set; }
    }
}
