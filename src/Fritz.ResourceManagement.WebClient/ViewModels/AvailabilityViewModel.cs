using Fritz.ResourceManagement.Domain;
using Fritz.ResourceManagement.WebClient.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
		public IScheduleRepository ScheduleRepository { get; }
		public ScheduleItemViewModel ItemViewModel { get; set; }

		public DateTime DayViewStart => DateTime.Today.AddHours(8);
		public DateTime DayViewEnd => DateTime.Today.AddHours(20);

		public Schedule MySchedule { get; set; } = null;
		private DateTime SelectedDate { get; set; } = DateTime.Today;

		// TODO: Simon G - Do we still need this?
		private DateTime ThisMonth { get { return new DateTime(SelectedDate.Year, SelectedDate.Month, 1); } }
		
		public ClaimsPrincipal _User;

		public AvailabilityViewModel(
			ScheduleState myScheduleState,
			IScheduleRepository scheduleRepository)
		{
			//this.CurrentUser = currentUser;

			this.MyScheduleState = myScheduleState;
			ScheduleRepository = scheduleRepository;
		}

		public async Task OnInitAsync(ClaimsPrincipal user)
		{

			_User = user;
			this.MyScheduleState.ScheduleId = _User.GetClaimValueAsInt(UserInfo.Claims.SCHEDULEID).Value;

			this.ResetScheduleItem();
			this.MySchedule = await ScheduleRepository.GetAvailability(MyScheduleState.ScheduleId);

			this.MyScheduleState.SelectDate(SelectedDate);
			this.MyScheduleState.Schedule = MySchedule;
			await ExpandSchedule();
		}

		private async Task ExpandSchedule()
		{

			// Cheer 142 cpayette 01/08/19 
			// Cheer 5000 fixterjake 01/08/19 
			// Cheer 500 cpayette 08/08/19 

			var fetchedTimeslots = await ScheduleRepository.FetchTimeSlots(MySchedule.Id);
			Console.WriteLine($"Fetched {fetchedTimeslots.Length} timeslots");
			MyScheduleState.TimeSlots.AddRange(fetchedTimeslots);
			Console.WriteLine($"MyScheduleState: {MyScheduleState.GetHashCode()}");


		}

		public async Task<IEnumerable<ValidationResult>> AddNewRecurringSchedule()
		{
			// Cheer 2000 organicIT 23/06/19

			var results = NewRecurringSchedule.Validate(null);
			if (results.Any()) return results;
			await ScheduleRepository.AddNewRecurringSchedule(this.MySchedule, NewRecurringSchedule);

			this.MyScheduleState.ScheduleUpdated();

			this.ResetScheduleItem();
			return new ValidationResult[] { };
		}

		public async Task<IEnumerable<ValidationResult>> AddNewScheduleItem()
		{
			// Cheer 200 ultramark 07/06/19
			// Cheer 100 TheMichaelJolley 07/06/19
			// Cheer 142 cpayette 18/8/19 

			NewScheduleItem.Status = ScheduleStatus.NotAvailable;

			var results = this.NewScheduleItem.Validate(null); // <<-- That's not right...
			if (results.Any()) return results;

			await ScheduleRepository.AddNewScheduleItem(MySchedule, NewScheduleItem);

			this.MyScheduleState.ScheduleUpdated();

			this.ResetScheduleItem();
			return new ValidationResult[] { };
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
