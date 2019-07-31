using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fritz.ResourceManagement.Domain
{
	public static class ExtensionMethods
	{

		public static int? GetPersonId(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
		{

			return claimsPrincipal.GetClaimValueAsInt(UserInfo.Claims.PERSONID);

		}

		public static int? GetClaimValueAsInt(this System.Security.Claims.ClaimsPrincipal claimsPrincipal, string claimName)
		{

			var outValue = claimsPrincipal.Claims
				.First(c => c.Type == claimName)?.Value;

			if (string.IsNullOrEmpty(outValue)) return null;

			return int.Parse(outValue);

		}

	}
}
