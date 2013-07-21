// -- FILE ------------------------------------------------------------------
// name       : ITimeLineMoment.cs
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
	public interface ITimeLineMoment
	{

		// ----------------------------------------------------------------------
		DateTime Moment { get; }

		// ----------------------------------------------------------------------
		ITimePeriodCollection Periods { get; }

		// ----------------------------------------------------------------------
		int StartCount { get; }

		// ----------------------------------------------------------------------
		int EndCount { get; }

	} // interface ITimeLineMoment

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
