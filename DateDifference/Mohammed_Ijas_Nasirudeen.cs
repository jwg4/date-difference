using System;

namespace DateDifference
{
  public class Mohammed_Ijas_Nasirudeen : IDateDifference
  {
    private int agedisplay;
    private int lmonth;
    private int lday;

    public void SetDates(DateTime start, DateTime end)
    {

      // Start of pasted code
      //-      private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
      //-{

      //-        int gyear = dateTimePicker1.Value.Year; 
      //-        int gmonth = dateTimePicker1.Value.Month; 
      //-        int gday = dateTimePicker1.Value.Day; 
      int gyear = start.Year; //+
      int gmonth = start.Month; //+
      int gday = start.Day; //+
      //-        int syear = DateTime.Now.Year; 
      //-        int smonth = DateTime.Now.Month; 
      //-        int sday = DateTime.Now.Day;
      int syear = end.Year; //+
      int smonth = end.Month; //+
      int sday = end.Day; //+

      int difday = DateTime.DaysInMonth(syear, gmonth);

      agedisplay = (syear - gyear);

      lmonth = (smonth - gmonth);
      lday = (sday - gday);


      if (smonth < gmonth)
      {
        agedisplay = agedisplay - 1;
      }
      if (smonth == gmonth)
      {
        if (sday < (gday))
        {
          agedisplay = agedisplay - 1;
        }
      }

      if (smonth < gmonth)
      {
        lmonth = (-(-smonth) + (-gmonth) + 12);
      }
      if (lday < 0)
      {
        lday = difday - (-lday);
        lmonth = lmonth - 1;
      }

      if (smonth == gmonth && sday < gday && gyear != syear)
      {
        lmonth = 11;
      }

      //-    ageDisplay.Text = Convert.ToString(agedisplay) + " Years,  " + lmonth + " Months,  " + lday + " Days.";

      //-}
      // End of pasted code

    }

    public int GetYears()
    {
      return agedisplay;
    }

    public int GetMonths()
    {
      return lmonth;
    }

    public int GetDays()
    {
      return lday;
    }
  }

  public class Mohammed_Ijas_Nasirudeen_Test : DateDifferenceTests<Mohammed_Ijas_Nasirudeen>
  {
  }

}