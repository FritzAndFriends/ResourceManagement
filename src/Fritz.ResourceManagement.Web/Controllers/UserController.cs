using Fritz.ResourceManagement.Domain;
using Fritz.ResourceManagement.Web.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
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
		private readonly UserManager<MyUser> _UserManager;
		private readonly SignInManager<MyUser> _SignInManager;

		public UserController(UserManager<MyUser> userManager, SignInManager<MyUser> signInManager)
		{
			_UserManager = userManager;
			_SignInManager = signInManager;
		}

    [HttpGet("user")]
    public async Task<UserInfo> GetUserAsync()
    {

			// Cheer 1500 clintonrocksmith 30/07/19 

			// TODO: Fetch the schedule id appropriately
			var theUser = await _UserManager.GetUserAsync(User);


			return User.Identity.IsAuthenticated
					? new UserInfo { 
						Name = User.Identity.Name, 
						IsAuthenticated = true, 
						ScheduleId = theUser.ScheduleId.Value 
					}
          : _LoggedOutUser;
    }

    [HttpGet("user/signin")]
    public async Task SignIn(string redirectUri)
    {
      if (string.IsNullOrEmpty(redirectUri) || !Url.IsLocalUrl(redirectUri))
      {
        redirectUri = "/";
      }

			// Cheer 700 cpayette 31/07/19 
			await HttpContext.ChallengeAsync(
					new AuthenticationProperties { RedirectUri = redirectUri });

		}

    [HttpGet("user/signout")]
    public async Task<IActionResult> SignOut()
    {
			await _SignInManager.SignOutAsync();
      return Redirect("~/");
    }

  }

}
