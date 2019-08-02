using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Fritz.ResourceManagement.Domain
{
	[DebuggerDisplay("{Name}: {StartDateTime}-{EndDateTime}")]
	public class TimeSlot
	{

		public string Name { get; set; }

		public DateTime StartDateTime { get; set; }

		public DateTime EndDateTime { get; set; }

		public TimeSpan Duration { get { return EndDateTime.Subtract(StartDateTime); } }

		public ScheduleStatus Status { get; set; }

		public bool Overlaps(TimeSlot ts2)
		{

			return this.StartDateTime <= ts2.EndDateTime && this.EndDateTime >= ts2.StartDateTime;

		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{

			var results = new List<ValidationResult>();

			if (Status == default)
				results.Add(new ValidationResult($"{nameof(Status)} cannot have the default value of {default(ScheduleStatus)}", new[] { nameof(Status) }));

			if (StartDateTime == default)
				results.Add(new ValidationResult($"{nameof(StartDateTime)} is required", new[] { nameof(StartDateTime) }));

			if (EndDateTime == default)
				results.Add(new ValidationResult($"{nameof(EndDateTime)} is required", new[] { nameof(EndDateTime) }));

			if (EndDateTime < StartDateTime)
				results.Add(new ValidationResult($"{nameof(EndDateTime)} cannot be before {nameof(StartDateTime)}", new[] { nameof(StartDateTime), nameof(EndDateTime) }));

			return results;

		}
	}
}
