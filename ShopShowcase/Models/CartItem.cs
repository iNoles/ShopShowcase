namespace ShopShowcase.Models;

public class CartItem
{
    public required Product Product { get; set; }
    public ProductVariant? Variant { get; set; }  // optional if product has no variants
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice => Variant?.Price ?? Product.Price ?? 0;
    public decimal TotalPrice => UnitPrice * Quantity;
    public string? SKU => Variant?.SKU;
    public string DisplayTitle => Variant != null ? $"{Product.Title} ({Variant.Title})" : Product.Title;
    public override string ToString() =>
    $"{DisplayTitle} x{Quantity} - {TotalPrice:C}";
}

