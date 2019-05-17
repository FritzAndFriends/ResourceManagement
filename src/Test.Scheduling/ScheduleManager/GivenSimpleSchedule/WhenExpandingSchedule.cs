using System;
using System.Collections.Generic;
using System.Linq;
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
			Assert.NotEmpty(results);
			Assert.Equal(15, results.First().StartDateTime.Hour);

		}

		[Fact]
		public void ShouldCreateMultipleScheduleItemsSpecified()
		{

			// arrange

			// act
			var sut = new Fritz.ResourceManagement.Scheduling.ScheduleManager();
			var results = sut.ExpandSchedule(_MyMultipleItemSchedule, new DateTime(2019, 1, 1), new DateTime(2019, 12, 31));

			// assert
			Assert.NotEmpty(results);
			Assert.Equal(2, results.Count());
			Assert.Equal(15, results.First().StartDateTime.Hour);
			Assert.Equal(17, results.Skip(1).First().StartDateTime.Hour);

		}

		[Fact]
		public void ShouldExpandWithinDatesSpecified()
		{

			// arrange

			// act
			var sut = new Fritz.ResourceManagement.Scheduling.ScheduleManager();
			var results = sut.ExpandSchedule(_MyMultipleItemSchedule, new DateTime(2019, 1, 1), new DateTime(2019, 5, 18));

			// assert
			Assert.Single(results);

		}

	}

}
