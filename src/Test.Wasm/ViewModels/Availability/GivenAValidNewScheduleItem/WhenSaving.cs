using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Fritz.ResourceManagement.Domain;
using Fritz.ResourceManagement.WebClient.Data;
using Fritz.ResourceManagement.WebClient.ViewModels;
using Moq;
using Xunit;

namespace Test.Wasm.ViewModels.Availability.GivenAValidNewScheduleItem
{

	public class WhenSaving
	{

		private readonly Mock<IScheduleRepository> _ScheduleRepository;

		public WhenSaving()
		{
			_ScheduleRepository = new Mock<IScheduleRepository>();
		}

		[Fact]
		public async Task ShouldPassDatesProperly() {

			// arrange
			var state = new ScheduleState();
			var StartDate = new DateTime(2019, 8, 25, 14, 0, 0);
			var EndDate = new DateTime(2019, 8, 25, 16, 0, 0);

			// Act
			var sut = new AvailabilityViewModel(state, _ScheduleRepository.Object);
			sut.MySchedule = new Schedule { Id = 1 };
			sut.NewScheduleItem.StartDateTime = StartDate;
			sut.NewScheduleItem.EndDateTime = EndDate;
			sut.NewScheduleItem.Name = "Interview with Emma";
			var results = await sut.AddNewScheduleItem();

			// Assert
			Assert.Empty(results);
			_ScheduleRepository.Verify(r => r.AddNewScheduleItem(It.IsAny<Schedule>(), 
				It.Is<ScheduleItem>(i => i.StartDateTime == StartDate && i.EndDateTime == EndDate)),
				Times.Once, "Did not load the schedule item properly");


		}

	}

}
