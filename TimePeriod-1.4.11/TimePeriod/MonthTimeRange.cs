// -- FILE ------------------------------------------------------------------
// name       : MonthTimeRange.cs
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
	public abstract class MonthTimeRange : CalendarTimeRange
	{

		// ----------------------------------------------------------------------
		protected MonthTimeRange( int startYear, YearMonth startMonth, int monthCounth ) :
			this( startYear, startMonth, monthCounth, new TimeCalendar() )
		{
		} // MonthTimeRange

		// ----------------------------------------------------------------------
		protected MonthTimeRange( int startYear, YearMonth startMonth, int monthCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( startYear, startMonth, monthCount ), calendar )
		{
			this.startYear = startYear;
			this.startMonth = startMonth;
			this.monthCount = monthCount;
			TimeTool.AddMonth( startYear, startMonth, monthCount - 1, out endYear, out endMonth );
		} // MonthTimeRange

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
		public YearMonth StartMonth
		{
			get { return startMonth; }
		} // StartMonth

		// ----------------------------------------------------------------------
		public YearMonth EndMonth
		{
			get { return endMonth; }
		} // EndMonth

		// ----------------------------------------------------------------------
		public int MonthCount
		{
			get { return monthCount; }
		} // MonthCount

		// ----------------------------------------------------------------------
		public string StartMonthName
		{
			get { return Calendar.GetMonthName( (int)StartMonth ); }
		} // StartMonthName

		// ----------------------------------------------------------------------
		public string StartMonthOfYearName
		{
			get { return Calendar.GetMonthOfYearName( StartYear, (int)StartMonth ); }
		} // StartMonthOfYearName

		// ----------------------------------------------------------------------
		public string EndMonthName
		{
			get { return Calendar.GetMonthName( (int)EndMonth ); }
		} // EndMonthName

		// ----------------------------------------------------------------------
		public string EndMonthOfYearName
		{
			get { return Calendar.GetMonthOfYearName( EndYear, (int)EndMonth ); }
		} // EndMonthOfYearName

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetDays()
		{
			TimePeriodCollection days = new TimePeriodCollection();
			DateTime startDate = new DateTime( startYear, (int)startMonth, 1 );
			for ( int month = 0; month < monthCount; month++ )
			{
				DateTime monthStart = startDate.AddMonths( month );
				int daysOfMonth = TimeTool.GetDaysInMonth( monthStart.Year, monthStart.Month );
				for ( int day = 0; day < daysOfMonth; day++ )
				{
					days.Add( new Day( monthStart.AddDays( day ), Calendar ) );
				}
			}
			return days;
		} // GetDays

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as MonthTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( MonthTimeRange comp )
		{
			return 
				startYear == comp.startYear &&
				startMonth == comp.startMonth &&
				monthCount == comp.monthCount &&
				endYear == comp.endYear &&
				endMonth == comp.endMonth;
		} // HasSameData

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), startYear, startMonth, monthCount, endYear, endMonth );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( int year, YearMonth yearMonth, int monthCount )
		{
			if ( monthCount < 1 )
			{
				throw new ArgumentOutOfRangeException( "monthCount" );
			}

			DateTime start = new DateTime( year, (int)yearMonth, 1 );
			DateTime end = start.AddMonths( monthCount );
			return new TimeRange( start, end );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly int startYear;
		private readonly YearMonth startMonth;
		private readonly int monthCount;
		private readonly int endYear; // cache
		private readonly YearMonth endMonth; // cache

	} // class MonthTimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
