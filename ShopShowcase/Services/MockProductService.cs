using ShopShowcase.Models;

namespace ShopShowcase.Services;

public class MockProductService : IProductService
{
    private static readonly Random _random = new();

    public Task<List<Product>> GetProductsAsync()
    {
        var products = new List<Product>();

        for (int i = 1; i <= 20; i++)
        {
            var product = new Product
            {
                Id = $"gid://shopify/Product/{i}",
                Title = $"Mock Product {i}",
                Description = $"This is a description for mock product {i}.",
                ImageUrl = $"https://placehold.co/300x300.png?text=Product+{i}",
                Variants = GetVariants(i),
                Options = GetOptions(i)
            };

            products.Add(product);
        }

        return Task.FromResult(products);
    }

    private static List<ProductVariant> GetVariants(int index)
    {
        var variants = new List<ProductVariant>();
        decimal basePrice = 9.99m + index;

        switch (index % 4)
        {
            case 1:
                // No variants — simulate single variant fallback
                variants.Add(new ProductVariant
                {
                    Id = $"variant-{index}-default",
                    Title = "Default",
                    Price = basePrice,
                    SKU = $"SKU-{index}-D"
                });
                break;

            case 2:
                // Size variants
                variants.AddRange(
                [
                    new ProductVariant { Id = $"variant-{index}-s", Title = "Size: Small", Price = basePrice, SKU = $"SKU-{index}-S" },
                    new ProductVariant { Id = $"variant-{index}-m", Title = "Size: Medium", Price = basePrice + 1, SKU = $"SKU-{index}-M" },
                    new ProductVariant { Id = $"variant-{index}-l", Title = "Size: Large", Price = basePrice + 2, SKU = $"SKU-{index}-L" }
                ]);
                break;

            case 3:
                // Color variants
                variants.AddRange(
                [
                    new ProductVariant { Id = $"variant-{index}-red", Title = "Color: Red", Price = basePrice, SKU = $"SKU-{index}-R" },
                    new ProductVariant { Id = $"variant-{index}-green", Title = "Color: Green", Price = basePrice + 1, SKU = $"SKU-{index}-G" },
                    new ProductVariant { Id = $"variant-{index}-blue", Title = "Color: Blue", Price = basePrice + 2, SKU = $"SKU-{index}-B" }
                ]);
                break;

            case 0:
                // Size and Color variants
                var sizes = new[] { "Small", "Medium", "Large" };
                var colors = new[] { "Black", "White", "Gray" };
                foreach (var size in sizes)
                {
                    foreach (var color in colors)
                    {
                        variants.Add(new ProductVariant
                        {
                            Id = $"variant-{index}-{size.ToLower()}-{color.ToLower()}",
                            Title = $"Size: {size}, Color: {color}",
                            Price = basePrice + _random.Next(0, 3),
                            SKU = $"SKU-{index}-{size[0]}{color[0]}"
                        });
                    }
                }
                break;
        }

        return variants;
    }

    private static List<ProductOption> GetOptions(int index)
    {
        var options = new List<ProductOption>();

        switch (index % 4)
        {
            case 2:
                options.Add(new ProductOption { Name = "Size", Values = ["Small", "Medium", "Large"] });
                break;

            case 3:
                options.Add(new ProductOption { Name = "Color", Values = ["Red", "Green", "Blue"] });
                break;

            case 0:
                options.Add(new ProductOption { Name = "Size", Values = ["Small", "Medium"] });
                options.Add(new ProductOption { Name = "Color", Values = ["Black", "White"] });
                break;
        }

        return options;
    }

    public Task<CheckoutResult?> CreateCheckoutAsync(IEnumerable<CartItem> items)
    {
        return Task.FromResult<CheckoutResult?>(null);
    }

    public Task<CheckoutResult?> UpdateCheckoutAsync(string checkoutId, IEnumerable<CartItem> items)
    {
        return Task.FromResult<CheckoutResult?>(null);
    }
}
