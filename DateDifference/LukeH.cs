using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DateDifference
{
  public class LukeH : IDateDifference
  {
    private int m_years;
    private int m_months;
    private int m_days;

    public void SetDates(DateTime start, DateTime end)
    {
      ToAgeString(start, end);
    }

    // start of pasted code
    //-    public static string ToAgeString(this DateTime dob)
    public void ToAgeString(DateTime dob, DateTime dt) //+  
    {
      //- DateTime dt = DateTime.Now;

      int days = dt.Day - dob.Day;
      if (days < 0)
      {
        dt = dt.AddMonths(-1);
        days += DateTime.DaysInMonth(dt.Year, dt.Month);
      }

      int months = dt.Month - dob.Month;
      if (months < 0)
      {
        dt = dt.AddYears(-1);
        months += 12;
      }

      int years = dt.Year - dob.Year;

      //-   return string.Format("{0} year{1}, {2} month{3} and {4} day{5}",
      //-                        years, (years == 1) ? "" : "s",
      //-                        months, (months == 1) ? "" : "s",
      //-                        days, (days == 1) ? "" : "s");
      m_years = years; //+
      m_months = months; //+
      m_days = days; //+
    }

    // End of pasted code

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

  public class LukeH_Test : DateDifferenceTests<LukeH>
  {
  }
  }
