using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Fritz.ResourceManagement.WebClient
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<ViewModels.AvailabilityViewModel>();
			services.AddTransient<ViewModels.DayPickerViewModel>();
			services.AddTransient<ViewModels.DayViewViewModel>();
		  services.AddTransient<ViewModels.ManagerScheduleViewViewModel>();
		  services.AddTransient<ViewModels.RecurrenceDataEntryViewModel>();	  
		}

		public void Configure(IComponentsApplicationBuilder app)
		{
			app.AddComponent<App>("app");
		}
	}
}
