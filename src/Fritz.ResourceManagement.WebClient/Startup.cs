using System.Net.Http;
using Fritz.ResourceManagement.WebClient.Data;
using Fritz.ResourceManagement.WebClient.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Fritz.ResourceManagement.WebClient
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{

			// Auth
			services.AddAuthorizationCore();
			services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

			services.AddScoped<HttpClient>(s => {
				var uriHelper = s.GetRequiredService<IUriHelper>();
				return new HttpClient
				{
					BaseAddress = new System.Uri(uriHelper.GetBaseUri())
				};
			});

			services.AddScoped<IScheduleRepository, ScheduleRepository>();

			services.AddScoped<Data.ScheduleState>();
			services.AddViewModels();

		}

		public void Configure(IComponentsApplicationBuilder app)
		{
			app.AddComponent<App>("app");
		}
	}
}
