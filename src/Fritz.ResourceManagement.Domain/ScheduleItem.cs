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

	  if (EndDateTime < StartDateTime)
		results.Add(new ValidationResult($"{nameof(EndDateTime)} cannot be before {nameof(StartDateTime)}", new[] { nameof(StartDateTime), nameof(EndDateTime) }));

	  return results;

	}
  }

}
