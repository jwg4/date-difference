using System;

namespace DateDifference
{
  public class Malu_MN : IDateDifference
  {
    private int yearDiff;
    private int daysDiff;
    private int monthDiff;


    public void SetDates(DateTime start, DateTime end)
    {
      // Start of pasted code
      //- DateTime todaysDate = DateTime.Now;
      //- DateTime interimDate = originalDate;
      DateTime todaysDate = end; //+
      DateTime interimDate = start; //+

      ///Find Year diff
      //- int yearDiff = System.Data.Linq.SqlClient.SqlMethods.DateDiffYear(interimDate, todaysDate);
      yearDiff = System.Data.Linq.SqlClient.SqlMethods.DateDiffYear(interimDate, todaysDate); //+
      interimDate = interimDate.AddYears(yearDiff);
      if (interimDate > todaysDate)
      {
        yearDiff -= 1;
        interimDate = interimDate.AddYears(-1);
      }

      ///Find Month diff
      //- int monthDiff = System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(interimDate, todaysDate);
      monthDiff = System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(interimDate, todaysDate); //+
      interimDate = interimDate.AddMonths(monthDiff);
      if (interimDate > todaysDate)
      {
        monthDiff -= 1;
        interimDate = interimDate.AddMonths(-1);
      }

      ///Find Day diff
      //- int daysDiff = System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(interimDate, todaysDate); throw new NotImplementedException();
      daysDiff = System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(interimDate, todaysDate); //+
      // End of pasted code
    }

    public int GetYears()
    {
      return yearDiff;
    }

    public int GetMonths()
    {
      return monthDiff;
    }

    public int GetDays()
    {
      return daysDiff;
    }
  }

  public class Malu_MN_Test : DateDifferenceTests<Malu_MN>
  {
  }
}
