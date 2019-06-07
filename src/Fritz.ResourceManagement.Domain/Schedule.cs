using System;
using System.Collections.Generic;

namespace Fritz.ResourceManagement.Domain
{
  public class Schedule
  {

	public int Id { get; set; }

	/// <summary>
	/// One off scheduled time.  A one-time appointment like a doctor's appointment, haircut.  
	/// </summary>
	public IList<ScheduleItem> ScheduleItems { get; set; } = new List<ScheduleItem>();

	/// <summary>
	/// An event that happens regularly over time.  This could be bowling league night, scheduled yoga classes, etc
	/// </summary>
	public IList<RecurringSchedule> RecurringSchedules { get; set; } = new List<RecurringSchedule>();

	/// <summary>
	/// Block or change the state of the schedule for events like annual vacation holiday
	/// </summary>
	public IList<ScheduleException> ScheduleExceptions { get; set; } = new List<ScheduleException>();

  }

}
