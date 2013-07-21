// -- FILE ------------------------------------------------------------------
// name       : HalfyearsTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
	public sealed class HalfyearsTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void YearBaseMonthTest()
		{
			DateTime moment = new DateTime( 2009, 2, 15 );
			int year = TimeTool.GetYearOf( YearMonth.April, moment.Year, moment.Month );
			Halfyears halfyears = new Halfyears( moment, YearHalfyear.First, 3, TimeCalendar.New( YearMonth.April ) );
			Assert.AreEqual( halfyears.YearBaseMonth, YearMonth.April );
			Assert.AreEqual( halfyears.Start, new DateTime( year, (int)YearMonth.April, 1 ) );
		} // YearBaseMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void SingleHalfyearsTest()
		{
			const int startYear = 2004;
			const YearHalfyear startHalfyear = YearHalfyear.Second;
			Halfyears halfyears = new Halfyears( startYear, startHalfyear, 1 );

			Assert.AreEqual( halfyears.YearBaseMonth, YearMonth.January );
			Assert.AreEqual( halfyears.HalfyearCount, 1 );
			Assert.AreEqual( halfyears.StartHalfyear, startHalfyear );
			Assert.AreEqual( halfyears.StartYear, startYear );
			Assert.AreEqual( halfyears.EndYear, startYear );
			Assert.AreEqual( halfyears.EndHalfyear, YearHalfyear.Second );
			Assert.AreEqual( halfyears.GetHalfyears().Count, 1 );
			Assert.IsTrue( halfyears.GetHalfyears()[ 0 ].IsSamePeriod( new Halfyear( 2004, YearHalfyear.Second ) ) );
		} // SingleHalfyearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void FirstCalendarHalfyearsTest()
		{
			const int startYear = 2004;
			const YearHalfyear startHalfyear = YearHalfyear.First;
			const int halfyearCount = 3;
			Halfyears halfyears = new Halfyears( startYear, startHalfyear, halfyearCount );

			Assert.AreEqual( halfyears.YearBaseMonth, YearMonth.January );
			Assert.AreEqual( halfyears.HalfyearCount, halfyearCount );
			Assert.AreEqual( halfyears.StartHalfyear, startHalfyear );
			Assert.AreEqual( halfyears.StartYear, startYear );
			Assert.AreEqual( halfyears.EndYear, 2005 );
			Assert.AreEqual( halfyears.EndHalfyear, YearHalfyear.First );
			Assert.AreEqual( halfyears.GetHalfyears().Count, halfyearCount );
			Assert.IsTrue( halfyears.GetHalfyears()[ 0 ].IsSamePeriod( new Halfyear( 2004, YearHalfyear.First ) ) );
			Assert.IsTrue( halfyears.GetHalfyears()[ 1 ].IsSamePeriod( new Halfyear( 2004, YearHalfyear.Second ) ) );
			Assert.IsTrue( halfyears.GetHalfyears()[ 2 ].IsSamePeriod( new Halfyear( 2005, YearHalfyear.First ) ) );
		} // FirstCalendarHalfyearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void SecondCalendarHalfyearsTest()
		{
			const int startYear = 2004;
			const YearHalfyear startHalfyear = YearHalfyear.Second;
			const int halfyearCount = 3;
			Halfyears halfyears = new Halfyears( startYear, startHalfyear, halfyearCount );

			Assert.AreEqual( halfyears.YearBaseMonth, YearMonth.January );
			Assert.AreEqual( halfyears.HalfyearCount, halfyearCount );
			Assert.AreEqual( halfyears.StartHalfyear, startHalfyear );
			Assert.AreEqual( halfyears.StartYear, startYear );
			Assert.AreEqual( halfyears.EndYear, 2005 );
			Assert.AreEqual( halfyears.EndHalfyear, YearHalfyear.Second );
			Assert.AreEqual( halfyears.GetHalfyears().Count, halfyearCount );
			Assert.IsTrue( halfyears.GetHalfyears()[ 0 ].IsSamePeriod( new Halfyear( 2004, YearHalfyear.Second ) ) );
			Assert.IsTrue( halfyears.GetHalfyears()[ 1 ].IsSamePeriod( new Halfyear( 2005, YearHalfyear.First ) ) );
			Assert.IsTrue( halfyears.GetHalfyears()[ 2 ].IsSamePeriod( new Halfyear( 2005, YearHalfyear.Second ) ) );
		} // SecondCalendarHalfyearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void FirstCustomCalendarHalfyearsTest()
		{
			TimeCalendar calendar = TimeCalendar.New( YearMonth.October );
			const int startYear = 2004;
			const YearHalfyear startHalfyear = YearHalfyear.First;
			const int halfyearCount = 3;
			Halfyears halfyears = new Halfyears( startYear, startHalfyear, halfyearCount, calendar );

			Assert.AreEqual( halfyears.YearBaseMonth, YearMonth.October );
			Assert.AreEqual( halfyears.HalfyearCount, halfyearCount );
			Assert.AreEqual( halfyears.StartHalfyear, startHalfyear );
			Assert.AreEqual( halfyears.StartYear, startYear );
			Assert.AreEqual( halfyears.EndYear, 2005 );
			Assert.AreEqual( halfyears.EndHalfyear, YearHalfyear.First );
			Assert.AreEqual( halfyears.GetHalfyears().Count, halfyearCount );
			Assert.IsTrue( halfyears.GetHalfyears()[ 0 ].IsSamePeriod( new Halfyear( 2004, YearHalfyear.First, calendar ) ) );
			Assert.IsTrue( halfyears.GetHalfyears()[ 1 ].IsSamePeriod( new Halfyear( 2004, YearHalfyear.Second, calendar ) ) );
			Assert.IsTrue( halfyears.GetHalfyears()[ 2 ].IsSamePeriod( new Halfyear( 2005, YearHalfyear.First, calendar ) ) );
		} // FirstCustomCalendarHalfyearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void SecondCustomCalendarHalfyearsTest()
		{
			TimeCalendar calendar = TimeCalendar.New( YearMonth.October );
			const int startYear = 2004;
			const YearHalfyear startHalfyear = YearHalfyear.Second;
			const int halfyearCount = 3;
			Halfyears halfyears = new Halfyears( startYear, startHalfyear, halfyearCount, calendar );

			Assert.AreEqual( halfyears.YearBaseMonth, YearMonth.October );
			Assert.AreEqual( halfyears.HalfyearCount, halfyearCount );
			Assert.AreEqual( halfyears.StartHalfyear, startHalfyear );
			Assert.AreEqual( halfyears.StartYear, startYear );
			Assert.AreEqual( halfyears.EndYear, 2005 );
			Assert.AreEqual( halfyears.EndHalfyear, YearHalfyear.Second );
			Assert.AreEqual( halfyears.GetHalfyears().Count, halfyearCount );
			Assert.IsTrue( halfyears.GetHalfyears()[ 0 ].IsSamePeriod( new Halfyear( 2004, YearHalfyear.Second, calendar ) ) );
			Assert.IsTrue( halfyears.GetHalfyears()[ 1 ].IsSamePeriod( new Halfyear( 2005, YearHalfyear.First, calendar ) ) );
			Assert.IsTrue( halfyears.GetHalfyears()[ 2 ].IsSamePeriod( new Halfyear( 2005, YearHalfyear.Second, calendar ) ) );
		} // SecondCustomCalendarHalfyearsTest

	} // class HalfyearsTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
