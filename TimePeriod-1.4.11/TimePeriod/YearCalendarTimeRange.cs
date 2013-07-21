// -- FILE ------------------------------------------------------------------
// name       : YearCalendarTimeRange.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public abstract class YearCalendarTimeRange : CalendarTimeRange
	{

		// ----------------------------------------------------------------------
		protected YearCalendarTimeRange( ITimePeriod period, ITimeCalendar calendar ) :
			base( period, calendar )
		{
		} // YearCalendarTimeRange

		// ----------------------------------------------------------------------
		public YearMonth YearBaseMonth
		{
			get { return Calendar.YearBaseMonth; }
		} // YearBaseMonth

		// ----------------------------------------------------------------------
		public abstract int BaseYear { get; }

	} // class YearCalendarTimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
