using System;

public static class DateHelper
{
    public static string ToOrdinalDate(DateTime date)
    {
        string ToOrdinalSuffix(int day)
        {
            if (day % 100 >= 11 && day % 100 <= 13) return "th";
            return (day % 10) switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th"
            };
        }

        return $"{date.Day}{ToOrdinalSuffix(date.Day)} {date:MMMM yyyy}";
    }
}