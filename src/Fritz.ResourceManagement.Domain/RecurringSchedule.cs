using System;
using System.Collections.Generic;

namespace Fritz.ResourceManagement.Domain
{
	/// <summary>
	/// An event that re-occurs on the schedule given a recurrence pattern defined in CronPattern
	/// </summary>
  public class RecurringSchedule {

    public int Id { get; set; }

    public int ScheduleId { get; set; }

    public ScheduleStatus Status { get; set; }

		public string Name { get; set; }

    public string CronPattern { get; set; }

		public TimeSpan Duration { get; set; }

    public DateTime MinStartDateTime { get; set; }

    public DateTime MaxEndDateTime { get; set; }

  }


}
