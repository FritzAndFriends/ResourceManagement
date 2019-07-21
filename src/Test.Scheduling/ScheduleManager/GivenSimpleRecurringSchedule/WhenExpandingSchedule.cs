using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Fritz.ResourceManagement.Domain;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Test.Scheduling.ScheduleManager.GivenSimpleRecurringSchedule
{
	public class WhenExpandingSchedule
	{
		private readonly ILoggerFactory _LoggerFactory;
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
		private Schedule _MyMultipleRecurringSchedule = new Schedule
		{
		RecurringSchedules = new List<RecurringSchedule>
					{
							new RecurringSchedule
							{
								MinStartDateTime = new DateTime(2018, 6, 1),
								MaxEndDateTime = new DateTime(2018, 6, 30),
								CronPattern = "0 15 * * 1",
								Duration = TimeSpan.FromHours(1)
							},
							new RecurringSchedule
							{
								MinStartDateTime = new DateTime(2020, 5, 1),
								MaxEndDateTime = new DateTime(2020, 5, 31),
								CronPattern = "0 15 * * 1",
								Duration = TimeSpan.FromHours(1)
							}
					}
		};

		public WhenExpandingSchedule()
		{
				
			_LoggerFactory = LoggerFactory.Create(config => {
				config.SetMinimumLevel(LogLevel.Trace)
					.AddConsole();
			});

		}

		[Fact]
		public void ShouldExpandWithinDelimitedDatesOnly()
		{

			// arrange

			// act
			var sut = new Fritz.ResourceManagement.Scheduling.ScheduleManager();
			var results = sut.ExpandSchedule(_MySchedule, new DateTime(2019, 1, 1), new DateTime(2019, 12, 31));

			// assert
			results.Count().Should().Be(4);
			results.First().StartDateTime.Hour.Should().Be(15);

		}

		[Fact]
		public void ShouldExpandWithinRequestedDatesOnly()
		{

			// arrange

			// act
			var sut = new Fritz.ResourceManagement.Scheduling.ScheduleManager();
			var results = sut.ExpandSchedule(_MySchedule, new DateTime(2019, 5, 1), new DateTime(2019, 5, 15), _LoggerFactory.CreateLogger("test"));

			// assert
			results.Should().HaveCount(2);
			results.First().StartDateTime.Hour.Should().Be(15);

		}

		[Fact]
		public void MultipleRecurringSchedules_ShouldExpandWithinRequestedDatesOnly_NoIntersection()
		{
			// arrange

			// act
			var sut = new Fritz.ResourceManagement.Scheduling.ScheduleManager();
			var results = sut.ExpandSchedule(_MyMultipleRecurringSchedule, new DateTime(2019, 5, 1), new DateTime(2019, 5, 15), _LoggerFactory.CreateLogger("test"));

			// assert
			results.Should().BeEmpty();
		}
		[Fact]
		public void ShouldExpandWithinRequestedDatesOnly_Scenario4()
		{
			// arrange

			// act
			var sut = new Fritz.ResourceManagement.Scheduling.ScheduleManager();
			var results = sut.ExpandSchedule(_MySchedule, new DateTime(2019, 4, 1), new DateTime(2019, 6, 1), _LoggerFactory.CreateLogger("test"));

			// assert
			results.Should().HaveCount(4);
			results.First().StartDateTime.Hour.Should().Be(15);

		}
		[Fact]
		public void ShouldExpandWithinRequestedDatesOnly_Scenario5()
		{
			// arrange
			//act
			var sut = new Fritz.ResourceManagement.Scheduling.ScheduleManager();
			var results = sut.ExpandSchedule(_MySchedule, new DateTime(2019, 5, 27), new DateTime(2019, 6, 30), _LoggerFactory.CreateLogger("test"));
			//assert
			results.Should().ContainSingle();
			results.First().StartDateTime.Hour.Should().Be(15);

			results = sut.ExpandSchedule(_MySchedule, new DateTime(2019, 5, 1), new DateTime(2019, 6, 30), _LoggerFactory.CreateLogger("test"));
			results.Should().HaveCount(4);
			results.First().StartDateTime.Hour.Should().Be(15);

		}

		[Fact]
		public void ShouldExpandWithinRequestedDatesOnly_Scenario6()
		{
			// arrange
			//act
			var sut = new Fritz.ResourceManagement.Scheduling.ScheduleManager();
			var results = sut.ExpandSchedule(_MySchedule, new DateTime(2019, 4, 27), new DateTime(2019, 5, 24), _LoggerFactory.CreateLogger("test"));
			//assert
			results.Should().HaveCount(3);
			results.First().StartDateTime.Hour.Should().Be(15);

			results = sut.ExpandSchedule(_MySchedule, new DateTime(2019, 4, 27), new DateTime(2019, 5, 31), _LoggerFactory.CreateLogger("test"));
			results.Should().HaveCount(4);
			results.First().StartDateTime.Hour.Should().Be(15);

		}

	}

}
