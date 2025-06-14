using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ShopShowcase.Models;

namespace ShopShowcase.Services;

public class CartService
{
    private readonly ObservableCollection<CartItem> _items = [];

    public ReadOnlyObservableCollection<CartItem> Items { get; }

    public event NotifyCollectionChangedEventHandler? CartChanged
    {
        add => _items.CollectionChanged += value;
        remove => _items.CollectionChanged -= value;
    }

    public CartService()
    {
        Items = new ReadOnlyObservableCollection<CartItem>(_items);
    }

    public void AddToCart(Product product, ProductVariant? variant = null, int quantity = 1)
    {
        if (quantity <= 0) return;

        var existing = _items.FirstOrDefault(
            i => i.Product.Id == product.Id && i.Variant?.Id == variant?.Id);

        if (existing != null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            _items.Add(new CartItem
            {
                Product = product,
                Variant = variant,
                Quantity = quantity
            });
        }
    }

    public void RemoveFromCart(CartItem item)
    {
        _items.Remove(item);
    }

    public void ClearCart() => _items.Clear();

    public int CartCount() => _items.Sum(i => i.Quantity);
}
