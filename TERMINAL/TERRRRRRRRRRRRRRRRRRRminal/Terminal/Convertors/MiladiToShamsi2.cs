using System.Globalization;
using System.Text;

namespace Terminal.Convertors
{
    public class MiladiToShamsi2
    {
        public static string ConvertMiladiToshamsi(DateTime now)
        {
            PersianCalendar persianCalendar = new();
            int persianYear = persianCalendar.GetYear(now);
            int persianMonth = persianCalendar.GetMonth(now);
            int persianDay = persianCalendar.GetDayOfMonth(now);

            StringBuilder persianDateTimeString = new();
            persianDateTimeString.Append($"{persianYear:0000}-{persianMonth:00}-{persianDay:00} ");
            return (persianDateTimeString.ToString());
        }
    }
}
