using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using CronEspresso.NETCore.Utils;

namespace Fritz.ResourceManagement.Domain
{
  /// <summary>
  /// An event that re-occurs on the schedule given a recurrence pattern defined in CronPattern
  /// </summary>
  public class RecurringSchedule
  {

	public int Id { get; set; }

	public int ScheduleId { get; set; }

	public ScheduleStatus Status { get; set; }

	public string Name { get; set; }

	public string CronPattern { get; set; }

	public TimeSpan Duration { get; set; }

		[NotMapped()]
		public string DurationText {
			get { return Duration.ToString(); }
			set { Duration = TimeSpan.Parse(value); }
		}

	public DateTime MinStartDateTime { get; set; }

	public DateTime MaxEndDateTime { get; set; }

	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{

	  var results = new List<ValidationResult>();

	  if (string.IsNullOrWhiteSpace(Name))
		results.Add(new ValidationResult($"{nameof(Name)} cannot be null, empty or consist of only whitespace", new[] { nameof(Name) }));

	  if (string.IsNullOrWhiteSpace(CronPattern))
		results.Add(new ValidationResult($"{nameof(CronPattern)} is required", new[] { nameof(CronPattern) }));
	  else
	  {
		var cronValidationResult = CronHelpers.ValidateCron(CronPattern);
		if (!cronValidationResult.IsValidCron)
		  results.Add(new ValidationResult($"{nameof(CronPattern) } is not a valid Cron pattern,  {cronValidationResult.ValidationMessage}", new[] { nameof(CronPattern) }));
	  }

	  if (Duration.TotalMinutes < 30)
		results.Add(new ValidationResult($"{nameof(Duration)} must be atleast 30 minutes", new[] { nameof(Duration) }));

	  if (MinStartDateTime == default)
		results.Add(new ValidationResult($"{nameof(MinStartDateTime)} is required", new[] { nameof(MinStartDateTime) }));

	  if (MaxEndDateTime == default)
		results.Add(new ValidationResult($"{nameof(MaxEndDateTime)} is required", new[] { nameof(MaxEndDateTime) }));

	  if (MaxEndDateTime < MinStartDateTime)
		results.Add(new ValidationResult($"{nameof(MaxEndDateTime)} cannot be before {nameof(MinStartDateTime)}", new[] { nameof(MaxEndDateTime), nameof(MinStartDateTime) }));

	  return results;
	}
  }
}
