using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fritz.ResourceManagement.Domain
{

	/// <summary>
	/// A one-off instance of an item on the schedule
	/// </summary>
	public class ScheduleItem : IValidatableObject
	{

		public int Id { get; set; }

		public int ScheduleId { get; set; }

		public ScheduleStatus Status { get; set; }

		public string Name { get; set; }

		public DateTime StartDateTime { get; set; }

		public DateTime EndDateTime { get; set; }

		/// <summary>
		/// Calculated from the difference in Start and End Times.
		/// </summary>
		public TimeSpan Duration
		{
			get
			{
				return EndDateTime.Subtract(StartDateTime);
			}
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{

			var results = new List<ValidationResult>();

			if (Status == default)
				results.Add(new ValidationResult($"{nameof(Status)} is required", new[] { nameof(Status) }));

			if (string.IsNullOrWhiteSpace(Name))
				results.Add(new ValidationResult($"{nameof(Name)} cannot be null, empty or consist of whitespace only", new[] { nameof(Name) }));
			else if (Name.Length > 50)
				results.Add(new ValidationResult($"{nameof(Name)} is greater than max length of 50", new[] { nameof(Name) }));

			if (StartDateTime == default)
				results.Add(new ValidationResult($"{nameof(StartDateTime)} is required", new[] { nameof(StartDateTime) }));

			if (EndDateTime == default)
				results.Add(new ValidationResult($"{nameof(EndDateTime)} is required", new[] { nameof(EndDateTime) }));

			if (EndDateTime < StartDateTime)
				results.Add(new ValidationResult($"{nameof(EndDateTime)} cannot be before {nameof(StartDateTime)}", new[] { nameof(StartDateTime), nameof(EndDateTime) }));
			else if (EndDateTime < StartDateTime.AddMinutes(10))
				results.Add(new ValidationResult($"{nameof(EndDateTime)} must be atleast 10 minutes after {nameof(StartDateTime)}", new[] { nameof(StartDateTime), nameof(EndDateTime) }));

			return results;

		}
	}

}
