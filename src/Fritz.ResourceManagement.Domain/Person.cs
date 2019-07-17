using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Data;

namespace Fritz.ResourceManagement.Domain
{
  public class Person
  {

	public int Id { get; set; }

	public string GivenName { get; set; }

	public string MiddleName { get; set; }

	public string SurName { get; set; }

	public IList<PersonPersonType> PersonPersonType { get; set; }

	public string PhoneNumber { get; set; }

	public Schedule Schedule { get; set; }

	#region Foreign Keys

	public int ScheduleId { get; set; }

	#endregion

	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{

	  var results = new List<ValidationResult>();

	  if (string.IsNullOrWhiteSpace(GivenName))
			results.Add(new ValidationResult($"{nameof(GivenName)} cannot be null, empty or consist of whitespace only", new[] { nameof(GivenName) }));

	  if (string.IsNullOrEmpty(SurName))
			results.Add(new ValidationResult($"{nameof(SurName)} cannot be null, empty or consist of whitespace only", new[] { nameof(GivenName) }));

	  if (string.IsNullOrWhiteSpace(PhoneNumber))
			results.Add(new ValidationResult($"{nameof(PhoneNumber)} cannot be null, empty or consist of whitespace only", new[] { nameof(PhoneNumber) }));

	  if (PersonPersonType.Count == 0)
			results.Add(new ValidationResult($"{nameof(PersonPersonType)} must contain atleast 1 item", new[] { nameof(PersonPersonType) }));

	  return results;
	}

  }

}
