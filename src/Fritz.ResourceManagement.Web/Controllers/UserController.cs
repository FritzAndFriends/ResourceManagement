using Fritz.ResourceManagement.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fritz.ResourceManagement.Web.Controllers
{

	[ApiController]
  public class UserController : Controller
  {

    private static readonly UserInfo _LoggedOutUser = new UserInfo { IsAuthenticated = false };

    [HttpGet("user")]
    public UserInfo GetUser()
    {
      return User.Identity.IsAuthenticated
          ? new UserInfo { Name = User.Identity.Name, IsAuthenticated = true }
          : _LoggedOutUser;
    }

    [HttpGet("user/signin")]
    public async Task SignIn(string redirectUri)
    {
      if (string.IsNullOrEmpty(redirectUri) || !Url.IsLocalUrl(redirectUri))
      {
        redirectUri = "/";
      }

			// TODO: Challenge appropriately for our authentication scheme
      //await HttpContext.ChallengeAsync(
      //    TwitterDefaults.AuthenticationScheme,
      //    new AuthenticationProperties { RedirectUri = redirectUri });
    }

    [HttpGet("user/signout")]
    public async Task<IActionResult> SignOut()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      return Redirect("~/");
    }

  }

}
