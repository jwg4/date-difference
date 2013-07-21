// -- FILE ------------------------------------------------------------------
// name       : TimeLineMoment.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.31
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public class TimeLineMoment : ITimeLineMoment
	{

		// ----------------------------------------------------------------------
		public TimeLineMoment( DateTime moment )
		{
			this.moment = moment;
		} // TimeLineMoment

		// ----------------------------------------------------------------------
		public DateTime Moment
		{
			get { return moment; }
		} // Moment

		// ----------------------------------------------------------------------
		public ITimePeriodCollection Periods
		{
			get { return periods; }
		} // Periods

		// ----------------------------------------------------------------------
		public int StartCount
		{
			get
			{
				int startCount = 0;
				foreach ( ITimePeriod period in periods )
				{
					if ( moment.Equals( period.Start ) )
					{
						startCount++;
					}
				}
				return startCount;
			}
		} // StartCount

		// ----------------------------------------------------------------------
		public int EndCount
		{
			get
			{
				int endCount = 0;
				foreach ( ITimePeriod period in periods )
				{
					if ( moment.Equals( period.End ) )
					{
						endCount++;
					}
				}
				return endCount;
			}
		} // StartCount

		// ----------------------------------------------------------------------
		public override string ToString()
		{
			return Moment + "[" + StartCount + "/" + EndCount + "]";
		} // ToString

		// ----------------------------------------------------------------------
		// members
		private readonly ITimePeriodCollection periods = new TimePeriodCollection();
		private readonly DateTime moment;

	} // class TimeLineMoment

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
