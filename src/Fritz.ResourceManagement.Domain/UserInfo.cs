using System;
using System.Collections.Generic;
using System.Text;

namespace Fritz.ResourceManagement.Domain
{
  public class UserInfo
  {

    public bool IsAuthenticated { get; set; }

    public string Name { get; set; }

		public int ScheduleId { get; set; }

		public static class Claims
		{

			public const string PERSONID = "personid";

			public const string SCHEDULEID = "scheduleid";

		}

	}
}
