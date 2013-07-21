using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DateDifference
{
  public class Rajeshwaran_S_P : IDateDifference
  {
    private int ageInDays;
    private int ageInYears;
    private int ageInMonths;

    public void SetDates(DateTime start, DateTime end)
    {
      // start of pasted code
      //- DateTime dateOfBirth = new DateTime(2000, 4, 18);
      DateTime dateOfBirth = start; //+
      //- DateTime currentDate = DateTime.Now;
      DateTime currentDate = end; //+

      //- int ageInYears = 0;
      //- int ageInMonths = 0;
      //- int ageInDays = 0;
      ageInYears = 0; //+
      ageInMonths = 0; //+
      ageInDays = 0; //+

      ageInDays = currentDate.Day - dateOfBirth.Day;
      ageInMonths = currentDate.Month - dateOfBirth.Month;
      ageInYears = currentDate.Year - dateOfBirth.Year;

      if (ageInDays < 0)
      {
        ageInDays += DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
        ageInMonths = ageInMonths--;

        if (ageInMonths < 0)
        {
          ageInMonths += 12;
          ageInYears--;
        }
      }
      if (ageInMonths < 0)
      {
        ageInMonths += 12;
        ageInYears--;
      }

      //- Console.WriteLine("{0}, {1}, {2}", ageInYears, ageInMonths, ageInDays);
      // end of pasted code
    }

    public int GetYears()
    {
      return ageInYears;
    }

    public int GetMonths()
    {
      return ageInMonths;
    }

    public int GetDays()
    {
      return ageInDays;
    }
  }

  public class Rajeshwaran_S_P_Test : DateDifferenceTests<Rajeshwaran_S_P>
  {
  }

}
