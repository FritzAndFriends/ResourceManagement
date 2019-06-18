using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fritz.ResourceManagement.Web
{

  public static class ExtensionMethods
  {

	public static int? GetPersonId(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
	{

	  var outValue = claimsPrincipal.Claims
		  .First(c => c.Type == Data.MyUser.Claims.PERSONID)?.Value;
			
	  if (string.IsNullOrEmpty(outValue)) return null;

	  return int.Parse(outValue);

	}

  }

}
