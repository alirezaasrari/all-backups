namespace SchduleServiceForCheckingMetersOfDataloggers
{
    public class DataLoggers
    {
        public int Id { get; set; }
        public string? DataLogger { get; set; }
        public int? Pk_DataLoggerId { get; set; }
        public int? Pk { get; set; } = 0;
        public string? CreatedAt { get; set; }
        public string? DeletedAt { get; set;}
    }
}
