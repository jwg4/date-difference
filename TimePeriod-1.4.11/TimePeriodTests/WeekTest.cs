// -- FILE ------------------------------------------------------------------
// name       : WeekTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;
using Itenso.TimePeriod;
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
	public sealed class WeekTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultCalendarTest()
		{
			const int startWeek = 1;
			int currentYear = ClockProxy.Clock.Now.Year;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				foreach ( YearWeekType yearWeekType in Enum.GetValues( typeof( YearWeekType ) ) )
				{
					int weeksOfYear = TimeTool.GetWeeksOfYear( currentYear, culture, yearWeekType );
					for ( int weekOfYear = startWeek; weekOfYear < weeksOfYear; weekOfYear++ )
					{
						Week week = new Week( currentYear, weekOfYear, TimeCalendar.New( culture, YearMonth.January, yearWeekType ) );
						Assert.AreEqual( week.Year, currentYear );

						DateTime weekStart = TimeTool.GetStartOfYearWeek( currentYear, weekOfYear, culture, yearWeekType );
						DateTime weekEnd = weekStart.AddDays( TimeSpec.DaysPerWeek );
						Assert.AreEqual( week.Start, weekStart.Add( week.Calendar.StartOffset ) );
						Assert.AreEqual( week.End, weekEnd.Add( week.Calendar.EndOffset ) );
					}
				}
			}
		} // DefaultCalendarTest

		// ----------------------------------------------------------------------
		[Test]
		public void EnAuCultureTest()
		{
			CultureInfo cultureInfo = new CultureInfo( "en-AU" );
			//	cultureInfo.DateTimeFormat.CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek;
			TimeCalendar calendar = new TimeCalendar( new TimeCalendarConfig { Culture = cultureInfo } );
			Week week = new Week( new DateTime( 2011, 4, 1, 9, 0, 0 ), calendar );
			Assert.AreEqual( week.Start, new DateTime( 2011, 3, 28 ) );
		} // EnAuCultureTest

		// ----------------------------------------------------------------------
		[Test]
		public void DanishUsCultureTest()
		{
			CultureInfo danishCulture = new CultureInfo( "da-DK" );
			Week danishWeek = new Week( 2011, 36, new TimeCalendar( new TimeCalendarConfig { Culture = danishCulture } ) );
			Assert.AreEqual( danishWeek.Start.Date, new DateTime( 2011, 9, 5 ) );
			Assert.AreEqual( danishWeek.End.Date, new DateTime( 2011, 9, 11 ) );

			CultureInfo usCulture = new CultureInfo( "en-US" );
			usCulture.DateTimeFormat.CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek;
			Week usWeek = new Week( 2011, 36, new TimeCalendar( new TimeCalendarConfig { Culture = usCulture } ) );
			Assert.AreEqual( usWeek.Start.Date, new DateTime( 2011, 9, 4 ) );
			Assert.AreEqual( usWeek.End.Date, new DateTime( 2011, 9, 10 ) );
		} // DanishUsCultureTest

	} // class WeekTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
