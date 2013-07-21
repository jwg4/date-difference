// -- FILE ------------------------------------------------------------------
// name       : YearTimeRange.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public abstract class YearTimeRange : YearCalendarTimeRange
	{

		// ----------------------------------------------------------------------
		protected YearTimeRange( int startYear, int yearCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( calendar.YearBaseMonth, startYear, yearCount ), calendar )
		{
			this.startYear = startYear;
			this.yearCount = yearCount;
			endYear = End.Year;
		} // YearTimeRange

		// ----------------------------------------------------------------------
		public int YearCount
		{
			get { return yearCount; }
		} // YearCount

		// ----------------------------------------------------------------------
		public override int BaseYear
		{
			get { return StartYear; }
		} // BaseYear

		// ----------------------------------------------------------------------
		public int StartYear
		{
			get { return startYear; }
		} // StartYear

		// ----------------------------------------------------------------------
		public int EndYear
		{
			get { return endYear; }
		} // EndYear

		// ----------------------------------------------------------------------
		public string StartYearName
		{
			get { return Calendar.GetYearName( StartYear ); }
		} // StartYearName

		// ----------------------------------------------------------------------
		public string EndYearName
		{
			get { return Calendar.GetYearName( EndYear ); }
		} // EndYearName

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetHalfyears()
		{
			TimePeriodCollection halfyears = new TimePeriodCollection();
			for ( int i = 0; i < yearCount; i++ )
			{
				for ( int halfyear = 0; halfyear < TimeSpec.HalfyearsPerYear; halfyear++ )
				{
					halfyears.Add( new Halfyear( startYear + i, (YearHalfyear)( halfyear + 1 ), Calendar ) );
				}
			}
			return halfyears;
		} // GetHalfyears

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetQuarters()
		{
			TimePeriodCollection quarters = new TimePeriodCollection();
			for ( int i = 0; i < yearCount; i++ )
			{
				for ( int quarter = 0; quarter < TimeSpec.QuartersPerYear; quarter++ )
				{
					quarters.Add( new Quarter( startYear + i, (YearQuarter)( quarter + 1 ), Calendar ) );
				}
			}
			return quarters;
		} // GetQuarters

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetMonths()
		{
			TimePeriodCollection months = new TimePeriodCollection();
			DateTime startDate = new DateTime( startYear, (int)YearBaseMonth, 1 );
			int monthCount = YearCount * TimeSpec.MonthsPerYear;
			for ( int i = 0; i < monthCount; i++ )
			{
				months.Add( new Month( startDate.AddMonths( i ), Calendar ) );
			}
			return months;
		} // GetMonths

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as YearTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( YearTimeRange comp )
		{
			return 
				startYear == comp.startYear &&
				endYear == comp.endYear &&
				yearCount == comp.yearCount;
		} // HasSameData

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), startYear, startYear, yearCount );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( YearMonth yearMonth, int year, int yearCount )
		{
			if ( yearCount < 1 )
			{
				throw new ArgumentOutOfRangeException( "yearCount" );
			}
			DateTime start = new DateTime( year, (int)yearMonth, 1 );
			DateTime end = start.AddYears( yearCount );
			return new TimeRange( start, end );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly int startYear;
		private readonly int yearCount;
		private readonly int endYear; // cache

	} // class YearTimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
