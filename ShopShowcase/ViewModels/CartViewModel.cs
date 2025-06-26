using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using ShopShowcase.Models;
using ShopShowcase.Services;

namespace ShopShowcase.ViewModels;

public class CartViewModel : BaseViewModel
{
    private readonly CartService _cartService;

    private readonly IProductService _productService;

    public ReadOnlyObservableCollection<CartItem> Items => _cartService.Items;

    public bool HasItems => Items.Any();

    public decimal Total => Items.Sum(item => item.TotalPrice);

    public IAsyncRelayCommand CheckoutCommand { get; }

    public CartViewModel(CartService cartService, IProductService productService)
    {
        _cartService = cartService;
        _productService = productService;

        _cartService.CartChanged += (s, e) =>
        {
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(HasItems));
            OnPropertyChanged(nameof(Total));
        };

        CheckoutCommand = new AsyncRelayCommand(OnCheckoutAsync);
    }

    private async Task OnCheckoutAsync()
    {
        if (!Items.Any())
        {
            await Shell.Current.DisplayAlert("Cart", "Your cart is empty.", "OK");
            return;
        }

        var result = await _productService.CreateCheckoutAsync(_cartService.Items);

        if (result != null)
        {
            await Shell.Current.DisplayAlert("Checkout", "Opening browser...", "OK");
            await Browser.Default.OpenAsync(result.WebUrl, BrowserLaunchMode.SystemPreferred);
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "Unable to start checkout.", "OK");
        }
    }
}
