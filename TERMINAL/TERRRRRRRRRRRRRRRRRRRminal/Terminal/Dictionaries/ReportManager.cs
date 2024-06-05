namespace Terminal.Dictionaries
{
    public class ReportManager
    {
        private readonly Dictionary<int, string> reportCodes;
        public ReportManager()
        {
            reportCodes = new Dictionary<int, string>();
        }
        public void AddReportCode(int reportCode, string description)
        {
            reportCodes.Add(reportCode, description);
        }
        public string GetDescription(int reportCode)
        {
            if (reportCodes.ContainsKey(reportCode))
            {
                return reportCodes[reportCode];
            }
            else
            {
                return "کد فرمان اشتباه یا تعریف نشده است";
            }
        }
        public bool RemoveReportCode(int reportCode)
        {
            return reportCodes.Remove(reportCode);
        }
    }

}
