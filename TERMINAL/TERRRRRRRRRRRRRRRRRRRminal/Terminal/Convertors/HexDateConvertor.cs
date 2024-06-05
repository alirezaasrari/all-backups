namespace Terminal { 
public static class HexDateConverter
{
    public static dynamic ConvertHexToDate(string hexString)
    {
        long unixTimestamp;

        try
        {
            // Convert the hex string to an integer
            unixTimestamp = Convert.ToInt64(hexString, 16);
        }
        catch (FormatException)
        {
            return "زمان با فرمت اشتباه";
        }

        // Convert the Unix timestamp to a datetime object
        return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).UtcDateTime;
    }
}
}