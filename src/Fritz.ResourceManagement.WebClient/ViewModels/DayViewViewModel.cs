using System;
using Fritz.ResourceManagement.Domain;
using Microsoft.AspNetCore.Components;

namespace Fritz.ResourceManagement.WebClient.ViewModels
{
	public class DayViewViewModel : ComponentBase
	{
		private const double MinuteInEms = 0.025; // 1 minute = 1.25em / 60 = 0.025em
		private const double RowGaps = 0.063;

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

		[Inject]
		[Parameter]
		public Data.ScheduleState MyScheduleState { get; set; }

		// Cheer 701 themichaeljolley 09/07/19
		// Cheer 600 cpayette 09/07/19
		// Cheer 1500 clintonrocksmith 09/07/19
		[Parameter]
		public DateTime DayViewStart { get; set; } = DateTime.Today.AddHours(8);

		[Parameter]
		public DateTime DayViewEnd { get; set; } = DateTime.Today.AddHours(20);

		// Cheer 110 copperbeardy 14/07/19
		[Parameter]
		public int DayCount { get; set; } = 1;

		/// <summary>
		/// Should we display the dates above the grid
		/// </summary>
		[Parameter]
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
			var startDayView = this.SelectedDate.Date.Add(this.DayViewStart.TimeOfDay);

			if (start <= startDayView)
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
				return $"{RowGaps * -1}em";
			}

			if (start > startDayView && start.Minute > 0)
			{
				top = MinuteInEms * start.Minute;
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
				height += RowGaps; // Add extra length for negative top position from ItemTopPosition
			}

			totalMinutes = endTime.Subtract(startTime).TotalMinutes;
			height += MinuteInEms * totalMinutes;

			/* Adjust for row gaps, 1px = 0.063em at base 16px size
			 * according to http://pxtoem.com/ */
			height += RowGaps * (endTime.Subtract(startTime).TotalHours);

			return (height > 0) ? $"{height}em" : "0";
		}

		// Cheer 142 cpayette 15/8/19 
		protected override void OnParametersSet()
		{
			MyScheduleState.OnSelectedDateChanged += (obj, args) =>
			{
				this.StateHasChanged();
			};

			this.StateHasChanged();
			base.OnParametersSet();
		}
	}
}
