using Fritz.ResourceManagement.Domain;
using Fritz.ResourceManagement.WebClient.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fritz.ResourceManagement.WebClient.ViewModels
{
	public class AvailabilityViewModel
	{

		// Cheer 342 cpayette 09/8/19 
		public enum Tabs {
			Single,
			Recurring
		}

		public Tabs SelectedTab { get; set; } = Tabs.Single;

		public object CurrentUser { get; set; }
		public ScheduleItem NewScheduleItem { get; set; } = new ScheduleItem() { };
		public RecurringSchedule NewRecurringSchedule { get; set; }
		public ScheduleState MyScheduleState { get; set; }

		public ScheduleItemViewModel ItemViewModel { get; set; }

		public DateTime DayViewStart => DateTime.Today.AddHours(8);
		public DateTime DayViewEnd => DateTime.Today.AddHours(20);

		private Schedule MySchedule { get; set; } = null;
		private DateTime SelectedDate { get; set; } = DateTime.Today;

		// TODO: Simon G - Do we still need this?
		private DateTime ThisMonth { get { return new DateTime(SelectedDate.Year, SelectedDate.Month, 1); } }
		
		private readonly HttpClient httpClient;
		public ClaimsPrincipal _User;

		public AvailabilityViewModel(
			//ClaimsPrincipal currentUser, 
			ScheduleState myScheduleState,
			HttpClient httpClient)
		{
			//this.CurrentUser = currentUser;

			this.MyScheduleState = myScheduleState;
			this.httpClient = httpClient;
		}

		public async Task OnInitAsync(ClaimsPrincipal user)
		{

			_User = user;
			this.MyScheduleState.ScheduleId = _User.GetClaimValueAsInt(UserInfo.Claims.SCHEDULEID).Value;

			this.ResetScheduleItem();
			this.MySchedule = await GetMyAvailability();

			this.MyScheduleState.SelectDate(SelectedDate);
			this.MyScheduleState.Schedule = MySchedule;
			await ExpandSchedule();
		}

		private async Task ExpandSchedule()
		{

			// Cheer 142 cpayette 01/08/19 
			// Cheer 5000 fixterjake 01/08/19 
			// Cheer 500 cpayette 08/08/19 

			var fetchedTimeslots = await httpClient.GetJsonAsync<TimeSlot[]>($"api/timeslot/{MyScheduleState.ScheduleId}/{DateTime.Today.AddMonths(-1).ToString("MM.dd.yyyy")}/{DateTime.Today.AddMonths(2).ToString("MM.dd.yyyy")}");
			Console.WriteLine($"Fetched {fetchedTimeslots.Length} timeslots");
			MyScheduleState.TimeSlots.AddRange(fetchedTimeslots);
			Console.WriteLine($"MyScheduleState: {MyScheduleState.GetHashCode()}");


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

		public void ClickTab(Tabs tab) {

			SelectedTab = tab;

		}

		public class ScheduleItemViewModel
		{

			// Cheer 142 cpayette 06/08/19 
			// Cheer 100 alternativecorn 06/08/19 

			[Required]
			public string Name { get; set; }

			[Required]
			public string StartDateTime { get; set; }

			[Required]
			public string EndDateTime { get; set; }

		}


	}
}
