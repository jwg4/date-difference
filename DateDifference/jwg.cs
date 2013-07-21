using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DateDifference
{
  public class jwg : IDateDifference
  {
    private int m_years;
    private int m_months;
    private int m_days;

    public void SetDates(DateTime start, DateTime end)
    {
      Calculate(start, end);
    }

    // start of pasted code
    //- public string LoopAge(DateTime myDOB, DateTime FutureDate)
    public void Calculate(DateTime start, DateTime end)
    {
      DateTime d;

      m_years = 0;
      d = start;
      while (d <= end)
      {
        m_years++;
        d = start.AddYears(m_years);
      }

      m_years--;
      start = start.AddYears(m_years);

      m_months = 0;
      d = start;
      while (d <= end)
      {
        m_months++;
        d = start.AddMonths(m_months);
      }

      m_months--;
      start = start.AddMonths(m_months);

      m_days = 0;
      d = start;
      while (d <= end)
      {
        m_days++;
        d = start.AddDays(m_days);
      }

      m_days--;
    }

    // end of pasted code

    public int GetYears()
    {
      return m_years;
    }

    public int GetMonths()
    {
      return m_months;
    }

    public int GetDays()
    {
      return m_days;
    }
  }

  public class jwg_Test : DateDifferenceTests<jwg>
  {
  }
}
