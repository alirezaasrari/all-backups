namespace ScheduleService
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
        public string? DeletedAt { get; set; }
        public string? CityTitle { get; set; }
        public string? ProvinceTitle { get; set; }
        public string? AreaTitle { get; set; }
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
        public List<Datalogger>? Data { get; set; }
        public bool Error { get; set; }
        public List<string>? Messages { get; set; }
        public Paginations? Pagination { get; set; }
    }
}
