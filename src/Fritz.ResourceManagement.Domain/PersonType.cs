using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fritz.ResourceManagement.Domain
{

  public class PersonType
  {

    public int Id { get; set; }

    public string Name { get; set; }

		public ICollection<PersonPersonType> PersonPersonTypes { get; set; }

	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{

	  var results = new List<ValidationResult>();

	  if (string.IsNullOrWhiteSpace(Name))
			results.Add(new ValidationResult($"{nameof(Name)} cannot be null, empty or consist of only whitespace", new[] { nameof(Name) }));

	  return results;

	}

  }

}
