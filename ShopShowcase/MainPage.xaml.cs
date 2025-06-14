using ShopShowcase.Models;
using ShopShowcase.ViewModel;

namespace ShopShowcase;

public partial class MainPage : ContentPage
{
	public MainPage(ProductsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private void ProductsCollectionView_SizeChanged(object sender, EventArgs e)
	{
		var width = ProductsCollectionView.Width;

		if (width < 600)
		{
			ProductsCollectionView.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical);
		}
		else
		{
			ProductsCollectionView.ItemsLayout = new GridItemsLayout(4, ItemsLayoutOrientation.Vertical);
		}
	}

	private async void ProductsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.Count > 0 && e.CurrentSelection[0] is Product selectedProduct)
		{
			await Navigation.PushAsync(new ProductDetailsPage(selectedProduct));
			((CollectionView)sender).SelectedItem = null; // Deselect
		}
	}

}
