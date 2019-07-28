using System;
using Fritz.ResourceManagement.Domain;
using Fritz.ResourceManagement.WebClient.Data;

namespace Fritz.ResourceManagement.WebClient.ViewModels
{
	public class DayPickerViewModel
	{
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
		
		public ScheduleState MyScheduleState { get; private set; }

		// TODO: Simon G - I think this was dead code from the razor code block and can probably be removed.
		public Schedule MySchedule
		{
			get { return this.MyScheduleState.Schedule; }
		}

		public DayPickerViewModel(ScheduleState myScheduleState)
		{
			this.MyScheduleState = myScheduleState;
		}

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
	}
}
