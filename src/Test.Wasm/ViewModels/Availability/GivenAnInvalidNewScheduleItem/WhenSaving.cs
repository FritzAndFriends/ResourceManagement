using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fritz.ResourceManagement.Domain;
using Fritz.ResourceManagement.WebClient.Data;
using Fritz.ResourceManagement.WebClient.ViewModels;
using Moq;
using Xunit;

namespace Test.Wasm.ViewModels.Availability.GivenAnInvalidNewScheduleItem
{

	public class WhenSaving
	{

		private readonly Mock<IScheduleRepository> _ScheduleRepository;

		public WhenSaving()
		{
			_ScheduleRepository = new Mock<IScheduleRepository>();
		}

		[Fact]
		public async Task ShouldReturnAppropriateValidationMessages() {

			// arrange
			var state = new ScheduleState();

			// Act
			var sut = new AvailabilityViewModel(state, _ScheduleRepository.Object);
			await sut.AddNewScheduleItem();

			// Assert
			_ScheduleRepository.Verify(r => r.AddNewScheduleItem(It.IsAny<Schedule>(), It.IsAny<ScheduleItem>()),
				Times.Never, "Should not have attempted to save the schedule");

		}


	}

}
