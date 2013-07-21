// -- FILE ------------------------------------------------------------------
// name       : YearsTest.cs
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
	public sealed class YearsTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void YearBaseMonthTest()
		{
			DateTime moment = new DateTime( 2009, 2, 15 );
			int year = TimeTool.GetYearOf( YearMonth.April, moment.Year, moment.Month );
			Years years = new Years( moment, 3, TimeCalendar.New( YearMonth.April ) );
			Assert.AreEqual( years.YearBaseMonth, YearMonth.April );
			Assert.AreEqual( years.Start, new DateTime( year, (int)YearMonth.April, 1 ) );
		} // YearBaseMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void SingleYearsTest()
		{
			const int startYear = 2004;
			Years years = new Years( startYear, 1 );

			Assert.AreEqual( years.YearCount, 1 );
			Assert.AreEqual( years.StartYear, startYear );
			Assert.AreEqual( years.EndYear, startYear );
			Assert.AreEqual( years.GetYears().Count, 1 );
			Assert.IsTrue( years.GetYears()[ 0 ].IsSamePeriod( new Year( startYear ) ) );
		} // SingleYearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultCalendarYearsTest()
		{
			const int startYear = 2004;
			const int yearCount = 3;
			Years years = new Years( startYear, yearCount );

			Assert.AreEqual( years.YearCount, yearCount );
			Assert.AreEqual( years.StartYear, startYear );
			Assert.AreEqual( years.EndYear, startYear + yearCount - 1 );

			int index = 0;
			foreach ( Year year in years.GetYears() )
			{
				Assert.IsTrue( year.IsSamePeriod( new Year( startYear + index ) ) );
				index++;
			}
		} // DefaultCalendarYearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void CustomCalendarYearsTest()
		{
			const int startYear = 2004;
			const int yearCount = 3;
			const int startMonth = 4;
			Years years = new Years( startYear, yearCount, TimeCalendar.New( (YearMonth)startMonth ) );

			Assert.AreEqual( years.YearCount, yearCount );
			Assert.AreEqual( years.StartYear, startYear );
			Assert.AreEqual( years.EndYear, startYear + yearCount );

			int index = 0;
			foreach ( Year year in years.GetYears() )
			{
				Assert.AreEqual( year.Start, new DateTime( startYear + index, startMonth, 1 ) );
				index++;
			}
		} // CustomCalendarYearsTest

	} // class YearsTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
