// -- FILE ------------------------------------------------------------------
// name       : TimeTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.04.12
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
	public sealed class TimeTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void ConstructorTest()
		{
			Time time = new Time( 18, 23, 56, 344 );

			Assert.AreEqual( time.Hour, 18 );
			Assert.AreEqual( time.Minute, 23 );
			Assert.AreEqual( time.Second, 56 );
			Assert.AreEqual( time.Millisecond, 344 );
		} // ConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void ConstructorHourTest()
		{
			Time time = new Time( 18 );

			Assert.AreEqual( time.Hour, 18 );
			Assert.AreEqual( time.Minute, 0 );
			Assert.AreEqual( time.Second, 0 );
			Assert.AreEqual( time.Millisecond, 0 );
		} // ConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void ConstructorMinuteTest()
		{
			Time time = new Time( 18, 23 );

			Assert.AreEqual( time.Hour, 18 );
			Assert.AreEqual( time.Minute, 23 );
			Assert.AreEqual( time.Second, 0 );
			Assert.AreEqual( time.Millisecond, 0 );
		} // ConstructorMinuteTest

		// ----------------------------------------------------------------------
		[Test]
		public void ConstructorSecondTest()
		{
			Time time = new Time( 18, 23, 56 );

			Assert.AreEqual( time.Hour, 18 );
			Assert.AreEqual( time.Minute, 23 );
			Assert.AreEqual( time.Second, 56 );
			Assert.AreEqual( time.Millisecond, 0 );
		} // ConstructorSecondTest

		// ----------------------------------------------------------------------
		[Test]
		public void EmptyConstructorTest()
		{
			Time time = new Time();

			Assert.AreEqual( time.Hour, 0 );
			Assert.AreEqual( time.Minute, 0 );
			Assert.AreEqual( time.Second, 0 );
			Assert.AreEqual( time.Millisecond, 0 );
			Assert.AreEqual( time.Ticks, 0 );
			Assert.AreEqual( time.Duration, TimeSpan.Zero );
			Assert.AreEqual( time.TotalHours, 0 );
			Assert.AreEqual( time.TotalMinutes, 0 );
			Assert.AreEqual( time.TotalSeconds, 0 );
			Assert.AreEqual( time.TotalMilliseconds, 0 );
		} // EmptyConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void DateTimeConstructorTest()
		{
			DateTime test = new DateTime( 2009, 7, 22, 18, 23, 56, 344 );
			Time time = new Time( test );

			Assert.AreEqual( time.Hour, test.Hour );
			Assert.AreEqual( time.Minute, test.Minute );
			Assert.AreEqual( time.Second, test.Second );
			Assert.AreEqual( time.Millisecond, test.Millisecond );
		} // DateTimeConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void EmptyDateTimeConstructorTest()
		{
			DateTime test = new DateTime( 2009, 7, 22 );
			Time time = new Time( test );

			Assert.AreEqual( time.Hour, 0 );
			Assert.AreEqual( time.Minute, 0 );
			Assert.AreEqual( time.Second, 0 );
			Assert.AreEqual( time.Millisecond, 0 );
			Assert.AreEqual( time.Ticks, 0 );
			Assert.AreEqual( time.Duration, TimeSpan.Zero );
			Assert.AreEqual( time.TotalHours, 0 );
			Assert.AreEqual( time.TotalMinutes, 0 );
			Assert.AreEqual( time.TotalSeconds, 0 );
			Assert.AreEqual( time.TotalMilliseconds, 0 );
		} // EmptyDateTimeConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void MinValueTest()
		{
			new Time( 0 );
		} // MinValueTest

		// ----------------------------------------------------------------------
		[Test]
		public void MaxValueTest()
		{
			new Time( TimeSpec.HoursPerDay - 1, TimeSpec.MinutesPerHour - 1, TimeSpec.SecondsPerMinute - 1, TimeSpec.MillisecondsPerSecond - 1 );
		} // MinValueTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MinHourTest()
		{
			new Time( -1 );
		} // MinHourTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MaxHourTest()
		{
			new Time( TimeSpec.HoursPerDay );
		} // MaxHourTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MinMinuteTest()
		{
			new Time( 0, -1 );
		} // MinMinuteTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MaxMinuteTest()
		{
			new Time( 0, TimeSpec.MinutesPerHour );
		} // MaxMinuteTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MinSecondTest()
		{
			new Time( 0, 0, -1 );
		} // MinSecondTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MaxSecondTest()
		{
			new Time( 0, 0, TimeSpec.SecondsPerMinute );
		} // MaxSecondTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MinMillisecondTest()
		{
			new Time( 0, 0, 0, -1 );
		} // MinMillisecondTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MaxMillisecondTest()
		{
			new Time( 0, 0, 0, TimeSpec.MillisecondsPerSecond );
		} // MaxMillisecondTest
		// ----------------------------------------------------------------------
		[Test]
		public void DurationTest()
		{
			TimeSpan test = new TimeSpan( 0, 18, 23, 56, 344 );
			Time time = new Time( test.Hours, test.Minutes, test.Seconds, test.Milliseconds );

			Assert.AreEqual( time.Hour, test.Hours );
			Assert.AreEqual( time.Minute, test.Minutes );
			Assert.AreEqual( time.Second, test.Seconds );
			Assert.AreEqual( time.Millisecond, test.Milliseconds );
			
			Assert.AreEqual( time.Duration.Ticks, test.Ticks );

			Assert.AreEqual( time.TotalHours, test.TotalHours );
			Assert.AreEqual( time.TotalMinutes, test.TotalMinutes );
			Assert.AreEqual( time.TotalSeconds, test.TotalSeconds );
			Assert.AreEqual( time.TotalMilliseconds, test.TotalMilliseconds );
		} // DurationTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetDateTimeTest()
		{
			DateTime dateTime = new DateTime( 2009, 7, 22 );
			TimeSpan timeSpan = new TimeSpan( 0, 18, 23, 56, 344 );
			Time time = new Time( timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds );

			Assert.AreEqual( time.GetDateTime( dateTime ), dateTime.Add( timeSpan ) );
		} // GetDateTimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetDateTimeFromDateTest()
		{
			Date date = new Date( 2009, 7, 22 );
			TimeSpan timeSpan = new TimeSpan( 0, 18, 23, 56, 344 );
			Time time = new Time( timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds );

			Assert.AreEqual( time.GetDateTime( date ), date.GetDateTime().Add( timeSpan ) );
		} // GetDateTimeFromDateTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetEmptyDateTimeTest()
		{
			DateTime dateTime = new DateTime( 2009, 7, 22 );
			Time time = new Time();

			Assert.AreEqual( time.GetDateTime( dateTime ), dateTime );
		} // GetEmptyDateTimeTest

	} // class TimeTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
