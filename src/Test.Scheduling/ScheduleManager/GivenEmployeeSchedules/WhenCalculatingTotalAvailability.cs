using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fritz.ResourceManagement.Domain;
using Xunit;
using Xunit.Sdk;
using SCHEDULING = Fritz.ResourceManagement.Scheduling;

namespace Test.Scheduling.ScheduleManager.GivenEmployeeSchedules
{

	public class WhenCalculatingTotalAvailability
	{

		// TODO: Convert to a theory with various grains submitted
		[Theory()]
		[InlineData(AvailabilityTimeUnit.HalfHour)]
		[InlineData(AvailabilityTimeUnit.Hour)]
		[InlineData(AvailabilityTimeUnit.Day)]
		public void ThenShouldReportAtTheGrainRequested(AvailabilityTimeUnit timeUnit)
		{

			// arrange
			var mySchedules = new List<Schedule>();
			var startDate = new DateTime(2019, 7, 14);
			var endDate = new DateTime(2019, 7, 20);
			byte startHour = 8;
			byte endHour = 17;

			// act
			var results = new SCHEDULING.ScheduleManager().CalculateAvailability(mySchedules, startDate, endDate, startHour, endHour, timeUnit);

			// assert
			var expectedResults = new Dictionary<AvailabilityTimeUnit, int> {
				{ AvailabilityTimeUnit.HalfHour, 126 },
				{ AvailabilityTimeUnit.Hour, 63 },
				{ AvailabilityTimeUnit.Day, 7 }
			};

			Assert.Equal(expectedResults[timeUnit], results.Count());

		}

		[Fact]
		public void ThenShouldCalculateAvailabilityForEachTimeSlotReturned()
		{

			// arrange
			var startDate = new DateTime(2019, 7, 14);
			var endDate = new DateTime(2019, 7, 15);
			byte startHour = 8;
			byte endHour = 17;
			var mySchedules = new List<Schedule>
			{
				new Schedule
				{
					ScheduleItems = new ScheduleItem[]
					{
						new ScheduleItem
						{
							Name = "Available all day",
							StartDateTime = startDate.Date.AddHours(7),
							EndDateTime = startDate.AddHours(22),
							Status = ScheduleStatus.Available
						}
					}
				}
			};

			// act
			var results = new SCHEDULING.ScheduleManager().CalculateAvailability(mySchedules, startDate, endDate, startHour, endHour, AvailabilityTimeUnit.Hour);

			// assert
			Assert.Equal(1, results.First().Count);

		}

	}

}
