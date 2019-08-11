using Fritz.ResourceManagement.Domain;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fritz.ResourceManagement.WebClient.Security
{
  public class ServerAuthenticationStateProvider : AuthenticationStateProvider
  {
    private readonly HttpClient _httpClient;

    public ServerAuthenticationStateProvider(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
      var userInfo = await _httpClient.GetJsonAsync<UserInfo>("/user");

      var identity = userInfo.IsAuthenticated
          ? new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userInfo.Name), new Claim("scheduleid", userInfo.ScheduleId.ToString()) }, "serverauth")
          : new ClaimsIdentity();

      return new AuthenticationState(new ClaimsPrincipal(identity));
    }
  }
}
