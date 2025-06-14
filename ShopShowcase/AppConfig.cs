namespace ShopShowcase;

public static class AppConfig
{
    public static bool UseMockData { get; set; } = true; // set to false to use real Shopify API
    public const string ShopifyUrl = "https://your-store.myshopify.com/api/2023-10/graphql.json";
    public const string ShopifyToken = "your-storefront-access-token";
}