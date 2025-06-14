using ShopShowcase.Models;

namespace ShopShowcase.GraphQL;

public class ShopifyProductsResponseData
{
    public ShopifyProductsConnection Products { get; set; } = new();
}

public class ShopifyProductsConnection
{
    public List<ShopifyProductEdge> Edges { get; set; } = [];
}

public class ShopifyProductEdge
{
    public ShopifyProductNode Node { get; set; } = new();
}

public class ShopifyProductNode
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ShopifyImageConnection? Images { get; set; }
    public List<ShopifyOption>? Options { get; set; }
    public ShopifyVariantConnection? Variants { get; set; }
}

public class ShopifyImageConnection
{
    public List<ShopifyImageEdge> Edges { get; set; } = [];
}

public class ShopifyImageEdge
{
    public ShopifyImageNode Node { get; set; } = new();
}

public class ShopifyImageNode
{
    public string Url { get; set; } = string.Empty;
}

public class ShopifyOption
{
    public string Name { get; set; } = string.Empty;
    public List<string>? Values { get; set; }
}

public class ShopifyVariantConnection
{
    public List<ShopifyVariantEdge> Edges { get; set; } = [];
}

public class ShopifyVariantEdge
{
    public ShopifyVariantNode Node { get; set; } = new();
}

public class ShopifyVariantNode
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public bool AvailableForSale { get; set; }
    public ShopifyPrice PriceV2 { get; set; } = new();
    public List<SelectedOption> SelectedOptions { get; set; } = [];
}

public class ShopifyPrice
{
    public string Amount { get; set; } = "0.0";
    public string CurrencyCode { get; set; } = "USD";
}
