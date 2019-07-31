using Microsoft.AspNetCore.Identity;

namespace Fritz.ResourceManagement.Web.Data
{
	public class MyUser : IdentityUser
	{

		public int? PersonId { get; set; }

		public int? ScheduleId { get; set; }

	}
}
