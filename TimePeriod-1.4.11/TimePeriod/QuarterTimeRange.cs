// -- FILE ------------------------------------------------------------------
// name       : QuarterTimeRange.cs
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
	public abstract class QuarterTimeRange : YearCalendarTimeRange
	{

		// ----------------------------------------------------------------------
		protected QuarterTimeRange( int startYear, YearQuarter startQuarter, int quarterCount ) :
			this( startYear, startQuarter, quarterCount, new TimeCalendar() )
		{
		} // QuarterTimeRange

		// ----------------------------------------------------------------------
		protected QuarterTimeRange( int startYear, YearQuarter startQuarter, int quarterCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( calendar.YearBaseMonth, startYear, startQuarter, quarterCount ), calendar )
		{
			this.startYear = startYear;
			this.startQuarter = startQuarter;
			this.quarterCount = quarterCount;
			TimeTool.AddQuarter( startYear, startQuarter, quarterCount - 1, out endYear, out endQuarter );
		} // QuarterTimeRange

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
		public YearQuarter StartQuarter
		{
			get { return startQuarter; }
		} // StartQuarter

		// ----------------------------------------------------------------------
		public YearQuarter EndQuarter
		{
			get { return endQuarter; }
		} // EndQuarter

		// ----------------------------------------------------------------------
		public int QuarterCount
		{
			get { return quarterCount; }
		} // QuarterCount

		// ----------------------------------------------------------------------
		public string StartQuarterName
		{
			get { return Calendar.GetQuarterName( StartQuarter ); }
		} // StartQuarterName

		// ----------------------------------------------------------------------
		public string StartQuarterOfYearName
		{
			get { return Calendar.GetQuarterOfYearName( StartYear, StartQuarter ); }
		} // StartQuarterOfYearName

		// ----------------------------------------------------------------------
		public string EndQuarterName
		{
			get { return Calendar.GetQuarterName( EndQuarter ); }
		} // EndQuarterName

		// ----------------------------------------------------------------------
		public string EndQuarterOfYearName
		{
			get { return Calendar.GetQuarterOfYearName( EndYear, EndQuarter ); }
		} // EndQuarterOfYearName

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetMonths()
		{
			TimePeriodCollection months = new TimePeriodCollection();
			DateTime startDate = new DateTime( startYear, (int)YearBaseMonth, 1 );
			int monthCount = quarterCount * TimeSpec.MonthsPerQuarter;
			for ( int i = 0; i < monthCount; i++ )
			{
				months.Add( new Month( startDate.AddMonths( i ), Calendar ) );
			}
			return months;
		} // GetMonths

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as QuarterTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( QuarterTimeRange comp )
		{
			return 
				startYear == comp.startYear &&
				startQuarter == comp.startQuarter &&
				quarterCount == comp.quarterCount &&
				endYear == comp.endYear &&
				endQuarter == comp.endQuarter;
		} // HasSameData

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), startYear, startQuarter, quarterCount, endYear, endQuarter );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( YearMonth yearMonth, int year, YearQuarter yearQuarter, int quarterCount )
		{
			if ( quarterCount < 1 )
			{
				throw new ArgumentOutOfRangeException( "quarterCount" );
			}

			DateTime yearStart = new DateTime( year, (int)yearMonth, 1 );
			DateTime start = yearStart.AddMonths( ( (int)yearQuarter - 1 ) * TimeSpec.MonthsPerQuarter );
			DateTime end = start.AddMonths( quarterCount * TimeSpec.MonthsPerQuarter );
			return new TimeRange( start, end );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly int startYear;
		private readonly YearQuarter startQuarter;
		private readonly int quarterCount;
		private readonly int endYear; // cache
		private readonly YearQuarter endQuarter; // cache

	} // class QuarterTimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
