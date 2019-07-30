using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Fritz.ResourceManagement.WebClient
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddViewModels();
		}

		public void Configure(IComponentsApplicationBuilder app)
		{
			app.AddComponent<App>("app");
		}
	}
}
