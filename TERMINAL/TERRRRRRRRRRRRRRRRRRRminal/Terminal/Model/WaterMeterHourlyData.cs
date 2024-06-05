namespace Terminal.Model
{
    public class WaterMeterHourlyData
    {
        public string? Fk_WaterMeterSerial { get; set; }
        public string Date { get; set; }
        public string? CommandCode { get; set; }
        public Dictionary<string, int>? UsageList { get; set; }
    }
}
