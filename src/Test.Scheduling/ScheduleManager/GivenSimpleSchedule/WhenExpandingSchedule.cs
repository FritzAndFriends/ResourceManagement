using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Fritz.ResourceManagement.Domain;
using Fritz.ResourceManagement.Scheduling;
using Xunit;

namespace Test.Scheduling.ScheduleManager.GivenSimpleSchedule
{


	public class WhenExpandingSchedule
	{

		private Schedule _MySimpleSchedule = new Schedule()
		{
			ScheduleItems = new List<ScheduleItem> {
							new ScheduleItem {
								StartDateTime=new DateTime(2019, 5, 17, 15, 0, 0),
								EndDateTime = new DateTime(2019, 5, 17, 16, 0, 0)
							}
						}
		};

		private Schedule _MyMultipleItemSchedule = new Schedule()
		{
			ScheduleItems = new List<ScheduleItem> {
							new ScheduleItem {
								StartDateTime=new DateTime(2019, 5, 17, 15, 0, 0),
								EndDateTime = new DateTime(2019, 5, 17, 16, 0, 0)
							},
							new ScheduleItem {
								StartDateTime=new DateTime(2019, 5, 18, 17, 0, 0),
								EndDateTime = new DateTime(2019, 5, 18, 18, 0, 0)
							}

						}
		};


		[Fact]
		public void ShouldCreateTheScheduleItemsSpecified()
		{

			// arrange

			// act
			var sut = new Fritz.ResourceManagement.Scheduling.ScheduleManager();
			var results = sut.ExpandSchedule(_MySimpleSchedule, new DateTime(2019, 1, 1), new DateTime(2019, 12, 31));

			// assert
			results.Should().NotBeEmpty();
			results.First().StartDateTime.Hour.Should().Be(15);
		}

		[Fact]
		public void ShouldCreateMultipleScheduleItemsSpecified()
		{

			// arrange

			// act
			var sut = new Fritz.ResourceManagement.Scheduling.ScheduleManager();
			var results = sut.ExpandSchedule(_MyMultipleItemSchedule, new DateTime(2019, 1, 1), new DateTime(2019, 12, 31));

			// assert
			results.Should().NotBeEmpty();
			results.Should().HaveCount(2);
			results.First().StartDateTime.Hour.Should().Be(15);
			results.Skip(1).First().StartDateTime.Hour.Should().Be(17);

		}

		[Fact]
		public void ShouldExpandWithinDatesSpecified()
		{

			// arrange

			// act
			var sut = new Fritz.ResourceManagement.Scheduling.ScheduleManager();
			var results = sut.ExpandSchedule(_MyMultipleItemSchedule, new DateTime(2019, 1, 1), new DateTime(2019, 5, 18));

			// assert
			results.Should().ContainSingle();

		}

	}

}
