using ShopShowcase.ViewModel;

namespace ShopShowcase;

public partial class CartPage : ContentPage
{
	public CartPage(CartViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}