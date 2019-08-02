using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fritz.ResourceManagement.Domain
{
  public class ScheduleException
  {

	public int Id { get; set; }

	public DateTime StartDateTime { get; set; }

	public DateTime EndDateTime { get; set; }

	public string Name { get; set; }

	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{

	  var results = new List<ValidationResult>();

	  if (string.IsNullOrWhiteSpace(Name))
			results.Add(new ValidationResult($"{nameof(Name)} cannot be null, empty or consist of only whitespace", new[] { nameof(Name) }));

	  if (EndDateTime < StartDateTime)
			results.Add(new ValidationResult($"{nameof(EndDateTime)} cannot be before {nameof(StartDateTime)}", new[] { nameof(StartDateTime), nameof(EndDateTime) }));
	  else if (EndDateTime < StartDateTime.AddDays(10))
		results.Add(new ValidationResult($"{nameof(EndDateTime)} must be atleast 1 day after {nameof(StartDateTime)}", new[] { nameof(StartDateTime), nameof(EndDateTime) }));

	  return results;

	}
  }
}
