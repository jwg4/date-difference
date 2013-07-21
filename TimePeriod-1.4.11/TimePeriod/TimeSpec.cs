// -- FILE ------------------------------------------------------------------
// name       : TimeSpec.cs
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
	public static class TimeSpec
	{

		// ----------------------------------------------------------------------
		// relations
		public const int MonthsPerYear = 12;
		public const int HalfyearsPerYear = 2;
		public const int QuartersPerYear = 4;
		public const int QuartersPerHalfyear = QuartersPerYear / HalfyearsPerYear;
		public const int MaxWeeksPerYear = 53;
		public const int MonthsPerHalfyear = MonthsPerYear / HalfyearsPerYear;
		public const int MonthsPerQuarter = MonthsPerYear / QuartersPerYear;
		public const int MaxDaysPerMonth = 31;
		public const int DaysPerWeek = 7;
		public const int HoursPerDay = 24;
		public const int MinutesPerHour = 60;
		public const int SecondsPerMinute = 60;
		public const int MillisecondsPerSecond = 1000;

		public const YearMonth CalendarYearStartMonth = YearMonth.January;
		public const DayOfWeek FirstWorkingDayOfWeek = DayOfWeek.Monday;

		// ----------------------------------------------------------------------
		// halfyear
		public static YearMonth[] FirstHalfyearMonths = new[] { YearMonth.January, YearMonth.February, YearMonth.March, YearMonth.April, YearMonth.May, YearMonth.June };
		public static YearMonth[] SecondHalfyearMonths = new[] { YearMonth.July, YearMonth.August, YearMonth.September, YearMonth.October, YearMonth.November, YearMonth.December };

		// ----------------------------------------------------------------------
		// quarter
		public const int FirstQuarterMonthIndex = 1;
		public const int SecondQuarterMonthIndex = FirstQuarterMonthIndex + MonthsPerQuarter;
		public const int ThirdQuarterMonthIndex = SecondQuarterMonthIndex + MonthsPerQuarter;
		public const int FourthQuarterMonthIndex = ThirdQuarterMonthIndex + MonthsPerQuarter;

		public static YearMonth[] FirstQuarterMonths = new[] { YearMonth.January, YearMonth.February, YearMonth.March };
		public static YearMonth[] SecondQuarterMonths = new[] { YearMonth.April, YearMonth.May, YearMonth.June };
		public static YearMonth[] ThirdQuarterMonths = new[] { YearMonth.July, YearMonth.August, YearMonth.September };
		public static YearMonth[] FourthQuarterMonths = new[] { YearMonth.October, YearMonth.November, YearMonth.December };

		// ----------------------------------------------------------------------
		// duration
		public static readonly TimeSpan NoDuration = TimeSpan.Zero;
		public static readonly TimeSpan MinPositiveDuration = new TimeSpan( 1 ); // positive tick;
		public static readonly TimeSpan MinNegativeDuration = new TimeSpan( -1 ); // negative tick;

		// ----------------------------------------------------------------------
		// period
		public static readonly DateTime MinPeriodDate = DateTime.MinValue;
		public static readonly DateTime MaxPeriodDate = DateTime.MaxValue;
		public static readonly TimeSpan MinPeriodDuration = TimeSpan.Zero;
		public static readonly TimeSpan MaxPeriodDuration = MaxPeriodDate - MinPeriodDate;

	} // class TimeSpec

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
