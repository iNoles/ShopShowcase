namespace ShopShowcase.Models;

public class Product
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public List<ProductOption> Options { get; set; } = [];
    public List<ProductVariant> Variants { get; set; } = [];

    // If no variants, Price is null
    public decimal? Price => Variants.FirstOrDefault()?.Price;
}

public class ProductOption
{
    public required string Name { get; set; }             // e.g., "Size", "Color"
    public List<string>? Values { get; set; } = [];        // e.g., ["S", "M", "L"]
}

public class ProductVariant
{
    public required string Id { get; set; }
    public required string Title { get; set; }            // e.g., "Medium / Red"
    public required decimal Price { get; set; }
    public string SKU { get; set; } = string.Empty;
    public List<SelectedOption> SelectedOptions { get; set; } = [];  // maps to option values
    public bool Available { get; set; } = true;
}

public class SelectedOption
{
    public required string Name { get; set; }             // e.g., "Size"
    public required string Value { get; set; }            // e.g., "Medium"
}

