namespace ShopShowcase;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(CartPage), typeof(CartPage));
	}

	private async void OnCartClicked(object sender, EventArgs e)
	{
		await Current.GoToAsync(nameof(CartPage));
	}
}
