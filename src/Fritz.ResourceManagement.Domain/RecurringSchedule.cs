using System;
using System.Collections.Generic;

namespace Fritz.ResourceManagement.Domain
{
  public class RecurringSchedule {

    public int Id { get; set; }

    public int ScheduleId { get; set; }

    public ScheduleStatus Status { get; set; }

    public string CronPattern { get; set; }

    public DateTime MinStartDateTime { get; set; }

    public DateTime MaxEndDateTime { get; set; }

  }


}
