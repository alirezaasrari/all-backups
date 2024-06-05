using System.ComponentModel.DataAnnotations;

namespace Terminal.Model
{
    public class DataLoggers
    {
        public int Id { get; set; }
        public string? DataLogger { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; } // Nullable type for possible null values
        public int? Pk_DataLoggerId { get; set; }
    }

}
