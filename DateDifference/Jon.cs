using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DateDifference
{
  public class Jon : IDateDifference
  {
    private int m_years;
    private int m_months;
    private int m_days;

    public void SetDates(DateTime start, DateTime end)
    {
      LoopAge(start, end);
    }

    // start of pasted code
    //- public string LoopAge(DateTime myDOB, DateTime FutureDate)
    public void LoopAge(DateTime myDOB, DateTime FutureDate)
    {
      int years = 0;
      int months = 0;
      int days = 0;

      DateTime tmpMyDOB = new DateTime(myDOB.Year, myDOB.Month, 1);

      DateTime tmpFutureDate = new DateTime(FutureDate.Year, FutureDate.Month, 1);

      while (tmpMyDOB.AddYears(years).AddMonths(months) < tmpFutureDate)
      {
        months++;
        if (months > 12)
        {
          years++;
          months = months - 12;
        }
      }

      if (FutureDate.Day >= myDOB.Day)
      {
        days = days + FutureDate.Day - myDOB.Day;
      }
      else
      {
        months--;
        if (months < 0)
        {
          years--;
          months = months + 12;
        }
        days = days +
               (DateTime.DaysInMonth(FutureDate.AddMonths(-1).Year, FutureDate.AddMonths(-1).Month) + FutureDate.Day) -
               myDOB.Day;

      }

      //add an extra day if the dob is a leap day
      if (DateTime.IsLeapYear(myDOB.Year) && myDOB.Month == 2 && myDOB.Day == 29)
      {
        //but only if the future date is less than 1st March
        if (FutureDate >= new DateTime(FutureDate.Year, 3, 1))
          days++;
      }

    //-    return "Years: " + years + " Months: " + months + " Days: " + days;
      m_years = years;
      m_months = months;
      m_days = days;
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

  public class Jon_Test : DateDifferenceTests<Jon>
  {}
}
