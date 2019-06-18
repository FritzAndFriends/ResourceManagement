using System;

namespace Fritz.ResourceManagement.Domain
{
  public class ScheduleException {

		public int Id { get; set; }

		public DateTime StartDateTime { get; set; }

		public DateTime EndDateTime { get; set; }

		public string Name { get; set; }

	}

}
