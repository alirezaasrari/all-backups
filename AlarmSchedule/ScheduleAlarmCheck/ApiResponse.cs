namespace ScheduleAlarmCheck
{
    public class Alarmss
    {
        public string? Code {  get; set; }
        public string? Title { get; set; }
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
        public List<Alarmss>? Data { get; set; }
        public bool Error { get; set; }
        public List<string>? Messages { get; set; }
        public Paginations? Pagination { get; set; }
    }
}
