using System;

namespace BetfairAPING
{
    [ToString]
    public class TimeRange
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public static TimeRange Since(DateTime from)
        {
            return new TimeRange { From = from, To = DateTime.Now };
        }

        public static TimeRange Since(TimeSpan from)
        {
            var now = DateTime.Now;
            return new TimeRange { From = now.Subtract(from), To = now };
        }
    }
}