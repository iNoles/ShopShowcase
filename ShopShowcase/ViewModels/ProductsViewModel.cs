using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ShopShowcase.Models;
using ShopShowcase.Services;

namespace ShopShowcase.ViewModels;

public class ProductsViewModel : ObservableObject
{
    private readonly IProductService _productService;

    public ObservableCollection<Product> Products { get; } = [];

    public ProductsViewModel(IProductService productService)
    {
        _productService = productService;
        _ = LoadProductsAsync();
    }

    private async Task LoadProductsAsync()
    {
        var products = await _productService.GetProductsAsync();
        Products.Clear();
        foreach (var product in products)
            Products.Add(product);
    }
}
