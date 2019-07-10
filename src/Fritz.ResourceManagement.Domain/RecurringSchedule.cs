using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

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

		[NotMapped()]
		public string DurationText {
			get { return Duration.ToString(); }
			set { Duration = TimeSpan.Parse(value); }
		}

    public DateTime MinStartDateTime { get; set; }

    public DateTime MaxEndDateTime { get; set; }

  }

}
