namespace Terminal
{ 
public class TimestampToDateConverter
{
    public static DateTime ConvertTimestampToDate(long timestamp)
    {
        return DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime.Date;
    }

    public static dynamic ConvertorTimestamp(string timestamp)
    {
            string? input = timestamp;
        if (input != null)
        { 
        long time = long.Parse(input);
            DateTime date = ConvertTimestampToDate(time);
            return date;
        }
        else 
        {
                return "زمان با فرمت اشتباه";
        }
    }
}
}
