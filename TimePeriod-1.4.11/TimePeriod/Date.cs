// -- FILE ------------------------------------------------------------------
// name       : Date.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.08.24
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public struct Date
	{

		// ----------------------------------------------------------------------
		public Date( DateTime date )
		{
			this.date = date.Date;
		} // Date

		// ----------------------------------------------------------------------
		public Date( int year, int month = 1, int day = 1 ) 
		{
			if ( year < DateTime.MinValue.Year || year > DateTime.MaxValue.Year )
			{
				throw new ArgumentOutOfRangeException( "year" );
			}
			if ( month <= 0 || month > TimeSpec.MonthsPerYear )
			{
				throw new ArgumentOutOfRangeException( "month" );
			}
			if ( day <= 0 || day > TimeSpec.MaxDaysPerMonth )
			{
				throw new ArgumentOutOfRangeException( "day" );
			}
			date = new DateTime( year, month, day );
		} // Date

		// ----------------------------------------------------------------------
		public int Year
		{
			get { return date.Year; }
		} // Year

		// ----------------------------------------------------------------------
		public int Month
		{
			get { return date.Month; }
		} // Month

		// ----------------------------------------------------------------------
		public int Day
		{
			get { return date.Day; }
		} // Day

		// ----------------------------------------------------------------------
		public DateTime GetDateTime()
		{
			return date;
		} // GetDateTime

		// ----------------------------------------------------------------------
		public DateTime GetDateTime( Time time )
		{
			return date.Add( time.Duration );
		} // GetDateTime

		// ----------------------------------------------------------------------
		public DateTime GetDateTime( int hour = 0, int minute = 0, int second = 0, int millisecond = 0 )
		{
			return new DateTime( Year, Month, Day, hour, minute, second, millisecond );
		} // GetDateTime

		// ----------------------------------------------------------------------
		public override string ToString()
		{
			return date.ToString( "d" ); // only the date part
		} // ToString

		// ----------------------------------------------------------------------
		public override bool Equals( object obj )
		{
			if ( obj == null || GetType() != obj.GetType() )
			{
				return false;
			}

			Date comp = (Date)obj;
			return date == comp.date;
		} // Equals

		// ----------------------------------------------------------------------
		public override int GetHashCode()
		{
			return HashTool.ComputeHashCode( GetType().GetHashCode(), date );
		} // GetHashCode

		// ----------------------------------------------------------------------
		// members
		private readonly DateTime date;

	} // struct Date

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
