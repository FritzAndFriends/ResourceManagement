using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fritz.ResourceManagement.Domain;
using Xunit;
using SCHEDULING = Fritz.ResourceManagement.Scheduling;

namespace Test.Scheduling.ScheduleManager.GivenEmployeeSchedules
{

	public class WhenCalculatingTotalAvailability
	{

		[Fact]
		public void ThenShouldReportAtTheGrainRequested()
		{

			// arrange
			var mySchedules = new List<Schedule>();
			var startDate = new DateTime(2019, 7, 14);
			var endDate = new DateTime(2019, 7, 21);
			var startHour = 8;
			var endHour = 17;

			// assert
			var results = new SCHEDULING.ScheduleManager().CalculateAvailability(mySchedules, startDate, endDate, startHour, endHour, TimeUnit.Hour);

			// act
			Assert.Equal(56, results.Count());


		}

	}

}
