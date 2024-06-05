using System;
namespace ScheduleService
{
    public class ApiResponse
    {
        public DataItem Data { get; set; } = new DataItem();
        public bool Error { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public Paginations Pagination { get; set; } = new Paginations();
        public class DataItem
        {
            public List<string> Columns { get; set; } = new List<string>();
            public List<Datalogger> Rows { get; set; } = new List<Datalogger>();
        }
        public class Datalogger
        {
            public long Pk_DataLoggerId { get; set; }
            public long Fk_DataLoggerTypeId { get; set; }
            public string Serial { get; set; } = string.Empty;
            public string Title { get; set; } = string.Empty;
            public string Mobile { get; set; } = string.Empty;
            public DateTime? LastConnection { get; set; }
            public DateTime? InstallDate { get; set; }
            public string InstallAddress { get; set; } = string.Empty;
            public object GeoLocation { get; set; } = new object();
            public DateTime? CreatedAt { get; set; }
            public DateTime? DeletedAt { get; set; }
            public string CityTitle { get; set; } = string.Empty;
            public string ProvinceTitle { get; set; } = string.Empty;
            public string AreaTitle { get; set; } = string.Empty;
        }
        public class Paginations
        {
            public int TotalCount { get; set; }
            public int PerPage { get; set; }
            public int CurrentPage { get; set; }
            public string PrevPage { get; set; } = string.Empty;
            public string NextPage { get; set; } = string.Empty;
            public int LastPage { get; set; }
        }
    }
}
