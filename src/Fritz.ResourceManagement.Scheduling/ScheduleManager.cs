﻿using System;
using System.Collections.Generic;
using System.Linq;
using Fritz.ResourceManagement.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NCrontab;

namespace Fritz.ResourceManagement.Scheduling
{
    public class ScheduleManager
    {

        public IEnumerable<TimeSlot> ExpandSchedule(Schedule schedule, DateTime startTime, DateTime endTime, ILogger logger = null) {

            var outSchedule = new List<TimeSlot>();
						logger = logger ?? NullLogger.Instance;

						if (schedule.ScheduleItems != null)
            outSchedule.AddRange(ExpandScheduleItems(schedule.ScheduleItems, startTime, endTime));

						if (schedule.RecurringSchedules != null) 
						outSchedule.AddRange(ExpandRecurringSchedules(schedule.RecurringSchedules, startTime, endTime, logger));

            return outSchedule;

        }

		/**
		
			Scenarios for schedule overlap
			My Schedule                       /----/
			Scenario 1:                /----/
			Scenario 2:                              /----/
			Scenario 3:                   /-----------/
			Scenario 4:                        /-/
			Scenario 5:                   /------/
			Scenario 6:                        /----------/
		
		 */

		private static (DateTime begin, DateTime end)? CalculateIntersectionOfTimeslots(
			(DateTime StartDateTime, DateTime EndDateTime) ts1, 
			(DateTime StartDateTime, DateTime EndDateTime) ts2,
			ILogger logger) {

			logger.LogTrace($"Checking Time 1 {ts1} against Time 2 {ts2}");

			// Eliminate scenario 1 and 2
			if (!(ts1.StartDateTime <= ts2.EndDateTime && ts2.StartDateTime <= ts1.EndDateTime)) {
				logger.LogTrace("Did not find an intersection");
				return null;
			}

			// Scenario 3
			if (ts1.StartDateTime >= ts2.StartDateTime && ts1.EndDateTime <= ts2.EndDateTime) {
				logger.LogTrace("Time 2 is contained entirely within Time 1");
				return (ts1.StartDateTime, ts1.EndDateTime);
			}

			// Scenario 4
			if (ts1.StartDateTime < ts2.StartDateTime && ts1.EndDateTime > ts2.EndDateTime) {
				logger.LogTrace("Time 1 is contained entirely within Time 2");
				return (ts2.StartDateTime, ts2.EndDateTime);
			}

			// Scenario 5
			if (ts1.StartDateTime >= ts2.StartDateTime && ts1.EndDateTime > ts2.EndDateTime) {
				logger.LogTrace("Time 1 starts after Time 2 and overlaps");
				return (ts1.StartDateTime, ts2.EndDateTime);
			}

			// Scenario 6
			if (ts1.StartDateTime < ts2.StartDateTime && ts1.EndDateTime <= ts2.EndDateTime) {
				logger.LogTrace("Time 2 starts after Time 1 and overlaps");
				return (ts2.StartDateTime, ts1.EndDateTime);
			}

			logger.LogError("Unable to identify intersection of dates");
			return null;

		}

    private IEnumerable<TimeSlot> ExpandRecurringSchedules(
			IList<RecurringSchedule> recurringSchedules, 
			DateTime startTime, DateTime endTime, ILogger logger)
    {
      
			var outList = new List<TimeSlot>();

			foreach (var r in recurringSchedules)
			{

					var expandRange = CalculateIntersectionOfTimeslots((startTime, endTime), (r.MinStartDateTime, r.MaxEndDateTime), logger);
					if (expandRange == null) continue;
					// Console.Out.WriteLine(expandRange.Value.ToString());

					var cts = CrontabSchedule.Parse(r.CronPattern);
					outList.AddRange(cts.GetNextOccurrences(expandRange.Value.begin, expandRange.Value.end)
						.Select(d => new TimeSlot {
							StartDateTime = d,
							EndDateTime = d.Add(r.Duration),
							Status = r.Status
						}));
			}

			return outList;

    }

    private IEnumerable<TimeSlot> ExpandScheduleItems(IList<ScheduleItem> scheduleItems, DateTime startTime, DateTime endTime)
    {
      
				// var scheduleIntersection = CalculateIntersectionOfTimeslots()

        return scheduleItems
        .Where(i => i.StartDateTime < endTime && i.EndDateTime > startTime)
        .Select(i => new TimeSlot() {
            StartDateTime = i.StartDateTime,
            EndDateTime = i.EndDateTime,
            Status = i.Status
        })
        .OrderBy(t => t.StartDateTime);

    }
  }
}
