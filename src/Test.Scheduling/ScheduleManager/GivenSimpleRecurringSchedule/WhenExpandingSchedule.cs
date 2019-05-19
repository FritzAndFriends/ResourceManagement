using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fritz.ResourceManagement.Domain;
using Xunit;

namespace Test.Scheduling.ScheduleManager.GivenSimpleRecurringSchedule
{
	public class WhenExpandingSchedule
	{

		private Schedule _MySchedule = new Schedule {
			RecurringSchedules = new List<RecurringSchedule> {
				new RecurringSchedule {
					MinStartDateTime = new DateTime(2019, 5, 1),
					MaxEndDateTime = new DateTime(2019, 5, 31),
					CronPattern = "0 15 * * 1",
					Duration = TimeSpan.FromHours(1)
				}
			}
		};

		[Fact]
		public void ShouldExpandWithinDelimitedDatesOnly()
		{

			// arrange

			// act
			var sut = new Fritz.ResourceManagement.Scheduling.ScheduleManager();
			var results = sut.ExpandSchedule(_MySchedule, new DateTime(2019, 1, 1), new DateTime(2019, 12, 31));

			// assert
			Assert.Equal(4, results.Count());
			Assert.Equal(15, results.First().StartDateTime.Hour);

		}

		[Fact]
		public void ShouldExpandWithinRequestedDatesOnly()
		{

			// arrange

			// act
			var sut = new Fritz.ResourceManagement.Scheduling.ScheduleManager();
			var results = sut.ExpandSchedule(_MySchedule, new DateTime(2019, 5, 1), new DateTime(2019, 5, 15));

			// assert
			Assert.Equal(2, results.Count());
			Assert.Equal(15, results.First().StartDateTime.Hour);

		}

	}
}
