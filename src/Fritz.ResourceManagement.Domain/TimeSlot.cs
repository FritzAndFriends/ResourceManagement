using System;

namespace Fritz.ResourceManagement.Domain
{
  public class TimeSlot {

		public string Name { get; set; }

		public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

		public TimeSpan Duration { get { return EndDateTime.Subtract(StartDateTime); } }

    public ScheduleStatus Status { get; set; }

  }

}
