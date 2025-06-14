using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ShopShowcase.Models;
using ShopShowcase.Services;

namespace ShopShowcase.ViewModel;

public class ProductsViewModel : ObservableObject
{
    private readonly IProductService _productService;

    public ObservableCollection<Product> Products { get; } = [];

    public ProductsViewModel()
    {
        _productService = AppConfig.UseMockData
            ? new MockProductService()
            : new ShopifyStorefrontService(); // must implement IProductService

        LoadProducts();
    }

    private async void LoadProducts()
    {
        var products = await _productService.GetProductsAsync();
        Products.Clear();
        foreach (var product in products)
            Products.Add(product);
    }
}
