using System;
using Fritz.ResourceManagement.Domain;

namespace Fritz.ResourceManagement.WebClient.ViewModels
{
	public class DayViewViewModel
	{

		public DayViewViewModel(Data.ScheduleState scheduleState)
		{
			this.MyScheduleState = scheduleState;
		}

		public int HoursPerDay
		{
			get { return this.DayViewEnd.Subtract(this.DayViewStart).Hours; }
		}

		public DateTime SelectedDate
		{
			get { return this.MyScheduleState?.SelectedDate ?? DateTime.Today; }
		}

		public Schedule MySchedule
		{
			get { return this.MyScheduleState.Schedule; }
		}

		public Data.ScheduleState MyScheduleState { get; set; }

		public DateTime DayViewStart { get; set; } = DateTime.Today.AddHours(8);

		public DateTime DayViewEnd { get; set; } = DateTime.Today.AddHours(20);

		public int DayCount { get; set; } = 1;

		/// <summary>
		/// Should we display the dates above the grid
		/// </summary>
		public bool DayDisplay { get; set; } = false;

		public bool DisplayItem(DateTime start, DateTime end)
		{
			var startDayView = this.SelectedDate.Date.Add(this.DayViewStart.TimeOfDay);
			var endDayView = this.SelectedDate.Date.Add(this.DayViewEnd.TimeOfDay);

			if (start >= endDayView || end <= startDayView) { return false; }
			return true;
		}

		public string ItemBorderStyle(DateTime start, DateTime end)
		{
			string top = "starts-before";
			string bottom = "ends-after";

			var startDayView = this.SelectedDate.Date.Add(this.DayViewStart.TimeOfDay);
			var endDayView = this.SelectedDate.Date.Add(this.DayViewEnd.TimeOfDay);

			return String.Format("{0} {1}",
				(start < startDayView) ? top : "",
				(end > endDayView) ? bottom : "");
		}

		public int ItemStartRow(DateTime start)
		{
			if (start.Hour <= this.DayViewStart.Hour)
			{
				return 1;
			}

			return (start.Hour - this.DayViewStart.Hour) + 1;
		}

		public string ItemTopPosition(DateTime start)
		{
			double top = 0D;
			var startDayView = this.SelectedDate.Date.Add(this.DayViewStart.TimeOfDay);

			if (start < startDayView)
			{
				return "-0.050em";
			}

			if (start > startDayView && start.Minute > 0)
			{
				top = 0.025 * start.Minute;
			}
			return (top > 0) ? $"{top}em" : "0";
		}

		public string ItemRowHeight(DateTime start, DateTime end)
		{
			double height = 0D;
			double totalMinutes;

			TimeSpan startTime;
			TimeSpan endTime;
			if (start.TimeOfDay < this.DayViewStart.TimeOfDay)
			{
				startTime = this.DayViewStart.TimeOfDay;
			}
			else if (start.Date < this.SelectedDate)
			{
				startTime = this.DayViewStart.TimeOfDay;
			}
			else
			{
				startTime = start.TimeOfDay;
			}

			if (end.TimeOfDay >= this.DayViewEnd.TimeOfDay)
			{
				endTime = this.DayViewEnd.TimeOfDay;
			}
			else if (end.Date > this.SelectedDate)
			{
				endTime = this.DayViewEnd.TimeOfDay;
			}
			else
			{
				endTime = end.TimeOfDay;
			}

			if (start.TimeOfDay < this.DayViewStart.TimeOfDay)
			{
				height += 0.050; // Add 2 extra minutes for negative top position from ItemTopPosition
			}

			totalMinutes = endTime.Subtract(startTime).TotalMinutes;
			height += 0.025 * totalMinutes; // 1 minute = 1.25em / 60 = 0.025em

			/* Adjust for row gaps, 1px = 0.063em at base 16px size
			 * according to http://pxtoem.com/ */
			height += 0.063 * (endTime.Subtract(startTime).TotalHours);

			return (height > 0) ? $"{height}em" : "0";
		}
	}
}
