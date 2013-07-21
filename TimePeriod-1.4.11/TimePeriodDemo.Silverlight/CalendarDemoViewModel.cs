// -- FILE ------------------------------------------------------------------
// name       : CalendarDemoViewModel.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class CalendarDemoViewModel : DependencyObject, INotifyPropertyChanged
	{

		// ----------------------------------------------------------------------
		public event PropertyChangedEventHandler PropertyChanged;

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty SelectedYearBaseMonthProperty = DependencyProperty.Register(
			"SelectedYearBaseMonth",
			typeof( YearMonth ),
			typeof( CalendarDemoViewModel ),
			new PropertyMetadata( YearMonth.January, CalendarChanged ) );

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty SelectedPeriodModeProperty = DependencyProperty.Register(
			"SelectedPeriodMode",
			typeof( TimePeriodMode ),
			typeof( CalendarDemoViewModel ),
			new PropertyMetadata( CalendarChanged ) );

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty WorkingDateProperty = DependencyProperty.Register(
			"WorkingDate",
			typeof( DateTime ),
			typeof( CalendarDemoViewModel ),
			new PropertyMetadata( CalendarChanged ) );

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty WorkingTimeProperty = DependencyProperty.Register(
			"WorkingTime",
			typeof( DateTime ),
			typeof( CalendarDemoViewModel ),
			new PropertyMetadata( CalendarChanged ) );

		// ----------------------------------------------------------------------
		public CalendarDemoViewModel()
		{
			WorkingDate = DateTime.Now;

			UpdateSelectedPeriodInfo();
		} // CalendarDemoViewModel

		// ----------------------------------------------------------------------
		public IEnumerable<YearMonth> YearMonths
		{
			get { return yearMonths; }
		} // YearMonths

		// ----------------------------------------------------------------------
		public IEnumerable<TimePeriodMode> PeriodModes
		{
			get { return periodModes; }
		} // PeriodModes

		// ----------------------------------------------------------------------
		public YearMonth SelectedYearBaseMonth
		{
			get { return (YearMonth)GetValue( SelectedYearBaseMonthProperty ); }
			set { SetValue( SelectedYearBaseMonthProperty, value ); }
		} // SelectedYearBaseMonth

		// ----------------------------------------------------------------------
		public TimePeriodMode SelectedPeriodMode
		{
			get { return (TimePeriodMode)GetValue( SelectedPeriodModeProperty ); }
			set { SetValue( SelectedPeriodModeProperty, value ); }
		} // SelectedPeriodMode

		// ----------------------------------------------------------------------
		public DateTime WorkingDate
		{
			get { return (DateTime)GetValue( WorkingDateProperty ); }
			set { SetValue( WorkingDateProperty, value ); }
		} // WorkingDate

		// ----------------------------------------------------------------------
		public DateTime WorkingTime
		{
			get { return (DateTime)GetValue( WorkingTimeProperty ); }
			set { SetValue( WorkingTimeProperty, value ); }
		} // WorkingTime

		// ----------------------------------------------------------------------
		public DateTime WorkingDateTime
		{
			get { return workingDateTime; }
			private set
			{
				if ( value == workingDateTime )
				{
					return;
				}
				workingDateTime = value;
				NotifyPropertyChanged( "WorkingDateTime" );
			}
		} // WorkingDateTime

		// ----------------------------------------------------------------------
		public string SelectedPeriodInfo
		{
			get { return selectedPeriodInfo; }
			private set
			{
				if ( value == selectedPeriodInfo )
				{
					return;
				}
				selectedPeriodInfo = value;
				NotifyPropertyChanged( "SelectedPeriodInfo" );
			}
		} // SelectedPeriodInfo

		// ----------------------------------------------------------------------
		private void UpdateSelectedPeriodInfo()
		{
			TimeCalendar timeCalendar = TimeCalendar.New( SelectedYearBaseMonth );
			switch ( SelectedPeriodMode )
			{
				case TimePeriodMode.Year:
					Year year = new Year( WorkingDateTime, timeCalendar );
					periodInfo.Clear();
					periodInfo.AddItem( "Year", year );
					periodInfo.AddItem( "YearBaseMonth", year.YearBaseMonth );
					periodInfo.AddItem( "IsCalendarYear", year.IsCalendarYear );
					periodInfo.AddItem( "StartYear", year.StartYear );
					periodInfo.AddItem( "FirstDayStart", year.FirstDayStart );
					periodInfo.AddItem( "LastDayStart", year.LastDayStart );
					periodInfo.AddItem( "LastMonthStart", year.LastMonthStart );
					periodInfo.AddItem( "YearName", year.YearName );
					periodInfo.AddSection( "Previous/Next" );
					periodInfo.AddItem( "Previous", year.GetPreviousYear() );
					periodInfo.AddItem( "Next", year.GetNextYear() );
					periodInfo.AddSubitems( "Halfyears", year.GetHalfyears() );
					periodInfo.AddSubitems( "Quarters", year.GetQuarters() );
					periodInfo.AddSubitems( "Months", year.GetMonths() );
					SelectedPeriodInfo = periodInfo.ToString();
					break;
				case TimePeriodMode.Halfyear:
					Halfyear halfyear = new Halfyear( WorkingDateTime, timeCalendar );
					periodInfo.Clear();
					periodInfo.AddItem( "Halfyear", halfyear );
					periodInfo.AddItem( "YearBaseMonth", halfyear.YearBaseMonth );
					periodInfo.AddItem( "StartMonth", halfyear.StartMonth );
					periodInfo.AddItem( "Year", halfyear.Year );
					periodInfo.AddItem( "YearHalfyear", halfyear.YearHalfyear );
					periodInfo.AddItem( "IsCalendarHalfyear", halfyear.IsCalendarHalfyear );
					periodInfo.AddItem( "MultipleCalendarYears", halfyear.MultipleCalendarYears );
					periodInfo.AddItem( "HalfyearName", halfyear.HalfyearName );
					periodInfo.AddItem( "HalfyearOfYearName", halfyear.HalfyearOfYearName );
					periodInfo.AddItem( "LastDayStart", halfyear.LastDayStart );
					periodInfo.AddItem( "LastMonthStart", halfyear.LastMonthStart );
					periodInfo.AddSection( "Previous/Next" );
					periodInfo.AddItem( "Previousr", halfyear.GetPreviousHalfyear() );
					periodInfo.AddItem( "Next", halfyear.GetNextHalfyear() );
					periodInfo.AddSubitems( "Quarters", halfyear.GetQuarters() );
					periodInfo.AddSubitems( "Months", halfyear.GetMonths() );
					SelectedPeriodInfo = periodInfo.ToString();
					break;
				case TimePeriodMode.Quarter:
					Quarter quarter = new Quarter( WorkingDateTime, timeCalendar );
					periodInfo.Clear();
					periodInfo.AddItem( "Quarter", quarter );
					periodInfo.AddItem( "YearBaseMonth", quarter.YearBaseMonth );
					periodInfo.AddItem( "StartMonth", quarter.StartMonth );
					periodInfo.AddItem( "Year", quarter.Year );
					periodInfo.AddItem( "YearQuarter", quarter.YearQuarter );
					periodInfo.AddItem( "IsCalendarQuarter", quarter.IsCalendarQuarter );
					periodInfo.AddItem( "MultipleCalendarYears", quarter.MultipleCalendarYears );
					periodInfo.AddItem( "QuarterName", quarter.QuarterName );
					periodInfo.AddItem( "QuarterOfYearName", quarter.QuarterOfYearName );
					periodInfo.AddItem( "LastDayStart", quarter.FirstDayStart );
					periodInfo.AddItem( "LastMonthStart", quarter.LastDayStart );
					periodInfo.AddSection( "Previous/Next" );
					periodInfo.AddItem( "Previous", quarter.GetPreviousQuarter() );
					periodInfo.AddItem( "Next", quarter.GetNextQuarter() );
					periodInfo.AddSubitems( "Months", quarter.GetMonths() );
					SelectedPeriodInfo = periodInfo.ToString();
					break;
				case TimePeriodMode.Month:
					Month month = new Month( WorkingDateTime, timeCalendar );
					periodInfo.Clear();
					periodInfo.AddItem( "Month", month );
					periodInfo.AddItem( "YearBaseMonth", month.YearMonth );
					periodInfo.AddItem( "Year", month.Year );
					periodInfo.AddItem( "DaysInMonth", month.DaysInMonth );
					periodInfo.AddItem( "MonthName", month.MonthName );
					periodInfo.AddItem( "MonthOfYearName", month.MonthOfYearName );
					periodInfo.AddItem( "LastDayStart", month.FirstDayStart );
					periodInfo.AddItem( "LastMonthStart", month.LastDayStart );
					periodInfo.AddSection( "Previous/Next" );
					periodInfo.AddItem( "Previous", month.GetPreviousMonth() );
					periodInfo.AddItem( "Next", month.GetNextMonth() );
					periodInfo.AddSubitems( "Days", month.GetDays() );
					SelectedPeriodInfo = periodInfo.ToString();
					break;
				case TimePeriodMode.Week:
					Week week = new Week( WorkingDateTime, timeCalendar );
					periodInfo.Clear();
					periodInfo.AddItem( "Week", week );
					periodInfo.AddItem( "MultipleCalendarYears", week.MultipleCalendarYears );
					periodInfo.AddItem( "FirstDayStart", week.FirstDayStart );
					periodInfo.AddItem( "FirstDayOfWeek", week.FirstDayOfWeek );
					periodInfo.AddItem( "LastDayStart", week.LastDayStart );
					periodInfo.AddItem( "LastDayOfWeek", week.LastDayOfWeek );
					periodInfo.AddItem( "WeekOfYear", week.WeekOfYear );
					periodInfo.AddItem( "WeekOfYearName", week.WeekOfYearName );
					periodInfo.AddSection( "Previous/Next" );
					periodInfo.AddItem( "Previous", week.GetPreviousWeek() );
					periodInfo.AddItem( "Next", week.GetNextWeek() );
					periodInfo.AddSubitems( "Days", week.GetDays() );
					SelectedPeriodInfo = periodInfo.ToString();
					break;
				case TimePeriodMode.Day:
					Day day = new Day( WorkingDateTime, timeCalendar );
					periodInfo.Clear();
					periodInfo.AddItem( "Day", day );
					periodInfo.AddItem( "Year", day.Year );
					periodInfo.AddItem( "FirstDayStart", day.FirstDayStart );
					periodInfo.AddItem( "Month", day.Month );
					periodInfo.AddItem( "DayValue", day.DayValue );
					periodInfo.AddItem( "DayOfWeek", day.DayOfWeek );
					periodInfo.AddItem( "DayName", day.DayName );
					periodInfo.AddItem( "FirstHourStart", day.FirstHourStart );
					periodInfo.AddItem( "LastHourStart", day.LastHourStart );
					periodInfo.AddSection( "Previous/Next" );
					periodInfo.AddItem( "Previous", day.GetPreviousDay() );
					periodInfo.AddItem( "Next", day.GetNextDay() );
					periodInfo.AddSubitems( "Hours", day.GetHours() );
					SelectedPeriodInfo = periodInfo.ToString();
					break;
				case TimePeriodMode.Hour:
					Hour hour = new Hour( WorkingDateTime, timeCalendar );
					periodInfo.Clear();
					periodInfo.AddItem( "Hour", hour );
					periodInfo.AddItem( "Year", hour.Year );
					periodInfo.AddItem( "Month", hour.Month );
					periodInfo.AddItem( "Day", hour.Day );
					periodInfo.AddItem( "HourValue", hour.HourValue );
					periodInfo.AddItem( "FirstMinuteStart", hour.FirstMinuteStart );
					periodInfo.AddItem( "LastMinuteStart", hour.LastMinuteStart );
					periodInfo.AddSection( "Previous/Next" );
					periodInfo.AddItem( "Previous", hour.GetPreviousHour() );
					periodInfo.AddItem( "Next", hour.GetNextHour() );
					periodInfo.AddSubitems( "Minutes", hour.GetMinutes() );
					SelectedPeriodInfo = periodInfo.ToString();
					break;
				case TimePeriodMode.Minute:
					Minute minute = new Minute( WorkingDateTime, timeCalendar );
					periodInfo.Clear();
					periodInfo.AddItem( "Minute", minute );
					periodInfo.AddItem( "Year", minute.Year );
					periodInfo.AddItem( "Month", minute.Month );
					periodInfo.AddItem( "Day", minute.Day );
					periodInfo.AddItem( "Hour", minute.Hour );
					periodInfo.AddItem( "MinuteValue", minute.MinuteValue );
					periodInfo.AddSection( "Previous/Next" );
					periodInfo.AddItem( "Previous", minute.GetPreviousMinute() );
					periodInfo.AddItem( "Next", minute.GetNextMinute() );
					SelectedPeriodInfo = periodInfo.ToString();
					break;
			}
		} // UpdateSelectedPeriodInfo

		// ----------------------------------------------------------------------
		private void UpdateWorkingDateTime()
		{
			DateTime workingDate = WorkingDate;
			DateTime workingTime = WorkingTime;
			WorkingDateTime =  new DateTime(
				workingDate.Year,
				workingDate.Month,
				workingDate.Day,
				workingTime.Hour,
				workingTime.Minute,
				workingTime.Second );
		} // UpdateWorkingDateTime

		// ----------------------------------------------------------------------
		private void NotifyPropertyChanged( string propertyName )
		{
			PropertyChangedEventHandler propertyChanged = PropertyChanged;
			if ( propertyChanged != null )
			{
				propertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
			}
		} // NotifyPropertyChanged

		// ----------------------------------------------------------------------
		private static void CalendarChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
		{
			CalendarDemoViewModel viewModel = d as CalendarDemoViewModel;
			if ( viewModel == null )
			{
				return;
			}

			viewModel.UpdateWorkingDateTime();
			viewModel.UpdateSelectedPeriodInfo();
		} // CalendarChanged

		// ----------------------------------------------------------------------
		// members
		private readonly TimePeriodInfo periodInfo = new TimePeriodInfo();
		private readonly EnumValueList<YearMonth> yearMonths = new EnumValueList<YearMonth>();
		private readonly EnumValueList<TimePeriodMode> periodModes = new EnumValueList<TimePeriodMode>();
		private DateTime workingDateTime;
		private string selectedPeriodInfo;

	} // class CalendarDemoViewModel

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
