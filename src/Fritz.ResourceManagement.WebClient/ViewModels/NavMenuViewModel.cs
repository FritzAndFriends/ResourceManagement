namespace Fritz.ResourceManagement.WebClient.ViewModels
{
	public class NavMenuViewModel
	{
		public bool CollapseNavMenu { get; set; } = true;

		public string NavMenuCssClass => this.CollapseNavMenu ? "collapse" : null;

		public void ToggleNavMenu()
		{
			this.CollapseNavMenu = !this.CollapseNavMenu;
		}
	}
}
