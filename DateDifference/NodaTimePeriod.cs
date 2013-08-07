using System;
using NodaTime;

namespace DateDifference
{
    public class NodaTimePeriod : IDateDifference
    {
        private Period _period;

        public void SetDates(DateTime start, DateTime end)
        {
            var ldt1 = LocalDateTime.FromDateTime(start);
            var ldt2 = LocalDateTime.FromDateTime(end);
            _period = Period.Between(ldt1, ldt2);
        }

        public int GetYears()
        {
            return (int) _period.Years;
        }

        public int GetMonths()
        {
            return (int) _period.Months;
        }

        public int GetDays()
        {
            return (int) _period.Days;
        }
    }

    public class NodaTimePeriod_Test : DateDifferenceTests<NodaTimePeriod>
    {
    }
}
