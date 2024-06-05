namespace ScheduleService
{
    public class DataLoggers
    {
        public int Id { get; set; }
        public string? DataLogger { get; set; } = string.Empty;
        public int? Pk { get; set; } = 0;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set;} = DateTime.Now;
    }
}
