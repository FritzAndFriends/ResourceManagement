using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fritz.ResourceManagement.Domain;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Test.Scheduling.ScheduleManager.GivenScheduleException
{
	public class WhenExpandingSchedule : SimpleScheduleException
	{

		private readonly ILoggerFactory _LoggerFactory;
		private Schedule _MySchedule = new Schedule
		{
			RecurringSchedules = new List<RecurringSchedule> {
				new RecurringSchedule {
					Name = "Available from Noon to 5pm in July 2019",
					MinStartDateTime = new DateTime(2019, 7, 1),
					MaxEndDateTime = new DateTime(2019, 7, 31, 23, 59, 59),
					CronPattern = "0 12 * * *",  // At Noon every day
					Duration = TimeSpan.FromHours(5),	// From Noon to 5pm we're available
					Status=ScheduleStatus.Available
				}
			}
		};

		[Fact]
		public void ShouldRemoveTimeslotsOverTheWeekend()
		{

			// Cheer 200 faniereynders 17/07/19 
			// Cheer 100 devlead 17/07/19 

			// arrange
			_MySchedule.ScheduleExceptions.Add(TheWeekend);

			// act
			var sut = new Fritz.ResourceManagement.Scheduling.ScheduleManager();
			var results = sut.ExpandSchedule(_MySchedule, new DateTime(2019, 1, 1), new DateTime(2019, 12, 31));

			// assert
			Assert.Equal(28, results.Count());
			Assert.Equal(12, results.First().StartDateTime.Hour);


		}


		[Fact]
		public void ShouldHandleAllDayEvents()
		{

			// Cheer 100 artfortheapocalypse 17/07/19 

			// arrange
			Schedule theSchedule = new Schedule
			{
				ScheduleItems = new List<ScheduleItem>
				{
					new ScheduleItem
					{
						Name = "Dance Marathon",
						StartDateTime = new DateTime(2019, 7, 20),
						EndDateTime = new DateTime(2019, 7, 21),
						Status = ScheduleStatus.NotAvailable
					}
				},
				ScheduleExceptions = new List<ScheduleException> { TheWeekend }
			};

			// act
			var sut = new Fritz.ResourceManagement.Scheduling.ScheduleManager();
			var results = sut.ExpandSchedule(theSchedule, new DateTime(2019, 1, 1), new DateTime(2019, 12, 31));

			// assert
			Assert.Empty(results);



		}

	}

}
