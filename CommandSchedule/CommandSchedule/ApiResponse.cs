namespace CommandSchedule
{
    public class Command
    {
          public string? Fk_CommandTypeCode { get; set; }
          public string? CommandTypeTitle { get; set; }
          public string? Fk_DataLoggerSerial { get; set; }
          public string? Fk_WaterMeterSerial { get; set; }
          public Meta? MetaData {  get; set; }
    }
    public class Meta
    {
        public string? date { get; set; }
        public string? setting_type { get; set; }
        public string? setting_value { get; set; }
    }
    public class Paginations
    {
        public int TotalCount { get; set; }
        public int PerPage { get; set; }
        public int CurrentPage { get; set; }
        public int? PrevPage { get; set; }
        public int? NextPage { get; set; }
        public int LastPage { get; set; }
    }
    public class ApiResponse
    {
        public List<Command>? Data { get; set; }
        public bool Error { get; set; }
        public List<string>? Messages { get; set; }
        public Paginations? Pagination { get; set; }
    }
}
