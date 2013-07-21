using System;

namespace DateDifference
{
  public class ho1 : IDateDifference
  {
    private int m_years;
    private int m_months;
    private int m_days;

    public void SetDates(DateTime start, DateTime end)
    {
      // start of pasted code
      //- DateTime bd = DateTime.Parse("2009-06-17");
      DateTime bd = start; //+
      //- TimeSpan ts = DateTime.Now.Subtract(bd);
      TimeSpan ts = end.Subtract(bd);
      DateTime age = DateTime.MinValue + ts;
      //string s = string.Format("{0} Years {1} months {2} days", age.Year - 1, age.Month - 1, age.Day - 1);
      m_years = age.Year - 1; //+
      m_months = age.Month - 1; //+
      m_days = age.Day - 1; //+
      // end of pasted code
    }

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

  public class ho1_Test : DateDifferenceTests<ho1>
  {
  }
}
