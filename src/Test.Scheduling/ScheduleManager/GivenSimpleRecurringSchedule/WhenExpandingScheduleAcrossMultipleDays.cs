using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using Fritz.ResourceManagement.Domain;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace Test.Scheduling.ScheduleManager.GivenSimpleRecurringSchedule
{
	public class WhenExpandingScheduleAcrossMultipleDays
	{

		private readonly ILoggerFactory _LoggerFactory;
		private readonly ITestOutputHelper testOutputHelper;
		private Schedule _MySchedule = new Schedule
		{
			RecurringSchedules = new List<RecurringSchedule> {
				new RecurringSchedule {
					MinStartDateTime = new DateTime(2019, 6, 23),
					MaxEndDateTime = new DateTime(2019, 6, 30),
					CronPattern = "0 0 * * *",
					Duration = TimeSpan.Parse("1:00:00:00")
				}
			}
		};

		public WhenExpandingScheduleAcrossMultipleDays(ITestOutputHelper testOutputHelper)
		{
			this.testOutputHelper = testOutputHelper;
		}


		[Fact]
		public void ShouldExpandWithinDelimitedDatesOnly()
		{

			// arrange
			var testLogger = new XunitLogger(testOutputHelper);

			// act
			var sut = new Fritz.ResourceManagement.Scheduling.ScheduleManager();
			testOutputHelper.WriteLine(_MySchedule.RecurringSchedules[0].Duration.TotalHours.ToString());
			var results = sut.ExpandSchedule(_MySchedule, new DateTime(2019, 6, 24), new DateTime(2019, 6, 26), testLogger);

			// assert
			results.Should().HaveCount(2);
			results.First().StartDateTime.Hour.Should().Be(0);
			results.First().EndDateTime.Hour.Should().Be(0);

		}

		public class XunitLogger : ILogger
		{
			private readonly ITestOutputHelper testOutputHelper;

			public XunitLogger(ITestOutputHelper testOutputHelper)
			{
				this.testOutputHelper = testOutputHelper;
			}

			public IDisposable BeginScope<TState>(TState state)
			{
				throw new NotImplementedException();
			}

			public bool IsEnabled(LogLevel logLevel)
			{
				throw new NotImplementedException();
			}

			public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
			{
				testOutputHelper.WriteLine(formatter.Invoke(state, exception));
			}
		}

	}

}
