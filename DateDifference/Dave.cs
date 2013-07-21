using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DateDifference
{
  public class Dave : IDateDifference
  {
    private DateTime endDate;
    private DateTime startDate;

    public void SetDates(DateTime start, DateTime end)
    {
      startDate = start;
      endDate = end;
    }

    public int GetYears()
    {
      return GetMonths() / 12;  
    }

    public int GetMonths()
    {
      // Start of pasted code
      //-   public int getMonths(DateTime startDate, DateTime endDate)
      //-{
      int months = 0;

      if (endDate.Month <= startDate.Month)
      {
        if (endDate.Day < startDate.Day)
        {
          months = (12*(endDate.Year - startDate.Year - 1))
                   + (12 - startDate.Month + endDate.Month - 1);
        }
        else if (endDate.Month < startDate.Month)
        {
          months = (12*(endDate.Year - startDate.Year - 1))
                   + (12 - startDate.Month + endDate.Month);
        }
        else // (endDate.Month == startDate.Month) && (endDate.Day >= startDate.Day)
        {
          months = (12*(endDate.Year - startDate.Year));
        }
      }
      else if (endDate.Day < startDate.Day)
      {
        months = (12*(endDate.Year - startDate.Year))
                 + (endDate.Month - startDate.Month) - 1;
      }
      else // (endDate.Month > startDate.Month) && (endDate.Day >= startDate.Day)
      {
        months = (12*(endDate.Year - startDate.Year))
                 + (endDate.Month - startDate.Month);
      }

      return months;
    }
    // End of pasted code

    public int GetDays()
    {
      return (endDate - startDate).Days;
    }
  }

  public class Dave_Test : DateDifferenceTests<Dave>
  {
  }
}
