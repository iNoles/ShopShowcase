using ShopShowcase.Models;

namespace ShopShowcase.Services;

public interface IProductService
{
    Task<List<Product>> GetProductsAsync();
}
