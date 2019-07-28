namespace Fritz.ResourceManagement.WebClient.ViewModels
{
	public class NavMenuViewModel
	{
		private bool collapseNavMenu { get; set; } = true;

		public string NavMenuCssClass => this.collapseNavMenu ? "collapse" : null;

		public void ToggleNavMenu()
		{
			this.collapseNavMenu = !this.collapseNavMenu;
		}
	}
}
