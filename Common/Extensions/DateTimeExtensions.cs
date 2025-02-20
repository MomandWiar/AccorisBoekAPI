namespace Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetWeekNumber(this DateTime date)
        {
            var calendar = System.Globalization.CultureInfo.InvariantCulture.Calendar;
            return calendar.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
    }
}