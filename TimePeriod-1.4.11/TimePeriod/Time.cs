// -- FILE ------------------------------------------------------------------
// name       : Time.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.04.10
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public struct Time
	{

		// ----------------------------------------------------------------------
		public Time( DateTime dateTime )
		{
			duration = dateTime.TimeOfDay;
		} // Time

		// ----------------------------------------------------------------------
		public Time( int hour = 0, int minute = 0, int second = 0, int millisecond = 0 ) 
		{
			if ( hour < 0 || hour >= TimeSpec.HoursPerDay )
			{
				throw new ArgumentOutOfRangeException( "hour" );
			}
			if ( minute < 0 || minute >= TimeSpec.MinutesPerHour )
			{
				throw new ArgumentOutOfRangeException( "minute" );
			}
			if ( second < 0 || second >= TimeSpec.SecondsPerMinute )
			{
				throw new ArgumentOutOfRangeException( "second" );
			}
			if ( millisecond < 0 || millisecond >= TimeSpec.MillisecondsPerSecond )
			{
				throw new ArgumentOutOfRangeException( "millisecond" );
			}

			duration = new TimeSpan( 0, hour, minute, second, millisecond );
		} // Time

		// ----------------------------------------------------------------------
		public int Hour
		{
			get { return duration.Hours; }
		} // Hour

		// ----------------------------------------------------------------------
		public int Minute
		{
			get { return duration.Minutes; }
		} // Minute

		// ----------------------------------------------------------------------
		public int Second
		{
			get { return duration.Seconds; }
		} // Second

		// ----------------------------------------------------------------------
		public int Millisecond
		{
			get { return duration.Milliseconds; }
		} // Millisecond

		// ----------------------------------------------------------------------
		public TimeSpan Duration
		{
			get { return duration; }
		} // Duration

		// ----------------------------------------------------------------------
		public long Ticks
		{
			get { return duration.Ticks; }
		} // Ticks

		// ----------------------------------------------------------------------
		public double TotalHours
		{
			get { return duration.TotalHours; }
		} // TotalHours

		// ----------------------------------------------------------------------
		public double TotalMinutes
		{
			get { return duration.TotalMinutes; }
		} // TotalMinutes

		// ----------------------------------------------------------------------
		public double TotalSeconds
		{
			get { return duration.TotalSeconds; }
		} // TotalSeconds

		// ----------------------------------------------------------------------
		public double TotalMilliseconds
		{
			get { return duration.TotalMilliseconds; }
		} // TotalMilliseconds

		// ----------------------------------------------------------------------
		public DateTime GetDateTime( DateTime dateTime )
		{
			return dateTime.Date.Add( duration );
		} // GetDateTime

		// ----------------------------------------------------------------------
		public DateTime GetDateTime( Date date )
		{
			return date.GetDateTime( this );
		} // GetDateTime

		// ----------------------------------------------------------------------
		public override string ToString()
		{
			if ( Millisecond == 0 )
			{
				if ( Second == 0 )
				{
					return Hour.ToString( "00" ) + ":" + Minute.ToString( "00" );
				}
				return Hour.ToString( "00" ) + ":" + Minute.ToString( "00" ) + ":" + Second.ToString( "00" );
			}
			return duration.ToString();
		} // ToString

		// ----------------------------------------------------------------------
		public override bool Equals( object obj )
		{
			if ( obj == null || GetType() != obj.GetType() )
			{
				return false;
			}

			Time comp = (Time)obj;
			return duration == comp.duration;
		} // Equals

		// ----------------------------------------------------------------------
		public override int GetHashCode()
		{
			return HashTool.ComputeHashCode( GetType().GetHashCode(), duration );
		} // GetHashCode

		// ----------------------------------------------------------------------
		// members
		private readonly TimeSpan duration;

	} // struct Time

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
