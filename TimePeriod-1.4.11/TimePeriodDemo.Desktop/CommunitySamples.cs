// -- FILE ------------------------------------------------------------------
// name       : CommunitySamples.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class UtcTimeRange : TimeRange
	{

		// ----------------------------------------------------------------------
		public UtcTimeRange( DateTimeOffset utcStart, DateTimeOffset utcEnd, bool isReadOnly = false ) :
			base( utcStart.UtcDateTime, utcEnd.UtcDateTime, isReadOnly )
		{
			if ( utcStart <= utcEnd )
			{
				this.utcStart = utcStart;
				this.utcEnd = utcEnd;
			}
			else
			{
				this.utcEnd = utcStart;
				this.utcStart = utcEnd;
			}
		} // UtcTimeRange

		// ----------------------------------------------------------------------
		public DateTimeOffset UtcStart
		{
			get { return utcStart; }
			set
			{
				if ( value > utcEnd )
				{
					throw new ArgumentOutOfRangeException( "value" );
				}
				utcStart = value;
				Start = utcStart.UtcDateTime;
			}
		} // UtcStart

		// ----------------------------------------------------------------------
		public DateTimeOffset UtcEnd
		{
			get { return utcEnd; }
			set
			{
				if ( value < utcStart )
				{
					throw new ArgumentOutOfRangeException( "value" );
				}
				utcEnd = value;
				End = utcEnd.UtcDateTime;
			}
		} // UtcEnd

		// ----------------------------------------------------------------------
		public void Setup( DateTimeOffset newStart, DateTimeOffset newEnd )
		{
			base.Setup( newStart.UtcDateTime, newEnd.UtcDateTime );
			if ( newStart <= newEnd )
			{
				utcStart = newStart;
				utcEnd = newEnd;
			}
			else
			{
				utcEnd = newStart;
				utcStart = newEnd;
			}
		} // Setup

		// ----------------------------------------------------------------------
		// members
		private DateTimeOffset utcStart;
		private DateTimeOffset utcEnd;

	} // UtcTimeRange

	// ------------------------------------------------------------------------
	public class CommunitySamples
	{

		#region DateTimeOffset

		[Sample( "DateTimeOffsetSample" )]
		// ----------------------------------------------------------------------
		public void DateTimeOffsetSample()
		{
			DateTime start1 = new DateTime( 2012, 3, 26, 14, 0, 0 );
			DateTime end1 = new DateTime( 2012, 3, 26, 20, 0, 0 );
			Console.WriteLine( "Difference 1: " + ( end1 - start1 ) );
			TimeRange timeRange1 = new TimeRange( start1, end1 );
			Console.WriteLine( "Duration 1: " + timeRange1.Duration );

			DateTimeOffset start2 = new DateTimeOffset( 2012, 3, 26, 14, 0, 0, 0, TimeSpan.Zero );
			DateTimeOffset end2 = new DateTimeOffset( 2012, 3, 26, 20, 0, 0, 0, new TimeSpan( 2, 0, 0 ) );
			Console.WriteLine( "Difference 2: " + ( end2 - start2 ) );
			UtcTimeRange timeRange2 = new UtcTimeRange( start2, end2 );
			Console.WriteLine( "Duration 2: " + timeRange2.Duration );
		} // DateTimeOffsetSample

		#endregion

		#region DateTimeSet

		[Sample( "DateTimeSet" )]
		// ----------------------------------------------------------------------
		public void DateTimeSetSample()
		{
			DateTimeSet moments = new DateTimeSet();

			// --- add ---
			moments.Add( new DateTime( 2012, 8, 10, 18, 15, 0 ) );
			moments.Add( new DateTime( 2012, 8, 10, 15, 0, 0 ) );
			moments.Add( new DateTime( 2012, 8, 10, 13, 30, 0 ) );
			moments.Add( new DateTime( 2012, 8, 10, 15, 0, 0 ) ); // twice -> ignored
			Console.WriteLine( "DateTimeSet.Add(): " + moments );
			// > DateTimeSet.Add(): Count = 3; 10.08.2012 13:30:00 - 18:15:00 | 0.04:45
			for ( int i = 0; i < moments.Count; i++ )
			{
				Console.WriteLine( "Moment[{0:0}]: {1}", i, moments[ i ] );
			}
			// > Moment[0]: 10.08.2012 13:30:00
			// > Moment[1]: 10.08.2012 15:00:00
			// > Moment[2]: 10.08.2012 18:15:00

			// --- durations ---
			IList<TimeSpan> durations = moments.GetDurations( 0, moments.Count );
			Console.WriteLine( "DateTimeSet.GetDurations() " );
			for ( int i = 0; i < durations.Count; i++ )
			{
				Console.WriteLine( "Duration[{0:0}]: {1}", i, durations[ i ] );
			}
			// > Duration[0]: 01:30:00
			// > Duration[1]: 03:15:00
		} // DateTimeSetSample

		#endregion

		#region Quarter

		[Sample( "Quarter" )]
		// ----------------------------------------------------------------------
		public void QuarterSample()
		{
			Quarters quarters = new Quarters( 2006, YearQuarter.First, 6 );
			Console.WriteLine( "Quarters of: {0}", quarters );
			// > Quarters of: Q1 2006 - Q3 2007; 01.01.2006 - 30.06.2007 | 545.23:59
			foreach ( Quarter quarter in quarters.GetQuarters() )
			{
				Console.WriteLine( "Quarter: {0}", quarter );
			}
			// > Quarter: Q1 2006; 01.01.2006 - 31.03.2006 | 89.23:59
			// > Quarter: Q2 2006; 01.04.2006 - 30.06.2006 | 90.23:59
			// > Quarter: Q3 2006; 01.07.2006 - 30.09.2006 | 91.23:59
			// > Quarter: Q4 2006; 01.10.2006 - 31.12.2006 | 91.23:59
			// > Quarter: Q1 2007; 01.01.2007 - 31.03.2007 | 89.23:59
			// > Quarter: Q2 2007; 01.04.2007 - 30.06.2007 | 90.23:59
		} // QuartesSample

		#endregion

		#region Duration

		[Sample( "Duration" )]
		// ----------------------------------------------------------------------
		public void DurationDescriptionSample()
		{
			DateTime start = new DateTime( 2011, 1, 28, 18, 45, 0 );
			DateTime end = new DateTime( 2012, 1, 1 );

			TimeRange timeRange = new TimeRange( start, end );
			Console.WriteLine( "TimeRange: " + timeRange );
			// > TimeRange: 28.01.2011 18:45:00 - 01.01.2012 00:00:00 | 337.05:15

			Console.WriteLine( "TimeRange.Duration: " + timeRange.Duration );
			// > TimeRange.Duration: 337.05:15:00
			Console.WriteLine( "TimeRange.DurationDescription: " + timeRange.DurationDescription );
			// > TimeRange.DurationDescription: 337 days 5 hours 15 mins
		} // DurationDescriptionSample

		#endregion

		#region TimeFormatter

		[Sample( "TimeFormatter" )]
		// ----------------------------------------------------------------------
		public void TimeFormatterSample()
		{
			DateTime start = new DateTime( 2011, 1, 28, 18, 45, 0 );
			DateTime end = new DateTime( 2012, 1, 1 );
			TimeRange timeRange = new TimeRange( start, end );

			// use default formatter
			Console.WriteLine( "TimeRange.GetDescription: " + timeRange.GetDescription() );
			// > TimeRange.GetDescription: 28.01.2011 18:45:00 - 01.01.2012 00:00:00 | 337.05:15

			// custom formatter (named parameters)
			TimeFormatter timeFormatter = new TimeFormatter(
				startEndSeparator: "...",
				durationType: DurationFormatType.Detailed,
				dateTimeFormat: "D" );

			// use custom formatter (named parameters)
			Console.WriteLine( "TimeRange.GetDescription: " + timeRange.GetDescription( timeFormatter ) );
			// > TimeRange.GetDescription: Freitag, 28. Januar 2011...Sonntag, 1. Januar 2012 | 337 days 5 hours 15 mins
		} // TimeFormatterSample

		#endregion

		#region Week

		[Sample( "WeekDays" )]
		// ----------------------------------------------------------------------
		public void WeekDaysSample()
		{
			Week week = new Week( new DateTime( 2011, 05, 13 ) );
			foreach ( Day day in week.GetDays() )
			{
				Console.WriteLine( "Day: {0}, DayOfWeek: {1}, Int: {2}", day, day.DayOfWeek, (int)day.DayOfWeek );
				// > Day: Montag; 09.05.2011 | 0.23:59, DayOfWeek: Monday, Int: 1
				// > Day: Dienstag; 10.05.2011 | 0.23:59, DayOfWeek: Tuesday, Int: 2
				// > Day: Mittwoch; 11.05.2011 | 0.23:59, DayOfWeek: Wednesday, Int: 3
				// > Day: Donnerstag; 12.05.2011 | 0.23:59, DayOfWeek: Thursday, Int: 4
				// > Day: Freitag; 13.05.2011 | 0.23:59, DayOfWeek: Friday, Int: 5
				// > Day: Samstag; 14.05.2011 | 0.23:59, DayOfWeek: Saturday, Int: 6
				// > Day: Sonntag; 15.05.2011 | 0.23:59, DayOfWeek: Sunday, Int: 0
			}
		} // WeekDaysSample

		[Sample( "WeekCulture" )]
		// ----------------------------------------------------------------------
		public void WeekCultureSample()
		{
			Week currentCultureWeek = new Week( 2011, 1 );
			Console.WriteLine( "Week: {0}, Culture: {1}", currentCultureWeek, currentCultureWeek.Calendar.Culture.DisplayName );
			// > Week: w/c 1 2011; 03.01.2011 - 09.01.2011 | 6.23:59, Culture: German (Switzerland)

			Week enUsWeek = new Week( 2011, 1, new TimeCalendar( new TimeCalendarConfig { Culture = new CultureInfo( "en-US" ) } ) );
			Console.WriteLine( "Week: {0}, Culture: {1}", enUsWeek, enUsWeek.Calendar.Culture.DisplayName );
			// > Week: w/c 1 2011; 01.01.2011 - 07.01.2011 | 6.23:59, Culture: English (United States)

			Week deChWeek = new Week( 2011, 1, new TimeCalendar( new TimeCalendarConfig { Culture = new CultureInfo( "de-CH" ) } ) );
			Console.WriteLine( "Week: {0}, Culture: {1}", deChWeek, deChWeek.Calendar.Culture.DisplayName );
			// > Week: w/c 1 2011; 03.01.2011 - 09.01.2011 | 6.23:59, Culture: German (Switzerland)
		} // WeekCultureSample

		[Sample( "CustomWeek" )]
		// ----------------------------------------------------------------------
		public void CustomWeekSample()
		{
			DateTime testDate = new DateTime( 2011, 3, 19 ); // saturday
			CustomWeek customWeek = new CustomWeek( testDate, DayOfWeek.Wednesday );
			Console.WriteLine( "CustomWeek: {0}", customWeek );
			// > CustomWeek: 16.03.2011 00:00:00 - 22.03.2011 23:59:59 | 6.23:59
			Console.WriteLine( "CustomWeek.FirstDayOfWeek: {0}", customWeek.FirstDayOfWeek );
			// > CustomWeek.FirstDayOfWeek: Wednesday

			// days
			foreach ( Day day in customWeek.GetDays() )
			{
				Console.WriteLine( "Day: {0}", day );
			}
			// > Day: Mittwoch; 16.03.2011 - 16.03.2011 | 0.23:59
			// > Day: Donnerstag; 17.03.2011 - 17.03.2011 | 0.23:59
			// > Day: Freitag; 18.03.2011 - 18.03.2011 | 0.23:59
			// > Day: Samstag; 19.03.2011 - 19.03.2011 | 0.23:59
			// > Day: Sonntag; 20.03.2011 - 20.03.2011 | 0.23:59
			// > Day: Montag; 21.03.2011 - 21.03.2011 | 0.23:59
			// > Day: Dienstag; 22.03.2011 - 22.03.2011 | 0.23:59
		} // CustomWeekSample

		[Sample( "GetDayOfCurrentWeek" )]
		// ----------------------------------------------------------------------
		public void GetDayOfCurrentWeek()
		{
			foreach ( DayOfWeek dayOfWeek in Enum.GetValues( typeof( DayOfWeek ) ) )
			{
				Console.WriteLine( "Day {0} of the current week: {1:d}", dayOfWeek, GetDayOfCurrentWeek( dayOfWeek ) );
			}
		} // GetDayOfCurrentWeek

		// ----------------------------------------------------------------------
		private static DateTime GetDayOfCurrentWeek( DayOfWeek dayOfWeek )
		{
			Week week = new Week();
			foreach ( Day day in week.GetDays() )
			{
				if ( day.DayOfWeek == dayOfWeek )
				{
					return day.Start.Date;
				}
			}
			throw new InvalidOperationException();
		} // GetDayOfCurrentWeek

		[Sample( "InvolvedWeekCount" )]
		// ----------------------------------------------------------------------
		public void InvolvedWeekCount()
		{
			Console.WriteLine( "Involved week count: " +
				CalcInvolvedWeekCount( new DateTime( 2011, 1, 1 ), new DateTime( 2011, 1, 2 ), DayOfWeek.Sunday ) );
			// > Involved week count: 2
			Console.WriteLine( "Involved week count: " +
				CalcInvolvedWeekCount( new DateTime( 2011, 1, 2 ), new DateTime( 2011, 1, 8 ), DayOfWeek.Sunday ) );
			// > Involved week count: 1
			Console.WriteLine( "Involved week count: " +
				CalcInvolvedWeekCount( new DateTime( 2011, 1, 1 ), new DateTime( 2011, 1, 29 ), DayOfWeek.Sunday ) );
			// > Involved week count: 5
			Console.WriteLine( "Involved week count: " +
				CalcInvolvedWeekCount( new DateTime( 2011, 1, 1 ), new DateTime( 2011, 1, 30 ), DayOfWeek.Sunday ) );
			// > Involved week count: 6
			Console.WriteLine( "Involved week count: " +
				CalcInvolvedWeekCount( new DateTime( 2011, 1, 1 ), new DateTime( 2011, 2, 1 ), DayOfWeek.Sunday ) );
			// > Involved week count: 6
		} // InvolvedWeekCount

		// ----------------------------------------------------------------------
		private static int CalcInvolvedWeekCount( DateTime date1, DateTime date2, DayOfWeek firstDayOfWeek )
		{
			if ( date1.Date.Equals( date2.Date ) )
			{
				return 0;
			}

			DateTime startWeek = TimeTool.GetStartOfWeek( date1, firstDayOfWeek );
			DateTime endWeek = TimeTool.GetStartOfWeek( date2, firstDayOfWeek ).AddDays( TimeSpec.DaysPerWeek );

			return (int)( endWeek.Subtract( startWeek ).TotalDays / TimeSpec.DaysPerWeek );
		} // CalcInvolvedWeekCount

		#endregion

		#region CalculateBusinessHours

		[Sample( "CalculateBusinessHours" )]
		// ----------------------------------------------------------------------
		public void CalculateBusinessHoursSample()
		{
			CalendarTimeRange testPeriod = new CalendarTimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 5, 1 ) );
			Console.WriteLine( "period: {0}", testPeriod );
			// > period: 01.03.2011 00:00:00 - 30.04.2011 23:59:59 | 60.23:59

			TimePeriodCollection holidays = new TimePeriodCollection();
			Console.WriteLine( "business hours without holidays: {0}", CalculateBusinessHours( testPeriod, holidays ) );
			// > business hours without holidays: 396

			holidays.Add( new Day( 2011, 3, 9 ) );       // day 3/9/2011
			Console.WriteLine( "business hours with holidays: {0}", CalculateBusinessHours( testPeriod, holidays ) );
			// > business hours with holidays: 387

			holidays.Add( new Days( 2011, 3, 16, 2 ) );  // days 16/9/2011 and 17/9/2011
			Console.WriteLine( "business hours with more holidays: {0}", CalculateBusinessHours( testPeriod, holidays ) );
			// > business hours with more holidays: 369

			holidays.Add( new Week( 2011, 13 ) );        // w/c 13 2011
			Console.WriteLine( "business hours with even more holidays: {0}", CalculateBusinessHours( testPeriod, holidays ) );
			// > business hours with even more holidays: 324
		} // CalculateBusinessHoursSample

		// ----------------------------------------------------------------------
		public double CalculateBusinessHours( CalendarTimeRange testPeriod, ITimePeriodCollection holidays = null )
		{
			CalendarPeriodCollectorFilter filter = new CalendarPeriodCollectorFilter();
			filter.CollectingMonths.Add( new MonthRange( YearMonth.January, YearMonth.January ) );
			filter.CollectingDays.Add( new DayRange( 1, 1 ) );
			filter.AddWorkingWeekDays(); // only working days
			filter.CollectingHours.Add( new HourRange( 8, 12 ) );  // opening hours morning
			filter.CollectingHours.Add( new HourRange( 13, 18 ) ); // opening hours afternoon
			if ( holidays != null )
			{
				filter.ExcludePeriods.AddAll( holidays );
			}

			CalendarPeriodCollector collector = new CalendarPeriodCollector( filter, testPeriod );
			collector.CollectHours();

			double businessHours = 0.0;
			foreach ( ICalendarTimeRange period in collector.Periods )
			{
				businessHours += Math.Round( period.Duration.TotalHours, 2 );
			}
			return businessHours;
		} // CalculateBusinessHours

		#endregion

		#region NetworkDays

		[Sample( "NetworkDays" )]
		// ----------------------------------------------------------------------
		public void NetworkDaysSample()
		{
			DateTime start = new DateTime( 2011, 3, 1 );
			DateTime end = new DateTime( 2011, 5, 1 );
			Console.WriteLine( "period: {0}", new CalendarTimeRange( start, end ) );
			// > period: 01.03.2011 00:00:00 - 30.04.2011 23:59:59 | 60.23:59

			Console.WriteLine( "network days without holidays: {0}", NetworkDays( start, end ) );
			// > network days without holidays: 44

			List<DateTime> holidays = new List<DateTime>(); // collection of holidays
			holidays.Add( new DateTime( 2011, 3, 9 ) );  // day 3/9/2011
			holidays.Add( new DateTime( 2011, 3, 16 ) ); // day 16/9/2011
			holidays.Add( new DateTime( 2011, 3, 17 ) ); // day 17/9/2011
			Console.WriteLine( "network days with holidays: {0}", NetworkDays( start, end, holidays ) );
			// > network days with holidays: 41

			TimePeriodCollection holidayPeriods = new TimePeriodCollection(); // collection of holiday-periods
			holidayPeriods.Add( new Week( 2011, 13 ) ); // w/c 13 2011
			Console.WriteLine( "network days with holidays and holiday-periods: {0}", NetworkDays( start, end, holidays, holidayPeriods ) );
			// > network days with holidays and holiday-periods: 36
		} // NetworkDaysSample

		// ----------------------------------------------------------------------
		public double NetworkDays( DateTime start, DateTime end, IEnumerable<DateTime> holidays = null, ITimePeriodCollection holidayPeriods = null )
		{
			Day startDay = new Day( start < end ? start : end );
			Day endDay = new Day( end > start ? end : start );
			if ( startDay.Equals( endDay ) )
			{
				return 0;
			}

			CalendarPeriodCollectorFilter filter = new CalendarPeriodCollectorFilter();
			filter.AddWorkingWeekDays(); // only working days
			if ( holidays != null )
			{
				foreach ( DateTime holiday in holidays )
				{
					filter.ExcludePeriods.Add( new Day( holiday ) );
				}
			}
			if ( holidayPeriods != null )
			{
				filter.ExcludePeriods.AddAll( holidayPeriods );
			}

			CalendarTimeRange testPeriod = new CalendarTimeRange( start, end );
			CalendarPeriodCollector collector = new CalendarPeriodCollector( filter, testPeriod );
			collector.CollectDays();

			double networkDays = 0.0;
			foreach ( ICalendarTimeRange period in collector.Periods )
			{
				networkDays += Math.Round( period.Duration.TotalDays, 2 );
			}
			return networkDays;
		} // NetworkDays

		#endregion

		#region WorkingPeriod

		[Sample( "WorkingPeriod" )]
		// ----------------------------------------------------------------------
		public void WorkingPeriodSample()
		{
			DateTime start = new DateTime( 2011, 3, 4, 16, 45, 0 );
			DateTime end = new DateTime( 2011, 3, 8, 15, 30, 0 );
			// result should be 9 hours 45 minutes:
			// 04/03/2011 16:45 - 19:00 -> 2 hours 15 minutes
			// 08/03/2011 07:00 - 12:00 -> 5 hours
			// 08/03/2011 13:00 - 15:30 -> 2 hours 30 minutes

			TimePeriodCollection excludePeriods = new TimePeriodCollection();
			excludePeriods.Add( new Day( 2011, 3, 7 ) ); // exclude the day 07/03/2011

			Console.WriteLine( "support minutes: {0}", CalcWorkingPeriod( start, end, excludePeriods ) );
			// > support hours: 09:45:00
		} // WorkingPeriodSample

		// ----------------------------------------------------------------------
		public TimeSpan CalcWorkingPeriod( DateTime start, DateTime end,
			ITimePeriodCollection excludePeriods = null )
		{
			if ( start.Equals( end ) )
			{
				return TimeSpan.Zero;
			}

			// test range
			TimeRange testRange = new TimeRange( start, end );

			// search range
			DateTime searchStart = new Day( testRange.Start ).Start;
			DateTime serachEnd = new Day( testRange.End ).GetNextDay().Start;
			TimeRange searchPeriod = new TimeRange( searchStart, serachEnd );

			// search filter
			CalendarPeriodCollectorFilter filter = new CalendarPeriodCollectorFilter();
			filter.AddWorkingWeekDays(); // working days
			if ( excludePeriods != null )
			{
				filter.ExcludePeriods.AddAll( excludePeriods );
			}
			filter.CollectingHours.Add( new HourRange( 07, 12 ) ); // working hours
			filter.CollectingHours.Add( new HourRange( 13, 19 ) ); // working hours

			// collect working hours
			TimeCalendar calendar = new TimeCalendar( new TimeCalendarConfig { EndOffset = TimeSpan.Zero } );
			CalendarPeriodCollector collector = new CalendarPeriodCollector( filter, searchPeriod, SeekDirection.Forward, calendar );
			collector.CollectHours();

			TimeSpan workingPeriod = new TimeSpan();
			foreach ( ICalendarTimeRange period in collector.Periods )
			{
				// get the intersection of the test-range and the day working-hours
				ITimePeriod intersection = testRange.GetIntersection( period );
				if ( intersection == null )
				{
					continue;
				}
				workingPeriod = workingPeriod.Add( intersection.Duration );
			}
			return workingPeriod;
		} // CalcWorkingPeriod

		#endregion

		#region YearDiff

		[Sample( "CalculateAgeSamples" )]
		// ----------------------------------------------------------------------
		public void CalculateAgeSamples()
		{
			PrintAge( new DateTime( 2000, 02, 29 ), new DateTime( 2009, 02, 28 ) );
			// > Birthdate=29.02.2000, Age at 28.02.2009 is 8 years
			PrintAge( new DateTime( 2000, 02, 29 ), new DateTime( 2012, 02, 28 ) );
			// > Birthdate=29.02.2000, Age at 28.02.2012 is 11 years
		} // CalculateAgeSamples

		// ----------------------------------------------------------------------
		public void PrintAge( DateTime birthDate, DateTime moment )
		{
			Console.WriteLine( "Birthdate={0:d}, Age at {1:d} is {2} years", birthDate, moment, YearDiff( birthDate, moment ) );
		} // PrintAge

		// ----------------------------------------------------------------------
		private static int YearDiff( DateTime date1, DateTime date2 )
		{
			if ( DateTimeFormatInfo.CurrentInfo == null )
			{
				return -1;
			}
			return YearDiff( date1, date2, DateTimeFormatInfo.CurrentInfo.Calendar );
		} // YearDiff

		// ----------------------------------------------------------------------
		// extract of DateDiff.CalcYears()
		private static int YearDiff( DateTime date1, DateTime date2, Calendar calendar )
		{
			if ( date1.Equals( date2 ) )
			{
				return 0;
			}

			int year1 = calendar.GetYear( date1 );
			int month1 = calendar.GetMonth( date1 );
			int year2 = calendar.GetYear( date2 );
			int month2 = calendar.GetMonth( date2 );

			// find the the day to compare
			int compareDay = date2.Day;
			int compareDaysPerMonth = calendar.GetDaysInMonth( year1, month1 );
			if ( compareDay > compareDaysPerMonth )
			{
				compareDay = compareDaysPerMonth;
			}

			// build the compare date
			DateTime compareDate = new DateTime( year1, month2, compareDay,
				date2.Hour, date2.Minute, date2.Second, date2.Millisecond );
			if ( date2 > date1 )
			{
				if ( compareDate < date1 )
				{
					compareDate = compareDate.AddYears( 1 );
				}
			}
			else
			{
				if ( compareDate > date1 )
				{
					compareDate = compareDate.AddYears( -1 );
				}
			}
			return year2 - calendar.GetYear( compareDate );
		} // YearDiff

		#endregion

		#region UserFriendlyTimeSpan

		[Sample( "UserFriendlyTimeSpan" )]
		// ----------------------------------------------------------------------
		public void UserFriendlyTimeSpanSample()
		{
			TimeSpan lastVisit = new TimeSpan( 400, 7, 25, 0 );
			Console.WriteLine( "last visit before {0}", GetTimeSpanDescription( lastVisit, 3 ) );
			// > last visit before: 1 Year 1 Month 3 Days

			// 60 days
			TimeSpan reminderDuration = new TimeSpan( 60, 0, 0, 0, 0 );
			Console.WriteLine( "reminder duration: {0} Days", reminderDuration.TotalDays );

			DateTime now = DateTime.Now;
			DateTime moment1 = new DateTime( 2011, 4, 1 );
			DateTime moment2 = new DateTime( 2011, 2, 1 );
			DateTime moment3 = new DateTime( 2011, 3, 1 );

			// past
			Console.WriteLine( "last reminder (from now): {0}", GetTimeSpanDescription( now, reminderDuration.Negate() ) );
			// > last reminder (from now): -1 Months -30 Days
			Console.WriteLine( "last reminder ({0:d}): {1}", moment1, GetTimeSpanDescription( moment1, reminderDuration.Negate() ) );
			// > last reminder (01.04.2011): -1 Months -29 Days
			Console.WriteLine( "last reminder ({0:d}): {1}", moment2, GetTimeSpanDescription( moment2, reminderDuration.Negate() ) );
			// > last reminder (01.02.2011): -2 Months -1 Days
			Console.WriteLine( "last reminder ({0:d}): {1}", moment3, GetTimeSpanDescription( moment3, reminderDuration.Negate() ) );
			// > last reminder (01.03.2011): -1 Months -29 Days

			// future
			Console.WriteLine( "next reminder (from now): {0}", GetTimeSpanDescription( now, reminderDuration ) );
			// > next reminder (from now): 1 Month 29 Days
			Console.WriteLine( "next reminder {0:d}: {1}", moment1, GetTimeSpanDescription( moment1, reminderDuration ) );
			// > next reminder 01.04.2011: 1 Month 30 Days
			Console.WriteLine( "next reminder {0:d}: {1}", moment2, GetTimeSpanDescription( moment2, reminderDuration ) );
			// > next reminder 01.02.2011: 2 Months 1 Day
			Console.WriteLine( "next reminder {0:d}: {1}", moment3, GetTimeSpanDescription( moment3, reminderDuration ) );
			// > next reminder 01.03.2011: 1 Month 29 Days
		} // UserFriendlyTimeSpanSample

		// ----------------------------------------------------------------------
		public string GetTimeSpanDescription( TimeSpan timeSpan, int precision = int.MaxValue )
		{
			return GetTimeSpanDescription( DateTime.Now, timeSpan, precision );
		} // GetTimeSpanDescription

		// ----------------------------------------------------------------------
		public string GetTimeSpanDescription( DateTime moment, TimeSpan timeSpan, int precision = int.MaxValue )
		{
			if ( timeSpan == TimeSpan.Zero )
			{
				return string.Empty;
			}

			bool isPositive = timeSpan > TimeSpan.Zero;
			DateTime date1 = isPositive ? moment : moment.Subtract( timeSpan );
			DateTime date2 = isPositive ? moment.Add( timeSpan ) : moment;

			return new DateDiff( date1, date2 ).GetDescription( precision );
		} // GetTimeSpanDescription

		#endregion

		#region CombinedPeriods

		[Sample( "CombinedPeriodsSample" )]
		// ----------------------------------------------------------------------
		public void CombinedPeriodsSample()
		{
			TimePeriodCollection periods = new TimePeriodCollection();
			periods.Add( new IdTimeRange( 1, new DateTime( 2000, 1, 1 ), new DateTime( 2019, 12, 31 ) ) );
			periods.Add( new IdTimeRange( 2, new DateTime( 2000, 1, 1 ), new DateTime( 2019, 12, 31 ) ) );
			periods.Add( new IdTimeRange( 3, new DateTime( 2000, 1, 1 ), new DateTime( 2009, 12, 31 ) ) );
			periods.Add( new IdTimeRange( 3, new DateTime( 2010, 1, 1 ), new DateTime( 2019, 12, 31 ) ) );
			periods.Add( new IdTimeRange( 4, new DateTime( 2000, 1, 1 ), new DateTime( 2019, 12, 31 ) ) );
			periods.Add( new IdTimeRange( 5, new DateTime( 2000, 1, 1 ), new DateTime( 2014, 12, 31 ) ) );
			periods.Add( new IdTimeRange( 5, new DateTime( 2015, 1, 1 ), new DateTime( 2019, 12, 31 ) ) );
			foreach ( ITimePeriod period in periods )
			{
				Console.WriteLine( "Period: " + period );
			}

			TimeRange test1 = new TimeRange( new DateTime( 2000, 1, 1 ), new DateTime( 2009, 12, 31 ) );
			ITimePeriodCollection intersections1 = periods.IntersectionPeriods( test1 );
			foreach ( ITimePeriod intersection1 in intersections1 )
			{
				Console.WriteLine( "Intersection of {0}: {1}", test1, intersection1 );
			}

			TimeRange test2 = new TimeRange( new DateTime( 2010, 1, 1 ), new DateTime( 2014, 12, 31 ) );
			ITimePeriodCollection intersections2 = periods.IntersectionPeriods( test2 );
			foreach ( ITimePeriod intersection2 in intersections2 )
			{
				Console.WriteLine( "Intersection of {0}: {1}", test2, intersection2 );
			}

			TimeRange test3 = new TimeRange( new DateTime( 2015, 1, 1 ), new DateTime( 2019, 12, 31 ) );
			ITimePeriodCollection intersections3 = periods.IntersectionPeriods( test3 );
			foreach ( ITimePeriod intersection3 in intersections3 )
			{
				Console.WriteLine( "Intersection of {0}: {1}", test3, intersection3 );
			}

		} // CombinedPeriodsSample

		#endregion

		#region TimeLinePeriods

		[Sample( "TimeLinePeriodsSample" )]
		// ----------------------------------------------------------------------
		public void TimeLinePeriodsSample()
		{
			TimePeriodCollection periods = new TimePeriodCollection();
			periods.Add( new IdTimeRange( 1, new DateTime( 2000, 1, 1 ), new DateTime( 2019, 12, 31 ) ) );
			periods.Add( new IdTimeRange( 2, new DateTime( 2000, 1, 1 ), new DateTime( 2019, 12, 31 ) ) );
			periods.Add( new IdTimeRange( 3, new DateTime( 2000, 1, 1 ), new DateTime( 2009, 12, 31 ) ) );
			periods.Add( new IdTimeRange( 3, new DateTime( 2010, 1, 1 ), new DateTime( 2019, 12, 31 ) ) );
			periods.Add( new IdTimeRange( 4, new DateTime( 2000, 1, 1 ), new DateTime( 2019, 12, 31 ) ) );
			periods.Add( new IdTimeRange( 5, new DateTime( 2000, 1, 1 ), new DateTime( 2014, 12, 31 ) ) );
			periods.Add( new IdTimeRange( 5, new DateTime( 2015, 1, 1 ), new DateTime( 2019, 12, 31 ) ) );
			foreach ( ITimePeriod period in periods )
			{
				Console.WriteLine( "Period: " + period );
			}

			// time line with all period start and end moments
			ITimeLineMomentCollection moments = new TimeLineMomentCollection();
			moments.AddAll( periods );
			DateTime start = periods.Start;
			foreach ( ITimeLineMoment moment in moments )
			{
				if ( moment.EndCount <= 0 ) // search the next period end
				{
					continue;
				}
				DateTime end = moment.Moment;
				TimeRange timeRange = new TimeRange( start, end );
				Console.WriteLine( "Period: {0}", timeRange );
				ITimePeriodCollection intersections = periods.IntersectionPeriods( timeRange );
				foreach ( ITimePeriod intersection in intersections )
				{
					Console.WriteLine( "  Intersection: {0}", intersection );
				}
				start = moment.Moment;
			}
		} // TimeLinePeriodsSample

		#endregion

		#region WeekRepeatSample

		[Sample( "WeekRepeatSample" )]
		// ----------------------------------------------------------------------
		public void WeekRepeatSample()
		{
			DateTime start = new DateTime( 2011, 06, 1 );
			DayOfWeek[] weekDays = new[] { DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };
			Console.WriteLine( "IsWeekRecuringDay: {0}", IsWeekRecuringDay( start, new DateTime( 2011, 06, 08 ), 2, weekDays ) ); // false
			Console.WriteLine( "IsWeekRecuringDay: {0}", IsWeekRecuringDay( start, new DateTime( 2011, 06, 11 ), 2, weekDays ) ); // false
			Console.WriteLine( "IsWeekRecuringDay: {0}", IsWeekRecuringDay( start, new DateTime( 2011, 06, 15 ), 2, weekDays ) ); // true
			Console.WriteLine( "IsWeekRecuringDay: {0}", IsWeekRecuringDay( start, new DateTime( 2011, 06, 18 ), 2, weekDays ) ); // false
			Console.WriteLine( "IsWeekRecuringDay: {0}", IsWeekRecuringDay( start, new DateTime( 2011, 06, 22 ), 2, weekDays ) ); // false
			Console.WriteLine( "IsWeekRecuringDay: {0}", IsWeekRecuringDay( start, new DateTime( 2011, 06, 25 ), 2, weekDays ) ); // false
			Console.WriteLine( "IsWeekRecuringDay: {0}", IsWeekRecuringDay( start, new DateTime( 2011, 06, 29 ), 2, weekDays ) ); // true
			Console.WriteLine( "IsWeekRecuringDay: {0}", IsWeekRecuringDay( start, new DateTime( 2011, 07, 02 ), 2, weekDays ) ); // false
		} // WeekRepeatSample

		// ----------------------------------------------------------------------
		public bool IsWeekRecuringDay( DateTime start, DateTime test, int recuringInterval, params DayOfWeek[] weekDays )
		{
			if ( test < start || recuringInterval <= 0 )
			{
				return false;
			}

			bool isValidDayOfWeek = false;
			DayOfWeek testDayOfWeek = test.DayOfWeek;
			foreach ( DayOfWeek weekDay in weekDays )
			{
				if ( weekDay == testDayOfWeek )
				{
					isValidDayOfWeek = true;
					break;
				}
			}
			if ( !isValidDayOfWeek )
			{
				return false;
			}

			DateDiff dateDiff = new DateDiff( start, test );
			return ( dateDiff.Weeks % recuringInterval ) == 0;
		} // IsWeekRecuringDay

		#endregion

		#region CountWorkingDays

		[Sample( "CountWorkingDays" )]
		// ----------------------------------------------------------------------
		public void CountWorkingDaysSample()
		{
			DayOfWeek[] workingDays = new[] { DayOfWeek.Monday, DayOfWeek.Tuesday };
			DateTime start = new DateTime( 2011, 3, 1 );
			DateTime end = new DateTime( 2011, 5, 1 );
			Console.WriteLine( "working days: {0}", CountWorkingDays( start, end, workingDays ) );
			// > working days: 19
		} // CountWorkingDaysSample

		// ----------------------------------------------------------------------
		public int CountWorkingDays( DateTime start, DateTime end, IList<DayOfWeek> workingDays )
		{
			if ( workingDays.Count == 0 )
			{
				return 0;
			}

			Week startWeek = new Week( start );
			Week endWeek = new Week( end );
			int dayCount = 0;

			// start week
			DateTime currentDay = start.Date;
			while ( currentDay < startWeek.End )
			{
				if ( workingDays.Contains( currentDay.DayOfWeek ) )
				{
					dayCount++;
				}
				currentDay = currentDay.AddDays( 1 );
			}

			// between weeks
			DateDiff inBetweenWeekDiff = new DateDiff( startWeek.End, endWeek.Start );
			dayCount += inBetweenWeekDiff.Weeks * workingDays.Count;

			// end week
			currentDay = endWeek.Start.Date;
			while ( currentDay < end )
			{
				if ( workingDays.Contains( currentDay.DayOfWeek ) )
				{
					dayCount++;
				}
				currentDay = currentDay.AddDays( 1 );
			}

			return dayCount;
		} // CountWorkingDays

		#endregion

		#region CountDaysByMonth

		[Sample( "CountDaysByMonth" )]
		// ----------------------------------------------------------------------
		public void CountDaysByMonthSample()
		{
			DateTime start = new DateTime( 2011, 3, 30 );
			DateTime end = new DateTime( 2011, 4, 5 );

			Dictionary<DateTime, int> monthDays = CountMonthDays( start, end );
			foreach ( KeyValuePair<DateTime, int> monthDay in monthDays )
			{
				Console.WriteLine( "month {0:d}, days {1}", monthDay.Key, monthDay.Value );
			}
			// > month 01.03.2011, days 2
			// > month 01.04.2011, days 5
		} // CountDaysByMonthSample

		// ----------------------------------------------------------------------
		public Dictionary<DateTime, int> CountMonthDays( DateTime start, DateTime end )
		{
			Dictionary<DateTime, int> monthDays = new Dictionary<DateTime, int>();

			Month startMonth = new Month( start );
			Month endMonth = new Month( end );

			// single month request
			if ( startMonth.Equals( endMonth ) )
			{
				monthDays.Add( startMonth.Start, end.Subtract( start ).Days );
				return monthDays;
			}

			Month month = startMonth;
			while ( month.Start < endMonth.End )
			{
				// start month
				if ( month.Equals( startMonth ) )
				{
					monthDays.Add( month.Start, month.DaysInMonth - start.Day + 1 );
				}
				// end month
				else if ( month.Equals( endMonth ) )
				{
					monthDays.Add( month.Start, end.Day );
				}
				// in-between month
				else
				{
					monthDays.Add( month.Start, month.DaysInMonth );
				}
				month = month.GetNextMonth();
			}

			return monthDays;
		} // CountMonthDays

		#endregion

		#region CalendarDateAddSample

		[Sample( "AddWeekDaySample" )]
		// ----------------------------------------------------------------------
		public void AddWeekDaySample()
		{
			CalendarDateAdd calendarDateAdd = new CalendarDateAdd();

			calendarDateAdd.AddWorkingWeekDays();

			calendarDateAdd.WorkingDayHours.Add( new DayHourRange( DayOfWeek.Monday, 09, 16 ) );
			calendarDateAdd.WorkingDayHours.Add( new DayHourRange( DayOfWeek.Tuesday, 09, 16 ) );
			calendarDateAdd.WorkingDayHours.Add( new DayHourRange( DayOfWeek.Wednesday, 09, 16 ) );
			calendarDateAdd.WorkingDayHours.Add( new DayHourRange( DayOfWeek.Thursday, 09, 16 ) );
			calendarDateAdd.WorkingDayHours.Add( new DayHourRange( DayOfWeek.Friday, 09, 13 ) );

			DateTime start = new DateTime( 2011, 08, 15 );
			Console.WriteLine( "AddWeekDaySample : {0}", calendarDateAdd.Add( start, new TimeSpan( 00, 0, 0 ) ) );
			// > AddWeekDaySample : 15.08.2011 09:00:00
			Console.WriteLine( "AddWeekDaySample : {0}", calendarDateAdd.Add( start, new TimeSpan( 07, 0, 0 ) ) );
			// > AddWeekDaySample : 16.08.2011 09:00:00
			Console.WriteLine( "AddWeekDaySample : {0}", calendarDateAdd.Add( start, new TimeSpan( 28, 0, 0 ) ) );
			// > AddWeekDaySample : 19.08.2011 09:00:00
			Console.WriteLine( "AddWeekDaySample : {0}", calendarDateAdd.Add( start, new TimeSpan( 31, 0, 0 ) ) );
			// > AddWeekDaySample : 19.08.2011 12:00:00
			Console.WriteLine( "AddWeekDaySample : {0}", calendarDateAdd.Add( start, new TimeSpan( 32, 0, 0 ) ) );
			// > AddWeekDaySample : 22.08.2011 09:00:00
		} // AddWeekDaySample

		#endregion

		#region CalendarDateDiffSample

		// ----------------------------------------------------------------------
		[Sample( "CalendarDateDiffHour" )]
		public void CalendarDateDiffHourSample()
		{
			DateTime date1 = new DateTime( 2011, 9, 11, 18, 0, 0 );
			DateTime date2 = new DateTime( 2011, 9, 15, 9, 0, 0 );

			CalendarDateDiff calendarDateDiff = new CalendarDateDiff();
			calendarDateDiff.WorkingHours.Add( new HourRange( 8, 12 ) );

			Console.WriteLine( "CalendarDateDiffHourSample : {0} hours", calendarDateDiff.Difference( date1, date2 ).TotalHours );
			// > CalendarDateDiffHourSample : 74 hours

			Console.WriteLine( "CalendarDateDiffHourSample : {0} hours", calendarDateDiff.Difference( date2, date1 ).TotalHours );
			// > CalendarDateDiffHourSample : -74 hours
		} // CalendarDateDiffHourSample

		// ----------------------------------------------------------------------
		[Sample( "CalendarDateDiffDayHour" )]
		public void CalendarDateDiffDayHourSample()
		{
			DateTime date1 = new DateTime( 2011, 9, 11, 18, 0, 0 );
			DateTime date2 = new DateTime( 2011, 9, 15, 9, 0, 0 );

			CalendarDateDiff calendarDateDiff = new CalendarDateDiff();
			calendarDateDiff.WorkingDayHours.Add( new DayHourRange( DayOfWeek.Monday, 8, 16 ) );
			calendarDateDiff.WorkingDayHours.Add( new DayHourRange( DayOfWeek.Tuesday, 8, 20 ) );

			Console.WriteLine( "CalendarDateDiffDayHourSample : {0} hours", calendarDateDiff.Difference( date1, date2 ).TotalHours );
			// > CalendarDateDiffHourSample : 67 hours

			Console.WriteLine( "CalendarDateDiffDayHourSample : {0} hours", calendarDateDiff.Difference( date2, date1 ).TotalHours );
			// > CalendarDateDiffHourSample : -67 hours
		} // CalendarDateDiffDayHourSample

		#endregion

		#region CalendarPeriodCollectorSample

		[Sample( "FindRemainigYearFridaysSample" )]
		// ----------------------------------------------------------------------
		public void FindRemainigYearFridaysSample()
		{
			// filter: only Fridays
			CalendarPeriodCollectorFilter filter = new CalendarPeriodCollectorFilter();
			filter.WeekDays.Add( DayOfWeek.Friday );

			// the collecting period
			CalendarTimeRange collectPeriod = new CalendarTimeRange( DateTime.Now, new Year().End.Date );

			// collect all Fridays
			CalendarPeriodCollector collector = new CalendarPeriodCollector( filter, collectPeriod );
			collector.CollectDays();

			// show the results
			foreach ( ITimePeriod period in collector.Periods )
			{
				Console.WriteLine( "Friday: " + period );
			}
		} // FindRemainigYearFridaysSample

		#endregion

		#region WorkingHourSum

		[Sample( "WorkingHourSum" )]
		// ----------------------------------------------------------------------
		public void WorkingHourSum()
		{
			DateTime start = new DateTime( 2011, 9, 11, 18, 0, 0 );
			DateTime end = new DateTime( 2011, 9, 15, 9, 0, 0 );

			// collect working hours
			CalendarPeriodCollectorFilter filter = new CalendarPeriodCollectorFilter();
			filter.CollectingDayHours.Add( new DayHourRange( DayOfWeek.Monday, 8, 12 ) );
			filter.CollectingDayHours.Add( new DayHourRange( DayOfWeek.Tuesday, 8, 12 ) );
			filter.CollectingDayHours.Add( new DayHourRange( DayOfWeek.Wednesday, 8, 12 ) );
			filter.CollectingDayHours.Add( new DayHourRange( DayOfWeek.Thursday, 8, 12 ) );
			filter.CollectingDayHours.Add( new DayHourRange( DayOfWeek.Friday, 8, 12 ) );
			filter.ExcludePeriods.Add( new Day( 2011, 9, 13 ) ); // exclude Tuesday
			CalendarPeriodCollector collector = new CalendarPeriodCollector(
				filter,
				new TimeRange( start.Date, end.Date.AddDays( 1 ) ),
				SeekDirection.Forward,
				new TimeCalendar( new TimeCalendarConfig { EndOffset = TimeSpan.Zero } ) );
			collector.CollectHours();

			// add boundaries
			collector.Periods.Add( new TimeRange( start, end ) );

			// intersect periods
			TimePeriodIntersector<TimeRange> periodIntersector = new TimePeriodIntersector<TimeRange>();
			ITimePeriodCollection periods = periodIntersector.IntersectPeriods( collector.Periods );

			// calculate working hours (sum period durations)
			TimeSpan workingHours = TimeSpan.Zero;
			foreach ( ITimePeriod period in periods )
			{
				workingHours = workingHours.Add( period.Duration );
			}

			Console.WriteLine( "Total working hours: " + workingHours );
			// > Total working hours: 09:00:00
		} // WorkingHourSum

		#endregion

		#region TimePeriodSubtractorSample

		[Sample( "TimePeriodSubtractorSample" )]
		// ----------------------------------------------------------------------
		public void TimePeriodSubtractorSample()
		{
			DateTime moment = new DateTime( 2012, 1, 29 );
			TimePeriodCollection sourcePeriods = new TimePeriodCollection
				{
						new TimeRange( moment.AddHours( 2 ), moment.AddDays( 1 ) )
				};

			TimePeriodCollection subtractingPeriods = new TimePeriodCollection
				{
						new TimeRange( moment.AddHours( 6 ), moment.AddHours( 10 ) ),
						new TimeRange( moment.AddHours( 12 ), moment.AddHours( 16 ) )
				};

			TimePeriodSubtractor<TimeRange> subtractor = new TimePeriodSubtractor<TimeRange>();
			ITimePeriodCollection subtractedPeriods = subtractor.SubtractPeriods( sourcePeriods, subtractingPeriods );
			foreach ( TimeRange subtractedPeriod in subtractedPeriods )
			{
				Console.WriteLine( "Subtracted Period: {0}", subtractedPeriod );
			}
			// > Subtracted Period : 29.01.2012 02:00:00 - 06:00:00 | 0.04:00
			// > Subtracted Period : 29.01.2012 10:00:00 - 12:00:00 | 0.02:00
			// > Subtracted Period : 29.01.2012 16:00:00 - 30.01.2012 00:00:00 | 0.08:00
		} // TimePeriodSubtractorSample

		#endregion

		#region CheckDateBetweenDatesSample

		[Sample( "CheckDateBetweenDatesSample" )]
		// ----------------------------------------------------------------------
		public void CheckDateBetweenDatesSample()
		{
			DateTime[] moments = new[]
				{
					new DateTime( 2011, 11, 02 ), 
					new DateTime( 2011, 12, 25 ), 
					new DateTime( 2011, 12, 09 ), 
					new DateTime( 2011, 12, 13 )
				};

			TimeRange timeRange = new TimeRange( new DateTime( 2011, 11, 01 ), new DateTime( 2011, 12, 15 ) );

			Console.WriteLine( "time range: {0}", timeRange );
			foreach ( DateTime moment in moments )
			{
				Console.WriteLine( "{0:d} is inside: {1}", moment, timeRange.HasInside( moment ) ? "1" : "0" );
			}
		} // CheckDateBetweenDatesSample

		#endregion

		#region LastDayInMonth

		[Sample( "LastDayInMonthSample" )]
		// ----------------------------------------------------------------------
		public void LastDayInMonthSample()
		{
			DateTime lastDayOfMonth = new DateTime( 2012, 3, 31 );
			DateTime notLastDayOfMonth = new DateTime( 2012, 3, 30 );
			Console.WriteLine( "{0:d} last day of month: {1}", lastDayOfMonth, IsLastDayOfMonth( lastDayOfMonth ) );
			Console.WriteLine( "{0:d} last day of month: {1}", notLastDayOfMonth, IsLastDayOfMonth( notLastDayOfMonth ) );

			DateTime lastWorkDayOfMonth = new DateTime( 2012, 3, 30 );
			DateTime notLastWorkDayOfMonth = new DateTime( 2012, 3, 29 );
			Console.WriteLine( "{0:d} last work day of month: {1}", lastWorkDayOfMonth, IsLastWorkingDayOfMonth( lastWorkDayOfMonth ) );
			Console.WriteLine( "{0:d} last work of month: {1}", notLastWorkDayOfMonth, IsLastWorkingDayOfMonth( notLastWorkDayOfMonth ) );
		} // LastDayInMonthSample

		// ----------------------------------------------------------------------
		private static DateTime LastDayOfMonth( int year, int month )
		{
			return new DateTime( year, month, DateTimeFormatInfo.CurrentInfo.Calendar.GetDaysInMonth( year, month ) );
		} // LastDayOfMonth

		// ----------------------------------------------------------------------
		private static bool IsLastDayOfMonth( DateTime test )
		{
			DateTime lastDayOfMonth = LastDayOfMonth( test.Year, test.Month );
			return test.Day == lastDayOfMonth.Day;
		} // IsLastDayOfMonth

		// ----------------------------------------------------------------------
		private static bool IsLastWorkingDayOfMonth( DateTime test )
		{
			DateTime lastWorkingDayOfMonth = LastDayOfMonth( test.Year, test.Month );
			while ( lastWorkingDayOfMonth.DayOfWeek == DayOfWeek.Saturday || lastWorkingDayOfMonth.DayOfWeek == DayOfWeek.Sunday )
			{
				lastWorkingDayOfMonth = lastWorkingDayOfMonth.AddDays( -1 );
			}

			return test.Day == lastWorkingDayOfMonth.Day;
		} // IsLastWorkingDayOfMonth

		#endregion

		#region DaysOfWeekSample

		[Sample( "DaysOfWeekSample" )]
		// ----------------------------------------------------------------------
		public void DaysOfWeekSample()
		{
			Week week = new Week( 2012, 13 );
			foreach ( Day day in week.GetDays() )
			{
				Console.WriteLine( day );
			}

			Console.WriteLine( "ISO 8601 Week" );
			TimeCalendar calendarIso8601 = new TimeCalendar(
				new TimeCalendarConfig { YearWeekType = YearWeekType.Iso8601 } );
			Week isoWeek = new Week( 2012, 13, calendarIso8601 );
			foreach ( Day isoDay in isoWeek.GetDays() )
			{
				Console.WriteLine( isoDay );
			}
		} // DaysOfWeekSample

		#endregion

		#region BusinessHoursSample

		[Sample( "BusinessHoursSample1" )]
		// ----------------------------------------------------------------------
		public void BusinessHoursSample1()
		{
			TimeRange timeRange = new TimeRange(
				new DateTime( 2012, 4, 05, 10, 30, 00 ), // start calc at 10:30 AM
				new DateTime( 2012, 4, 10, 23, 59, 59 ) );

			CalendarDateDiff dateDiff = new CalendarDateDiff();
			dateDiff.WorkingHours.Add( new HourRange( 8, 18 ) );
			dateDiff.AddWorkingWeekDays();
			TimeSpan nonBusinessHours = dateDiff.Difference( timeRange.Start, timeRange.End );

			double businessHours = timeRange.Duration.TotalHours - nonBusinessHours.TotalHours;
			Console.WriteLine( "business hours: " + businessHours );
			// > business hours: 37.5
		} // BusinessHoursSample1

		[Sample( "BusinessHoursSample2" )]
		// ----------------------------------------------------------------------
		public void BusinessHoursSample2()
		{
			TimeRange timeRange = new TimeRange(
				new DateTime( 2012, 4, 06, 10, 30, 00 ),
				new DateTime( 2012, 4, 06, 18, 00, 00 ) );
			double businessHours = CalculateBusinessHours( timeRange );
			Console.WriteLine( "business hours: " + businessHours );
			// > business hours: 37.5
		} // BusinessHoursSample2

		// ----------------------------------------------------------------------
		private double CalculateBusinessHours( ITimePeriod timePeriod )
		{
			double businessHours = 0;

			// collect filter
			CalendarPeriodCollectorFilter filter = new CalendarPeriodCollectorFilter();
			filter.AddWorkingWeekDays();
			filter.CollectingHours.Add( new HourRange( 8, 18 ) );
			//filter.ExcludePeriods.Add( new Day( 2012, 4, 6 ) );
			// mor exclude periods

			// collect the working hours
			TimeRange collectorTimeRange = new TimeRange( timePeriod.Start.Date, timePeriod.End.Date.AddDays( 1 ) );
			TimeCalendar calendar = new TimeCalendar( new TimeCalendarConfig { EndOffset = TimeSpan.Zero } );
			CalendarPeriodCollector collector = new CalendarPeriodCollector( filter, collectorTimeRange, SeekDirection.Forward, calendar );
			collector.CollectHours();
			if ( collector.Periods.Count > 0 )
			{
				// calculate the business hour periods
				collector.Periods.Add( timePeriod ); // add source period to find the intersections
				TimePeriodIntersector<TimeRange> intersector = new TimePeriodIntersector<TimeRange>();
				ITimePeriodCollection businessHourPeriods = intersector.IntersectPeriods( collector.Periods );

				// collect the business hours
				foreach ( TimeRange businessHourPeriod in businessHourPeriods )
				{
					businessHours += businessHourPeriod.Duration.TotalHours;
				}
			}

			return businessHours;
		} // CalculateBusinessHours

		#endregion

		#region CommonIntersectionSample

		[Sample( "CommonIntersectionSample" )]
		// ----------------------------------------------------------------------
		public void CommonIntersectionSample()
		{
			ITimePeriodCollection periods = new TimePeriodCollection();
			periods.Add( new TimeRange( new DateTime( 2000, 1, 10 ), new DateTime( 2000, 1, 20 ) ) );
			periods.Add( new TimeRange( new DateTime( 2000, 1, 15 ), new DateTime( 2000, 1, 25 ) ) );
			periods.Add( new TimeRange( new DateTime( 2000, 1, 18 ), new DateTime( 2000, 1, 30 ) ) );

			Console.WriteLine( "Common intersection: " + GetCommonIntersection( periods ) );
		} // CommonIntersectionSample

		// ----------------------------------------------------------------------
		private ITimePeriod GetCommonIntersection( ITimePeriodCollection periods )
		{
			if ( periods.Count == 0 )
			{
				return null;
			}

			ITimeRange timeRange = new TimeRange( periods[ 0 ] );
			if ( periods.Count > 1 )
			{
				for ( int i = 1; i < periods.Count; i++ )
				{
					timeRange = timeRange.GetIntersection( periods[ i ] );
					if ( timeRange == null )
					{
						return null;
					}
				}
			}

			return timeRange;
		} // GetCommonIntersection

		#endregion

		#region FiscalYearWeekCountSample

		[Sample( "FiscalYearWeekCountSample" )]
		// ----------------------------------------------------------------------
		public void FiscalYearWeekCountSample()
		{
			TimeCalendar calendar = new TimeCalendar(
				new TimeCalendarConfig
				{
					YearBaseMonth = YearMonth.April,  //  April year base month
					//YearWeekType = YearWeekType.Iso8601, // ISO 8601 week numbering
					YearType = YearType.FiscalYear// treat years as fiscal years
				} );

			bool countOnlyFullWeeks = true;
			Year year = new Year( 2012, calendar );
			foreach ( Month month in year.GetMonths() )
			{
				Month nextMonth = month.GetNextMonth();
				Week week = new Week( month.Start.Date );
				int weekCount = 0;
				while ( week.Start.Date < nextMonth.Start.Date )
				{
					if ( countOnlyFullWeeks )
					{
						if ( month.HasInside( week ) )
						{
							weekCount++;
						}
					}
					else
					{
						weekCount++;
					}
					week = week.GetNextWeek();
				}
				Console.WriteLine( "Month: " + month + ", Tot Weeks : " + weekCount );
			}
		} // FiscalYearWeekCountSample

		#endregion

		#region GetOfLastQuarterMonthSample

		[Sample( "GetDayOfLastQuarterMonthSample" )]
		// ----------------------------------------------------------------------
		public void CheckDayOfLastQuarterMonth()
		{
			DateTime? day = GetDayOfLastQuarterMonth( DayOfWeek.Friday, 3 );
			if ( day.HasValue && day.Equals( DateTime.Now.Date ) )
			{
				// do ...
			}
		} // CheckDayOfLastQuarterMonth

		// ----------------------------------------------------------------------
		public DateTime? GetDayOfLastQuarterMonth( DayOfWeek dayOfWeek, int count )
		{
			Quarter quarter = new Quarter();
			Month lastMonthOfQuarter = new Month( quarter.End.Date );

			DateTime? searchDay = null;
			foreach ( Day day in lastMonthOfQuarter.GetDays() )
			{
				if ( day.DayOfWeek == dayOfWeek )
				{
					count--;
					if ( count == 0 )
					{
						searchDay = day.Start.Date;
						break;
					}
				}
			}
			return searchDay;
		} // GetDayOfLastQuarterMonth

		#endregion

		#region TimeGapPrecisionSample

		[Sample( "TimeGapPrecisionSample" )]
		// ----------------------------------------------------------------------
		public void TimeGapPrecisionSample()
		{
			TimePeriodCollection periods = new TimePeriodCollection();
			periods.Add( new TimeRange( new DateTime( 2012, 3, 09, 0, 0, 00 ), new DateTime( 2012, 3, 09, 23, 59, 59 ) ) );
			periods.Add( new TimeRange( new DateTime( 2012, 3, 10, 0, 0, 00 ), new DateTime( 2012, 3, 10, 23, 59, 59 ) ) );
			periods.Add( new TimeRange( new DateTime( 2012, 3, 11, 0, 0, 10 ), new DateTime( 2012, 3, 11, 23, 59, 59 ) ) );
			periods.Add( new TimeRange( new DateTime( 2012, 3, 12, 0, 0, 00 ), new DateTime( 2012, 3, 12, 23, 59, 59 ) ) );

			// calculate the gaps using the time calendar as period mapper
			TimeGapCalculator<TimeRange> gapCalculator =
				new TimeGapCalculator<TimeRange>( new TimeCalendar( new TimeCalendarConfig { EndOffset = TimeSpan.Zero } ) );
			ITimePeriodCollection freeTimes =
				gapCalculator.GetGaps( periods );

			foreach ( TimeRange freeTime in freeTimes )
			{
				Console.WriteLine( "free time : " + freeTime );
			}
		} // TimeGapPrecisionSample

		#endregion

		#region AddBusinessDaysSample

		// ----------------------------------------------------------------------
		public DateTime AddBusinessDays1( DateTime startDate, int noOfBusinessDays )
		{
			CalendarVisitorFilter filter = new CalendarVisitorFilter();
			filter.AddWorkingWeekDays();

			DaySeeker seeker = new DaySeeker( filter );
			Day targetDay = seeker.FindDay( new Day( startDate ), noOfBusinessDays );
			return targetDay.Start;
		} // AddBusinessDays1

		// ----------------------------------------------------------------------
		public DateTime AddBusinessDays2( DateTime startDate, int noOfBusinessDays )
		{
			CalendarDateAdd dateAdd = new CalendarDateAdd();
			dateAdd.AddWorkingWeekDays();
			DateTime? targetDate = dateAdd.Add( startDate.Date.AddTicks( 1 ), new TimeSpan( noOfBusinessDays, 0, 0, 0, 0 ) );
			return targetDate.HasValue ? targetDate.Value.Date : startDate.Date;
		} // AddBusinessDays2

		[Sample( "AddBusinessDaysSample" )]
		// ----------------------------------------------------------------------
		public void AddBusinessDaysSample()
		{
			DateTime startDate = new DateTime( 2012, 6, 9, 18, 30, 45 );
			const int noOfBusinessDays = 1;

			DateTime targetDate1 = AddBusinessDays1( startDate, noOfBusinessDays );
			Console.WriteLine( "start date : " + startDate );
			Console.WriteLine( "number of business days : " + noOfBusinessDays );
			Console.WriteLine( "target date : " + targetDate1 );

			DateTime targetDate2 = AddBusinessDays2( startDate, noOfBusinessDays );
			Console.WriteLine( "start date : " + startDate );
			Console.WriteLine( "number of business days : " + noOfBusinessDays );
			Console.WriteLine( "target date : " + targetDate2 );
		} // AddBusinessDaysSample

		// ----------------------------------------------------------------------
		public DateTime AddBusinessDays3( DateTime startDate, int noOfBusinessDays )
		{
			TimeCalendar timeCalendar = new TimeCalendar( new TimeCalendarConfig { EndOffset = TimeSpan.Zero } );
			CalendarDateAdd dateAdd = new CalendarDateAdd( timeCalendar );
			dateAdd.AddWorkingWeekDays();

			// Exclude holidays (simplified from the actual production code, here reduced to one hardcoded day)
			dateAdd.ExcludePeriods.Add( new Day( 2012, 6, 22, timeCalendar ) );
			DateTime? targetDate = dateAdd.Add( startDate.Date.AddTicks( 1 ), new TimeSpan( noOfBusinessDays, 0, 0, 0, 0 ) );
			return targetDate.HasValue ? targetDate.Value.Date : startDate.Date;
		} // AddBusinessDays3

		[Sample( "AddBusinessDaysSample2" )]
		// ----------------------------------------------------------------------
		public void AddBusinessDaysSample2()
		{
			DateTime startDate = new DateTime( 2012, 6, 22 );

			int noOfBusinessDays = 0;
			Console.WriteLine( string.Format( "{0} + {1} day: {2}", startDate, noOfBusinessDays, AddBusinessDays3( startDate, noOfBusinessDays ) ) );

			noOfBusinessDays = 1;
			Console.WriteLine( string.Format( "{0} + {1} day: {2}", startDate, noOfBusinessDays, AddBusinessDays3( startDate, noOfBusinessDays ) ) );

			startDate = new DateTime( 2012, 6, 21 );
			noOfBusinessDays = 2;
			Console.WriteLine( string.Format( "{0} + {1} day: {2}", startDate, noOfBusinessDays, AddBusinessDays3( startDate, noOfBusinessDays ) ) );

			noOfBusinessDays = 3;
			Console.WriteLine( string.Format( "{0} + {1} day: {2}", startDate, noOfBusinessDays, AddBusinessDays3( startDate, noOfBusinessDays ) ) );
		} // AddBusinessDaysSample2

		#endregion

		#region MonthNameSample

		// ----------------------------------------------------------------------
		public void ShowMonthNameInfo( DateTimeFormatInfo info, int month )
		{
			Console.WriteLine( "Current month name: " + info.GetMonthName( month ) );
			Console.WriteLine( "Current abbreviated month name: " + info.GetAbbreviatedMonthName( month ) );
			Console.WriteLine( "Current month index: " + info.MonthNames, info.GetMonthName( month ) );
		} // ShowMonthNameInfo

		[Sample( "MonthNameSample" )]
		// ----------------------------------------------------------------------
		public void MonthNameSample()
		{
			DateTime now = DateTime.Now;
			// current culture
			ShowMonthNameInfo( DateTimeFormatInfo.CurrentInfo, now.Month );
			// culture EN-us
			ShowMonthNameInfo( new CultureInfo( "EN-us" ).DateTimeFormat, now.Month );
			// culture De-de
			ShowMonthNameInfo( new CultureInfo( "DE-de" ).DateTimeFormat, now.Month );
		} // MonthNameSample

		#endregion

		#region WeekStartEndSample

		[Sample( "WeekStartEndSample" )]
		// ----------------------------------------------------------------------
		public void WeekStartEndSample()
		{
			Week week = new Week( new DateTime( 2012, 6, 1 ) );
			while ( week.Start.Month < 7 )
			{
				Console.WriteLine( "week : " + week );
				week = week.GetNextWeek();
			}
			// > week : w/c 22 2012; 28.05.2012 - 03.06.2012 | 6.23:59
			// > week : w/c 23 2012; 04.06.2012 - 10.06.2012 | 6.23:59
			// > week : w/c 24 2012; 11.06.2012 - 17.06.2012 | 6.23:59
			// > week : w/c 25 2012; 18.06.2012 - 24.06.2012 | 6.23:59
			// > week : w/c 26 2012; 25.06.2012 - 01.07.2012 | 6.23:59
		} // WeekStartEndSample

		#endregion

		#region CalendarWeekSample

		// ----------------------------------------------------------------------
		// see also http://blogs.msdn.com/b/shawnste/archive/2006/01/24/517178.aspx
		[Sample( "CalendarWeekSample" )]
		public void CalendarWeekSample()
		{
			DateTime testDate = new DateTime( 2007, 12, 31 );

			// .NET calendar week
			TimeCalendar calendar = new TimeCalendar();
			Console.WriteLine( "Calendar Week of {0}: {1}", testDate.ToShortDateString(),
												 new Week( testDate, calendar ).WeekOfYear );
			// > Calendar Week of 31.12.2007: 53

			// ISO 8601 calendar week

			CultureInfo culture = new CultureInfo( "EN-us" );
			culture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
			culture.DateTimeFormat.CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek;
			TimeCalendar calendarIso8601 = new TimeCalendar(
				new TimeCalendarConfig { YearWeekType = YearWeekType.Iso8601, Culture = culture } );
			Console.WriteLine( "ISO 8601 Week of {0}: {1}", testDate.ToShortDateString(),
												 new Week( testDate, calendarIso8601 ).WeekOfYear );
			// > ISO 8601 Week of 31.12.2007: 1
		} // CalendarWeekSample

		#endregion

		#region WeekCountSample

		// ----------------------------------------------------------------------
		[Sample( "WeekCountSample" )]
		public void WeekCountSample()
		{
			DateTime startDateTime = new DateTime( 2004, 01, 01 );
			DateTime endDateTime = new DateTime( 2004, 12, 31 );

			// .NET calendar week
			Console.WriteLine( "Week count between {0} and {1}: {2}",
				startDateTime.ToShortDateString(),
				endDateTime.ToShortDateString(),
				CalcWeekCount( startDateTime, endDateTime ) );
			// > Week count between 01.01.2004 and 31.12.2004: 53
		} // WeekCountSample

		// ----------------------------------------------------------------------
		private static int CalcWeekCount( DateTime startDateTime, DateTime endDateTime )
		{
			const DayOfWeek firstDayOfWeek = DayOfWeek.Monday;
			CultureInfo culture = new CultureInfo( "en-US" );
			culture.DateTimeFormat.FirstDayOfWeek = firstDayOfWeek;
			culture.DateTimeFormat.CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek;

			DateTime startWeek = TimeTool.GetStartOfWeek( startDateTime, firstDayOfWeek );
			DateTime endWeek = TimeTool.GetStartOfWeek( endDateTime, firstDayOfWeek );
			if ( startWeek.Equals( endWeek ) )
			{
				return 0;
			}

			return new DateDiff( startWeek, endWeek, culture.Calendar, firstDayOfWeek ).Weeks + 1;
		} // CalcWeekCount

		#endregion

		#region TimeLineSample

		// ----------------------------------------------------------------------
		[Sample( "TimeLineMomentSample" )]
		public void TimeLineMomentSample()
		{
			TimePeriodCollection periods = new TimePeriodCollection();
			periods.Add( new TimeRange( new DateTime( 2012, 09, 03 ), new DateTime( 2012, 09, 05, 23, 59, 59, 999 ) ) );
			periods.Add( new TimeRange( new DateTime( 2012, 09, 04 ), new DateTime( 2012, 09, 06, 23, 59, 59, 999 ) ) );
			TimeSpan duration = new TimeSpan( 4, 0, 0, 0 );
			Console.WriteLine( "Target moment: {0}", CalcTargetMoment( periods, duration ) );
			Console.WriteLine( "Target moment: {0}", new TargetMomentCalculator( periods, duration ).GetTargetMoment() );
		} // TimeLineMomentSample

		// ----------------------------------------------------------------------
		private static DateTime? CalcTargetMoment( ICollection<ITimePeriod> periods, TimeSpan duration )
		{
			DateTime? targetMoment = null;
			if ( periods.Count > 0 && duration.Ticks > 0 )
			{
				TimeLineMomentCollection timeLineMoments = new TimeLineMomentCollection();
				timeLineMoments.AddAll( periods );
				if ( timeLineMoments.Count > 2 )
				{
					int balance = 0;
					for ( int i = 0; i < timeLineMoments.Count - 1; i++ )
					{
						ITimeLineMoment start = timeLineMoments[ i ];
						ITimeLineMoment end = timeLineMoments[ i + 1 ];

						if ( i == 0 )
						{
							balance += start.StartCount;
							balance -= start.EndCount;
						}
						TimeRange slot = new TimeRange( start.Moment, end.Moment );
						TimeSpan slotDuration = new TimeSpan( slot.Duration.Ticks * balance );

						if ( slotDuration >= duration )
						{
							targetMoment = start.Moment.Add( new TimeSpan( duration.Ticks / balance ) );
							break;
						}

						duration = duration.Subtract( slotDuration );
						balance += end.StartCount;
						balance -= end.EndCount;
					}
				}
			}

			return targetMoment;
		} // CalcTargetMoment

		// ----------------------------------------------------------------------
		private static DateTime? CalcTargetMoment2( ICollection<ITimePeriod> periods, TimeSpan duration )
		{
			DateTime? targetMoment = null;
			if ( periods.Count > 0 && duration.Ticks > 0 )
			{
				TimeLineMomentCollection timeLineMoments = new TimeLineMomentCollection();
				timeLineMoments.AddAll( periods );
				if ( timeLineMoments.Count > 2 )
				{
					int balance = 0;
					for ( int i = 0; i < timeLineMoments.Count - 1; i++ )
					{
						ITimeLineMoment start = timeLineMoments[ i ];
						ITimeLineMoment end = timeLineMoments[ i + 1 ];

						if ( i == 0 )
						{
							balance += start.StartCount;
							balance -= start.EndCount;
						}
						TimeRange slot = new TimeRange( start.Moment, end.Moment );
						TimeSpan slotDuration = new TimeSpan( slot.Duration.Ticks * balance );

						if ( slotDuration >= duration )
						{
							targetMoment = start.Moment.Add( new TimeSpan( duration.Ticks / balance ) );
							break;
						}

						duration = duration.Subtract( slotDuration );
						balance += end.StartCount;
						balance -= end.EndCount;
					}
				}
			}

			return targetMoment;
		} // CalcTargetMoment2

		#endregion

	} // class CommunitySamples

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
