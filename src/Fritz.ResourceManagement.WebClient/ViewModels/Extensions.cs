using Fritz.ResourceManagement.WebClient.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Fritz.ResourceManagement.WebClient
{
	public static class Extensions
	{
		public static void AddViewModels(this IServiceCollection services)
		{
			services.AddTransient<AvailabilityViewModel>();
			services.AddTransient<DayPickerViewModel>();
			services.AddTransient<DayViewViewModel>();
			services.AddTransient<ManagerScheduleViewViewModel>();
			services.AddTransient<NavMenuViewModel>();
			services.AddTransient<RecurrenceDataEntryViewModel>();
		}
	}
}
