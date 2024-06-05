namespace ScheduleServiceForWaterMetersOfDatalogger
{
    public class Datalogger
    {
        public long Pk_DataLoggerId { get; set; }
        public long Fk_DataLoggerTypeId { get; set; }
        public string? Serial { get; set; }
        public string? Title { get; set; }
        public string? Mobile { get; set; }
        public DateTime? LastConnection { get; set; }
        public DateTime? InstallDate { get; set; }
        public string? InstallAddress { get; set; }
        public object? GeoLocation { get; set; }
        public string? CreatedAt { get; set; }
        public string? CityTitle { get; set; }
        public string? ProvinceTitle { get; set; }
        public string? AreaTitle { get; set; }
        public List<WaterMeter>? WaterMeters { get; set; }
    }
    public class ApiResponse
    {
        public Datalogger? Data { get; set; }
        public bool Error { get; set; }
        public List<string>? Messages { get; set; }
    }

    public class WaterMeter
    {
        public int Pk_WaterMeterId { get; set; }
        public int? Fk_DataLoggerId { get; set; }
        public string? Serial {  get; set; }
        public string? SubscriptionNumber { get; set; }
        public string? InstallDate{ get; set; }
        public string? InstallAddress{ get; set; }
        public string? GeoLocation{ get; set; }
        public string? CreatedAt { get; set; }
        public string? DeletedAt { get; set; }
    }
}
