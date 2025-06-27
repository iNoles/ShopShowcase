using ShopShowcase.Models;

namespace ShopShowcase.Services;

public interface IProductService
{
    Task<List<Product>> GetProductsAsync();
    Task<CheckoutResult?> CreateCheckoutAsync(IEnumerable<CartItem> items);
    Task<CheckoutResult?> UpdateCheckoutAsync(string checkoutId, IEnumerable<CartItem> items);
}
