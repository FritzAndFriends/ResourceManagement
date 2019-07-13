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

	  if (Status is default(ScheduleStatus))
		results.Add(new ValidationResult($"{nameof(Status)} cannot have the default value of {default(ScheduleStatus)}", new[] { nameof(Status) }));

	  if (EndDateTime < StartDateTime)
			results.Add(new ValidationResult($"{nameof(EndDateTime)} cannot be before {nameof(StartDateTime)}", new[] { nameof(StartDateTime), nameof(EndDateTime) }));

	  return results;

	}
  }
}
