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
      return m_diff.ElapsedYears;
    }

    public int GetMonths()
    {
      return m_diff.ElapsedMonths;
    }

    public int GetDays()
    {
      return m_diff.ElapsedDays;
    }
  }

  public class Jani_Test : DateDifferenceTests<Jani>
  {
  }
}
