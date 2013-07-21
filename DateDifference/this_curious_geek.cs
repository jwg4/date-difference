using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DateDifference
{
  public class this_curious_geek : IDateDifference
  {
    private int m_years;
    private int m_months;
    private int m_days;
    
    public void SetDates(DateTime start, DateTime end)
    {
      TimeSpanToDate(start, end, out m_years, out m_months, out m_days);
    }

    // start of pasted code
    public void TimeSpanToDate(DateTime d1, DateTime d2, out int years, out int months, out int days)
    {
      // compute & return the difference of two dates,
      // returning years, months & days
      // d1 should be the larger (newest) of the two dates
      // we want d1 to be the larger (newest) date
      // flip if we need to
      if (d1 < d2)
      {
        DateTime d3 = d2;
        d2 = d1;
        d1 = d3;
      }

      // compute difference in total months
      months = 12 * (d1.Year - d2.Year) + (d1.Month - d2.Month);

      // based upon the 'days',
      // adjust months & compute actual days difference
      if (d1.Day < d2.Day)
      {
        months--;
        days = DateTime.DaysInMonth(d2.Year, d2.Month) - d2.Day + d1.Day;
      }
      else
      {
        days = d1.Day - d2.Day;
      }
      // compute years & actual months
      years = months / 12;
      months -= years * 12;
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

  public class this_curious_geek_Test : DateDifferenceTests<this_curious_geek>
  {
  }
}
