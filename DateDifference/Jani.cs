using System;
using Itenso.TimePeriod;

namespace DateDifference
{
  public class Jani : IDateDifference
  {
    private DateDiff m_diff;

    public void SetDates(DateTime start, DateTime end)
    {
      m_diff = new DateDiff(start, end);
    }

    public int GetYears()
    {
      return m_diff.Years;
    }

    public int GetMonths()
    {
      return m_diff.Months;
    }

    public int GetDays()
    {
      return m_diff.Days;
    }
  }

  public class Jani_Test : DateDifferenceTests<Jani>
  {
  }
}
