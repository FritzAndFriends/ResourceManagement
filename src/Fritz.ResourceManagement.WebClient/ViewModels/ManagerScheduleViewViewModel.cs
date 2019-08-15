using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Fritz.ResourceManagement.Domain;
using Fritz.ResourceManagement.WebClient.Data;
using Microsoft.AspNetCore.Components;

namespace Fritz.ResourceManagement.WebClient.ViewModels
{
	public class ManagerScheduleViewViewModel
	{
		public IEnumerable<Schedule> Schedules { get; private set; }
		public DateTime SelectedDate
		{
			get { return this.MyScheduleState.SelectedDate; }
			set { 
				this.MyScheduleState.DisplayBeginDate = this.SelectedDate.Subtract(TimeSpan.FromDays((int)value.DayOfWeek));
				this.MyScheduleState.DisplayEndDate = this.MyScheduleState.DisplayBeginDate.AddDays(7);
				this.MyScheduleState.SelectDate(value);
			}
		}

		public ScheduleState MyScheduleState { get; private set; }

		private readonly HttpClient HttpClient;

		public ManagerScheduleViewViewModel(ScheduleState myScheduleState, HttpClient httpClient)
		{
			this.MyScheduleState = myScheduleState;
			this.HttpClient = httpClient;
		}

		public void OnChangeDate(int daysToChange)
		{
			this.SelectedDate = this.SelectedDate.AddDays(daysToChange);
		}

		public async Task OnParametersSetAsync()
		{

			this.MyScheduleState.DisplayBeginDate = this.SelectedDate.Subtract(TimeSpan.FromDays((int)this.SelectedDate.DayOfWeek));
			this.MyScheduleState.DisplayEndDate = this.MyScheduleState.DisplayBeginDate.AddDays(7);

			//this.Schedules = await HttpClient.GetJsonAsync<Schedule[]>($"api/schedule/forrange/{this.MyScheduleState.DisplayBeginDate.ToShortDateString()}/{this.MyScheduleState.DisplayEndDate.ToShortDateString()}");
		}
	}
}
