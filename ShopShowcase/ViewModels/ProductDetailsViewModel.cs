using System.Collections.ObjectModel;
using System.Windows.Input;
using ShopShowcase.Models;
using ShopShowcase.Services;

namespace ShopShowcase.ViewModel;

public class ProductDetailsViewModel : BaseViewModel
{
    private int _quantity = 1;
    public int Quantity
    {
        get => _quantity;
        set => SetProperty(ref _quantity, value > 0 ? value : 1);
    }

    private readonly CartService _cartService;

    public Product Product { get; }

    public ObservableCollection<OptionViewModel> Options { get; }

    private ProductVariant? _selectedVariant;
    public ProductVariant? SelectedVariant
    {
        get => _selectedVariant;
        private set => SetProperty(ref _selectedVariant, value);
    }

    public ICommand AddToCartCommand { get; }

    public ProductDetailsViewModel(Product product)
    {
        Product = product;
        _cartService = ServiceHelper.GetService<CartService>();

        Options = new ObservableCollection<OptionViewModel>(
            product.Options.Select(option =>
            {
                var initial = option.Values.FirstOrDefault() ?? string.Empty;
                var vm = new OptionViewModel(option.Name, option.Values, initial);
                vm.SelectedChanged += OnOptionSelectedChanged;
                return vm;
            }));

        UpdateSelectedVariant();
        AddToCartCommand = new Command(OnAddToCart);
    }

    private void OnOptionSelectedChanged(object? sender, SelectedOptionChangedEventArgs e)
    {
        UpdateSelectedVariant();
    }

    private void UpdateSelectedVariant()
    {
        var selectedOptions = Options.ToDictionary(o => o.Name, o => o.SelectedValue);

        SelectedVariant = Product.Variants.FirstOrDefault(variant =>
            variant.SelectedOptions.All(o =>
                selectedOptions.TryGetValue(o.Name, out var value) && value == o.Value
            ));
    }

    private void OnAddToCart()
    {
        _cartService.AddToCart(Product, SelectedVariant, Quantity);
        Shell.Current.DisplayAlert("Added", $"{Product.Title} added to cart.", "OK");
    }
}
