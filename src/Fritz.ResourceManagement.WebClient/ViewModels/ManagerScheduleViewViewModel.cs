using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Fritz.ResourceManagement.Domain;
using Fritz.ResourceManagement.WebClient.Data;
using Microsoft.AspNetCore.Components;

namespace Fritz.ResourceManagement.WebClient.ViewModels
{
	public class ManagerScheduleViewViewModel : ComponentBase
	{
		public IEnumerable<Schedule> Schedules { get; private set; }
		public DateTime SelectedDate
		{
			get { return this.MyScheduleState.SelectedDate; }
			set 
			{ 
				this.MyScheduleState.DisplayBeginDate = this.SelectedDate.Subtract(TimeSpan.FromDays((int)value.DayOfWeek));
				this.MyScheduleState.DisplayEndDate = this.MyScheduleState.DisplayBeginDate.AddDays(7);
				this.MyScheduleState.SelectDate(value);
				this.StateHasChanged();
			}
		}

		// Cheer 642 cpayette 16/8/19
		[Parameter]
		public string RequestedDate
		{
			get { return this.SelectedDate.ToString("yyyyMMdd", CultureInfo.InvariantCulture); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.SelectedDate = DateTime.Today;
					return;
				}
				this.SelectedDate = DateTime.ParseExact(value, "yyyyMMdd", CultureInfo.InvariantCulture);
			}
		}
		
		[Inject]
		public ScheduleState MyScheduleState { get; private set; }

		public void OnChangeDate(int daysToChange)
		{
			this.SelectedDate = this.SelectedDate.AddDays(daysToChange);
		}

		protected override async Task OnParametersSetAsync()
		{

			this.MyScheduleState.DisplayBeginDate = this.SelectedDate.Subtract(TimeSpan.FromDays((int)this.SelectedDate.DayOfWeek));
			this.MyScheduleState.DisplayEndDate = this.MyScheduleState.DisplayBeginDate.AddDays(7);

			//this.Schedules = await HttpClient.GetJsonAsync<Schedule[]>($"api/schedule/forrange/{this.MyScheduleState.DisplayBeginDate.ToShortDateString()}/{this.MyScheduleState.DisplayEndDate.ToShortDateString()}");
		}
	}
}
