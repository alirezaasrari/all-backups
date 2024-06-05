namespace Terminal.Model
{
    public class WaterMeterTotalUsage
    {
        public string? Fk_WaterMeterSerial { get; set; }
        public string? Date { get; set; }
        public int Usage { get; set; }
        public string? CommandCode { get; set; }
    }
}
