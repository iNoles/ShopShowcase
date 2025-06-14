using System.Collections.ObjectModel;
using System.Windows.Input;
using ShopShowcase.Models;
using ShopShowcase.Services;

namespace ShopShowcase.ViewModel;

public class CartViewModel : BaseViewModel
{
    private readonly CartService _cartService;

    public ReadOnlyObservableCollection<CartItem> Items => _cartService.Items;

    public bool HasItems => Items.Any();

    public decimal Total => Items.Sum(item => item.TotalPrice);

    public ICommand CheckoutCommand { get; }

    public CartViewModel(CartService cartService)
    {
        _cartService = cartService;

        _cartService.CartChanged += (s, e) =>
        {
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(HasItems));
            OnPropertyChanged(nameof(Total));
        };

        CheckoutCommand = new Command(OnCheckout);
    }

    private void OnCheckout()
    {
        Shell.Current.DisplayAlert("Checkout", "Proceeding to in-app browser...", "OK");
        // You can launch your in-app browser logic here
    }
}
