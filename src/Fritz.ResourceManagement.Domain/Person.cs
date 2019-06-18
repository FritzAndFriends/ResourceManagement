using System.Collections.Generic;
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

  }

}
