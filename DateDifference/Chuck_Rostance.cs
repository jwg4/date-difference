﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DateDifference
{
  public class Chuck_Rostance : IDateDifference
  {
    private int m_years;
    private int m_months;
    private int m_days;

    public void SetDates(DateTime start, DateTime end)
    {
      int tmp1, tmp2;
      TimeSpanToDateParts(start, end, out m_years, out m_months, out m_days, out tmp1, out tmp2);
    }

    // start of pasted code
    public static void TimeSpanToDateParts(DateTime d1, DateTime d2, out int years, out int months, out int days, out int hours, out int minutes)
    {
      if (d1 < d2)
      {
        var d3 = d2;
        d2 = d1;
        d1 = d3;
      }

      var span = d1 - d2;

      months = 12 * (d1.Year - d2.Year) + (d1.Month - d2.Month);

      //month may need to be decremented because the above calculates the ceiling of the months, not the floor.
      //to do so we increase d2 by the same number of months and compare.
      //(500ms fudge factor because datetimes are not precise enough to compare exactly)
      if (d1.CompareTo(d2.AddMonths(months).AddMilliseconds(-500)) <= 0)
      {
        --months;
      }

      years = months / 12;
      months -= years * 12;

      if (months == 0 && years == 0)
      {
        days = span.Days;
      }
      else
      {
        var md1 = new DateTime(d1.Year, d1.Month, d1.Day);
        var md2 = new DateTime(d2.Year, d2.Month, d1.Day);
        var mDays = (int)(md1 - md2).TotalDays;

        if (mDays > span.Days)
        {
          mDays = (int)(md1.AddMonths(-1) - md2).TotalDays;
        }

        days = span.Days - mDays;


      }
      hours = span.Hours;
      minutes = span.Minutes;
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

  public class ChuckRostance_Test : DateDifferenceTests<Chuck_Rostance>
  {
  }
}
