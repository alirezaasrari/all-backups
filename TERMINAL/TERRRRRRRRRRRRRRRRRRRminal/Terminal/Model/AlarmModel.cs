namespace Terminal.Model
{
    public class AlarmModel
    {
        public string? Fk_AlarmTypeCode { get; set; }
        public string? Fk_DataLoggerSerial { get; set; }
        public string? Fk_WaterMeterSerial { get; set; }
        public string? ActionAt { get; set; }
    }
}