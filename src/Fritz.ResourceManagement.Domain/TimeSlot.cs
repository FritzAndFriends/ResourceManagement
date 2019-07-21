using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fritz.ResourceManagement.Domain
{
  public class TimeSlot
  {

	public DateTime StartDateTime { get; set; }

	public DateTime EndDateTime { get; set; }

	public ScheduleStatus Status { get; set; }

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
