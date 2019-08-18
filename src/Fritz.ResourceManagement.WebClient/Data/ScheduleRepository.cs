using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Fritz.ResourceManagement.Domain;
using Microsoft.AspNetCore.Components;

namespace Fritz.ResourceManagement.WebClient.Data
{

	public interface IScheduleRepository {

		Task AddNewScheduleItem(Schedule schedule, ScheduleItem scheduleItem);

		Task AddNewRecurringSchedule(Schedule schedule, RecurringSchedule recurringSchedule);

		Task<TimeSlot[]> FetchTimeSlots(int scheduleId);

		Task<Schedule> GetAvailability(int scheduleId);

	}

	public class ScheduleRepository : IScheduleRepository
	{

		public ScheduleRepository(HttpClient httpClient, IUriHelper uriHelper)
		{
			this.Client = httpClient;
		}

		private HttpClient Client { get; }

		public async Task AddNewRecurringSchedule(Schedule schedule, RecurringSchedule recurringSchedule)
		{

			recurringSchedule.Status = ScheduleStatus.NotAvailable;
			recurringSchedule.ScheduleId = schedule.Id;
			schedule.RecurringSchedules.Add(recurringSchedule);

			await Client.PutJsonAsync($"/api/schedule/{schedule.Id}", schedule);

		}

		public async Task AddNewScheduleItem(Schedule schedule, ScheduleItem scheduleItem)
		{

			//scheduleItem.Status = ScheduleStatus.NotAvailable;
			scheduleItem.ScheduleId = schedule.Id;
			schedule.ScheduleItems.Add(scheduleItem);

			await this.Client.PutJsonAsync($"/api/schedule/{schedule.Id}", schedule);

		}

		public async Task<TimeSlot[]> FetchTimeSlots(int scheduleId)
		{
			return await Client.GetJsonAsync<TimeSlot[]>($"/api/timeslot/{scheduleId}/{DateTime.Today.AddMonths(-1).ToString("MM.dd.yyyy")}/{DateTime.Today.AddMonths(2).ToString("MM.dd.yyyy")}");
		}

		public async Task<Schedule> GetAvailability(int scheduleId)
		{
			return await Client.GetJsonAsync<Schedule>($"/api/schedule/{scheduleId}");
		}
	}
}
