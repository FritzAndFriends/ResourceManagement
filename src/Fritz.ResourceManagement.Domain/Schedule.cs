using System;
using System.Collections.Generic;

namespace Fritz.ResourceManagement.Domain
{
  public class Schedule {

    public int Id { get; set; }

    public IList<ScheduleItem> ScheduleItems { get; set; }

    public IList<RecurringSchedule> RecurringSchedules {get; set;}

    public IList<ScheduleException> ScheduleExceptions { get; set; }

  }

}
