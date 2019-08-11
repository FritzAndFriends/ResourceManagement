using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using TEST = Fritz.ResourceManagement.WebClient.Data;

namespace Test.Scheduling.ScheduleState
{

	public class ExpandSchedule
	{

		[Fact]	
		public void ShouldExpandTheSchedule() {

			// arrange
			var s = new TEST.ScheduleState();

			// act
			s.ExpandSchedule(DateTime.Today, DateTime.Today.AddDays(2));

			// assert

		}	
	
	}

}
