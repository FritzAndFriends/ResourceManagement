using Fritz.ResourceManagement.Domain;
using Fritz.ResourceManagement.WebClient.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fritz.ResourceManagement.WebClient.ViewModels
{
	public class AvailabilityViewModel
	{
		public object CurrentUser { get; set; }
		public ScheduleItem NewScheduleItem { get; set; } = new ScheduleItem() { };
		public RecurringSchedule NewRecurringSchedule { get; set; }
		public ScheduleState MyScheduleState { get; set; }

		public DateTime DayViewStart => DateTime.Today.AddHours(8);
		public DateTime DayViewEnd => DateTime.Today.AddHours(20);

		private Schedule MySchedule { get; set; } = null;
		private DateTime SelectedDate { get; set; } = DateTime.Today;

		// TODO: Simon G - Do we still need this?
		private DateTime ThisMonth { get { return new DateTime(SelectedDate.Year, SelectedDate.Month, 1); } }
		
		private readonly HttpClient httpClient;
		private ClaimsPrincipal _User;

		public AvailabilityViewModel(
			//ClaimsPrincipal currentUser, 
			ScheduleState myScheduleState,
			HttpClient httpClient)
		{
			//this.CurrentUser = currentUser;

			// TODO: Remove mocked schedulestate
			//this.MyScheduleState = myScheduleState;
			this.httpClient = httpClient;
		}

		public async Task OnInitAsync(ClaimsPrincipal user)
		{

			_User = user;
			this.MyScheduleState = new ScheduleState { ScheduleId = _User.GetClaimValueAsInt(UserInfo.Claims.SCHEDULEID).Value };

			this.ResetScheduleItem();
			this.MySchedule = await GetMyAvailability();

			this.MyScheduleState.SelectDate(SelectedDate);
			this.MyScheduleState.Schedule = MySchedule;
			this.MyScheduleState.ExpandSchedule();
		}

		public async Task AddNewRecurringSchedule()
		{
			// Cheer 2000 organicIT 23/06/19

			this.NewRecurringSchedule.Status = ScheduleStatus.NotAvailable;
			this.NewRecurringSchedule.ScheduleId = MySchedule.Id;
			this.MySchedule.RecurringSchedules.Add(NewRecurringSchedule);

			await this.httpClient.PutJsonAsync($"api/schedule/{MySchedule.Id}", MySchedule);

			this.MyScheduleState.ScheduleUpdated();

			this.ResetScheduleItem();
		}

		private async Task<Schedule> GetMyAvailability()
		{
			return await this.httpClient.GetJsonAsync<Schedule>($"api/schedule/{MyScheduleState.ScheduleId}");
		}

		public async Task AddNewScheduleItem()
		{
			// Cheer 200 ultramark 07/06/19
			// Cheer 100 TheMichaelJolley 07/06/19

			this.NewScheduleItem.Status = ScheduleStatus.NotAvailable;
			this.NewScheduleItem.ScheduleId = MySchedule.Id;
			this.MySchedule.ScheduleItems.Add(NewScheduleItem);

			await this.httpClient.PutJsonAsync($"api/schedule/{MySchedule.Id}", MySchedule);

			this.MyScheduleState.ScheduleUpdated();

			this.ResetScheduleItem();
		}

		private void ResetScheduleItem()
		{
			this.NewScheduleItem = new ScheduleItem()
			{
			  StartDateTime = DateTime.Today,
				EndDateTime = DateTime.Today.AddDays(1)
			};
			this.NewRecurringSchedule = new RecurringSchedule()
			{
			  MinStartDateTime = DateTime.Today,
			  MaxEndDateTime = DateTime.Today.AddDays(7),
			};
		}
	}
}
