using ShopShowcase.Models;
using ShopShowcase.ViewModel;

namespace ShopShowcase;

public partial class ProductDetailsPage : ContentPage
{
	private ProductDetailsViewModel ViewModel => (ProductDetailsViewModel)BindingContext;

	public ProductDetailsPage(Product product)
	{
		InitializeComponent();
		BindingContext = new ProductDetailsViewModel(product);
	}

	private void OnIncreaseQuantity(object sender, EventArgs e)
	{
		ViewModel.Quantity++;
	}

	private void OnDecreaseQuantity(object sender, EventArgs e)
	{
		if (ViewModel.Quantity > 1)
			ViewModel.Quantity--;
	}
}
