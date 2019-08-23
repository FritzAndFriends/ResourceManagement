using System;
using System.Collections.Generic;
using System.Linq;
using Fritz.ResourceManagement.Domain;
using Fritz.ResourceManagement.WebClient.Data;
using Microsoft.AspNetCore.Components;

namespace Fritz.ResourceManagement.WebClient.ViewModels
{
	public class DayPickerViewModel : ComponentBase
	{

		protected override void OnInitialized()
		{
			Console.WriteLine($"MyScheduleState: {MyScheduleState.GetHashCode()}");
			base.OnInitialized();
		}

		public DateTime SelectedDate
		{
			get { return this.MyScheduleState.SelectedDate; }
			set { this.MyScheduleState.SelectDate(value); }
		}
		public int FirstDayOfMonthDoW
		{
			get
			{
				var first = new DateTime(this.SelectedDate.Year, this.SelectedDate.Month, 1);
				return (int)first.DayOfWeek;
			}
		}
		
		public int LastDayOfMonth
		{
			get
			{
				return new DateTime(this.SelectedDate.Year, this.SelectedDate.Month, 1).AddMonths(1).AddDays(-1).Day;
			}
		}
		
		public DayOfMonthDisplayInfo DisplayDayOfMonth(int dayOfMonth)
		{
			var thisDay = new DateTime(this.SelectedDate.Year, this.SelectedDate.Month, dayOfMonth);
			var today = (thisDay.Date == DateTime.Today.Date) ? "today" : null;

			var hasAppt = MyScheduleState.TimeSlots.Any(x => x.StartDateTime.Date == thisDay.Date) ? "appt" : null;

			return new DayOfMonthDisplayInfo()
			{
				ThisDay = thisDay,
				Today = today,
				HasAppointment = hasAppt,
				IsSelected = (thisDay == this.SelectedDate) ? "active" : null,
				Title = (string.IsNullOrEmpty(today)) ? null : "Today!"
			};
		}
		
		[Inject]
		public ScheduleState MyScheduleState { get; private set; }

		public void GotoToday()
		{
			this.SelectedDate = DateTime.Today;
		}

		public void PrevMonth()
		{
			this.SelectedDate = this.SelectedDate.AddMonths(-1);
			this.SelectedDate = new DateTime(this.SelectedDate.Year, this.SelectedDate.Month, 1);
		}
		public void NextMonth()
		{
			this.SelectedDate = this.SelectedDate.AddMonths(1);
			this.SelectedDate = new DateTime(this.SelectedDate.Year, this.SelectedDate.Month, 1);
		}

		public struct DayOfMonthDisplayInfo
		{
			public DateTime ThisDay;
			public string Today;
			public string HasAppointment;
			public string IsSelected;
			public string Title;
		}
	}
}
