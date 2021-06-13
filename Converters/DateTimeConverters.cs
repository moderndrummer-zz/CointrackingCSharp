using System;

public static class DateTimeConverters
{
    public static long ToUnixTimeSeconds(this DateTime dateTime) => new DateTimeOffset(dateTime).ToUnixTimeSeconds();
    public static DateTime ToDateTime(this long unixTimeSeconds) => DateTimeOffset.FromUnixTimeSeconds(unixTimeSeconds).LocalDateTime;
}