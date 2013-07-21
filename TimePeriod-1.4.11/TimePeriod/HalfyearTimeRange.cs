// -- FILE ------------------------------------------------------------------
// name       : HalfyearTimeRange.cs
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
	public abstract class HalfyearTimeRange : YearCalendarTimeRange
	{

		// ----------------------------------------------------------------------
		protected HalfyearTimeRange( int startYear, YearHalfyear startHalfyear, int halfyearCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( calendar.YearBaseMonth, startYear, startHalfyear, halfyearCount ), calendar )
		{
			this.startYear = startYear;
			this.startHalfyear = startHalfyear;
			this.halfyearCount = halfyearCount;
			TimeTool.AddHalfyear( startYear, startHalfyear, halfyearCount - 1, out endYear, out endHalfyear );
		} // HalfyearTimeRange

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
		public YearHalfyear StartHalfyear
		{
			get { return startHalfyear; }
		} // StartHalfyear

		// ----------------------------------------------------------------------
		public YearHalfyear EndHalfyear
		{
			get { return endHalfyear; }
		} // EndHalfyear

		// ----------------------------------------------------------------------
		public int HalfyearCount
		{
			get { return halfyearCount; }
		} // HalfyearCount

		// ----------------------------------------------------------------------
		public string StartHalfyearName
		{
			get { return Calendar.GetHalfyearName( StartHalfyear ); }
		} // StartHalfyearName

		// ----------------------------------------------------------------------
		public string StartHalfyearOfYearName
		{
			get { return Calendar.GetHalfyearOfYearName( StartYear, StartHalfyear ); }
		} // StartHalfyearOfYearName

		// ----------------------------------------------------------------------
		public string EndHalfyearName
		{
			get { return Calendar.GetHalfyearName( EndHalfyear ); }
		} // EndHalfyearName

		// ----------------------------------------------------------------------
		public string EndHalfyearOfYearName
		{
			get { return Calendar.GetHalfyearOfYearName( EndYear, EndHalfyear ); }
		} // EndHalfyearOfYearName

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetQuarters()
		{
			TimePeriodCollection quarters = new TimePeriodCollection();
			int quarterCount = HalfyearCount * TimeSpec.QuartersPerHalfyear;
			int startQuarter = ((int)startHalfyear - 1 ) * TimeSpec.QuartersPerHalfyear;
			for ( int quarter = 0; quarter < quarterCount; quarter++ )
			{
				int targetQuarter = startQuarter + quarter;
				int year = BaseYear + ( targetQuarter / 4 );
				quarters.Add( new Quarter( year, (YearQuarter)( targetQuarter + 1 ), Calendar ) );
			}
			return quarters;
		} // GetQuarters

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetMonths()
		{
			TimePeriodCollection months = new TimePeriodCollection();
			DateTime startDate = new DateTime( startYear, (int)YearBaseMonth, 1 );
			int monthCount = halfyearCount * TimeSpec.MonthsPerHalfyear;
			for ( int i = 0; i < monthCount; i++ )
			{
				months.Add( new Month( startDate.AddMonths( i ), Calendar ) );
			}
			return months;
		} // GetMonths

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as HalfyearTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( HalfyearTimeRange comp )
		{
			return
				startYear == comp.startYear &&
				startHalfyear == comp.startHalfyear &&
				halfyearCount == comp.halfyearCount &&
				endYear == comp.endYear &&
				endHalfyear == comp.endHalfyear; 
		} // HasSameData

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), startYear, startHalfyear, halfyearCount, endYear, endHalfyear );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( YearMonth yearMonth, int startYear, YearHalfyear startHalfyear, int halfyearCount )
		{
			if ( halfyearCount < 1 )
			{
				throw new ArgumentOutOfRangeException( "halfyearCount" );
			}

			DateTime yearStart = new DateTime( startYear, (int)yearMonth, 1 );
			DateTime start = yearStart.AddMonths( ( (int)startHalfyear - 1 ) * TimeSpec.MonthsPerHalfyear );
			DateTime end = start.AddMonths( halfyearCount * TimeSpec.MonthsPerHalfyear );
			return new TimeRange( start, end );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly int startYear;
		private readonly YearHalfyear startHalfyear;
		private readonly int halfyearCount;
		private readonly int endYear; // cache
		private readonly YearHalfyear endHalfyear; // cache

	} // class HalfyearTimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
