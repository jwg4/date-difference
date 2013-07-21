using System;

namespace DateDifference
{
  public class ruffin : IDateDifference
  {
    private int intDiffInYears;
    private int intDiffInMonths;
    private int intDiffInDays;


    public void SetDates(DateTime start, DateTime end)
    {
      // Start of pasted code
      //- private void Form1_Load(object sender, EventArgs e) {
      DateTime dteThen = start;
      DateTime dteNow = end;

      //- int intDiffInYears = 0;
      //- int intDiffInMonths = 0;
      //- int intDiffInDays = 0;
      intDiffInYears = 0; //+
      intDiffInMonths = 0; //+
      intDiffInDays = 0; //+


      if (dteNow.Month >= dteThen.Month)
      {
        if (dteNow.Day >= dteThen.Day)
        {
          // this is a best case, easy subtraction situation
          intDiffInYears = dteNow.Year - dteThen.Year;
          intDiffInMonths = dteNow.Month - dteThen.Month;
          intDiffInDays = dteNow.Day - dteThen.Day;
        }
        else
        {
          // else we need to substract one from the month diff (borrow the one)
          // and days get wacky.

          // Watch for the outlier of Month = Month with DayNow < DayThen, as then we've 
          // got to subtract one from the year diff to borrow a month and have enough
          // days to subtract Then from Now.
          if (dteNow.Month == dteThen.Month)
          {
            intDiffInYears = dteNow.Year - dteThen.Year - 1;
            intDiffInMonths = 11; // we borrowed a year and broke ONLY 
            // the LAST month into subtractable days
            // Stay with me -- because we borrowed days from the year, not the month,
            // this is much different than what appears to be a similar calculation below.
            // We know we're a full intDiffInYears years apart PLUS eleven months.
            // Now we need to know how many days occurred before dteThen was done with 
            // dteThen.Month.  Then we add the number of days we've "earned" in the current
            // month.  
            //
            // So 12/25/2009 to 12/1/2011 gives us 
            // 11-9 = 2 years, minus one to borrow days = 1 year difference.
            // 1 year 11 months - 12 months = 11 months difference
            // (days from 12/25 to the End Of Month) + (Begin of Month to 12/1) = 
            //                (31-25)                +       (0+1)              =
            //                   6                   +         1                = 
            //                                  7 days diff
            //
            // 12/25/2009 to 12/1/2011 is 1 year, 11 months, 7 days apart.  QED.

            int intDaysInSharedMonth = System.DateTime.DaysInMonth(dteThen.Year, dteThen.Month);
            intDiffInDays = intDaysInSharedMonth - dteThen.Day + dteNow.Day;
          }
          else
          {
            intDiffInYears = dteNow.Year - dteThen.Year;
            intDiffInMonths = dteNow.Month - dteThen.Month - 1;

            // So now figure out how many more days we'd need to get from dteThen's 
            // intDiffInMonth-th month to get to the current month/day in dteNow.
            // That is, if we're comparing 2/8/2011 to 11/7/2011, we've got (10/8-2/8) = 8
            // full months between the two dates.  But then we've got to go from 10/8 to
            // 11/07.  So that's the previous month's (October) number of days (31) minus
            // the number of days into the month dteThen went (8), giving the number of days
            // needed to get us to the end of the month previous to dteNow (23).  Now we
            // add back the number of days that we've gone into dteNow's current month (7)
            // to get the total number of days we've gone since we ran the greatest integer
            // function on the month difference (23 to the end of the month + 7 into the
            // next month == 30 total days.  You gotta make it through October before you 
            // get another month, G, and it's got 31 days).

            int intDaysInPrevMonth = System.DateTime.DaysInMonth(dteNow.Year, (dteNow.Month - 1));
            intDiffInDays = intDaysInPrevMonth - dteThen.Day + dteNow.Day;
          }
        }
      }
      else
      {
        // else dteThen.Month > dteNow.Month, and we've got to amend our year subtraction
        // because we haven't earned our entire year yet, and don't want an obo error.
        intDiffInYears = dteNow.Year - dteThen.Year - 1;

        // So if the dates were THEN: 6/15/1999 and NOW: 2/20/2010...
        // Diff in years is 2010-1999 = 11, but since we're not to 6/15 yet, it's only 10.
        // Diff in months is (Months in year == 12) - (Months lost between 1/1/1999 and 6/15/1999
        // when dteThen's clock wasn't yet rolling == 6) = 6 months, then you add the months we
        // have made it into this year already.  The clock's been rolling through 2/20, so two months.
        // Note that if the 20 in 2/20 hadn't been bigger than the 15 in 6/15, we're back to the
        // intDaysInPrevMonth trick from earlier.  We'll do that below, too.
        intDiffInMonths = 12 - dteThen.Month + dteNow.Month;

        if (dteNow.Day >= dteThen.Day)
        {
          intDiffInDays = dteNow.Day - dteThen.Day;
        }
        else
        {
          intDiffInMonths--; // subtract the month from which we're borrowing days.

          // Maybe we shoulda factored this out previous to the if (dteNow.Month > dteThen.Month)
          // call, but I think this is more readable code.
          int intDaysInPrevMonth = System.DateTime.DaysInMonth(dteNow.Year, (dteNow.Month - 1));
          intDiffInDays = intDaysInPrevMonth - dteThen.Day + dteNow.Day;
        }

      }

      //- this.addToBox("Years: " + intDiffInYears + " Months: " + intDiffInMonths + " Days: " + intDiffInDays); // adds results to a rich text box.

      //- }      
      // End of pasted code
    }

    public int GetYears()
    {
      return intDiffInYears;
    }

    public int GetMonths()
    {
      return intDiffInMonths;
    }

    public int GetDays()
    {
      return intDiffInDays;
    }
  }

  public class ruffin_Test : DateDifferenceTests<ruffin>
  {
  }
}
