using System;
using System.Diagnostics;

namespace Fritz.ResourceManagement.Domain
{
	[DebuggerDisplay("{Name}: {StartDateTime}-{EndDateTime}")]
  public class TimeSlot {

		public string Name { get; set; }

		public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

		public TimeSpan Duration { get { return EndDateTime.Subtract(StartDateTime); } }

    public ScheduleStatus Status { get; set; }

		public bool Overlaps(TimeSlot ts2)
		{

			return this.StartDateTime <= ts2.EndDateTime && this.EndDateTime >= ts2.StartDateTime;

		}

  }

}
