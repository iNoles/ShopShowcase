using System.Net.Http.Headers;
using ShopShowcase.GraphQL;
using ShopShowcase.Models;
using ProductVariant = ShopShowcase.Models.ProductVariant;

namespace ShopShowcase.Services;

public class ShopifyStorefrontService : IProductService
{
  private readonly GraphQLClient _graphqlClient;

  public ShopifyStorefrontService()
  {
    var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    httpClient.DefaultRequestHeaders.Add("X-Shopify-Storefront-Access-Token", AppConfig.ShopifyToken);

    _graphqlClient = new GraphQLClient(httpClient, AppConfig.ShopifyUrl);
  }

  public async Task<List<Product>> GetProductsAsync()
  {
    const string query = @"
        query {
          products(first: 10) {
            edges {
              node {
                id
                title
                description
                images(first: 1) {
                  edges {
                    node {
                      url
                    }
                  }
                }
                options {
                  name
                  values
                }
                variants(first: 20) {
                  edges {
                    node {
                      id
                      title
                      priceV2 {
                        amount
                        currencyCode
                      }
                      sku
                      selectedOptions {
                        name
                        value
                      }
                      availableForSale
                    }
                  }
                }
              }
            }
          }
        }";

    var result = await _graphqlClient.ExecuteAsync<ShopifyProductsResponseData>(query);

    if (result.Products?.Edges == null)
      return [];

    return [.. result.Products.Edges.Select(edge =>
    {
      var node = edge.Node;
      return new Product
      {
        Id = node.Id,
        Title = node.Title,
        Description = node.Description,
        ImageUrl = node.Images?.Edges.FirstOrDefault()?.Node.Url ?? string.Empty,
        Options = node.Options?.Select(o => new ProductOption
        {
          Name = o.Name,
          Values = o.Values ?? []
        }).ToList() ?? [],
        Variants = node.Variants?.Edges.Select(v => new ProductVariant
        {
          Id = v.Node.Id,
          Title = v.Node.Title,
          Price = decimal.TryParse(v.Node.PriceV2.Amount, out var price) ? price : 0,
          SKU = v.Node.Sku,
          Available = v.Node.AvailableForSale,
          SelectedOptions = [.. v.Node.SelectedOptions.Select(so => new SelectedOption
          {
            Name = so.Name,
            Value = so.Value
          })]
        }).ToList() ?? []
      };
    })];
  }

  public async Task<CheckoutResult?> CreateCheckoutAsync(IEnumerable<CartItem> items)
  {
    const string mutation = @"
      mutation checkoutCreate($lineItems: [CheckoutLineItemInput!]!) {
        checkoutCreate(input: { lineItems: $lineItems }) {
          checkout {
            id
            webUrl
          }
          userErrors {
            field
            message
          }
        }
      }";

    var lineItems = items.Select(i =>
    {
      var variantId = i.Variant?.Id ?? i.Product.Variants.FirstOrDefault()?.Id;
      return new
      {
        variantId,
        quantity = i.Quantity
      };
    })
    .Where(i => !string.IsNullOrEmpty(i.variantId)) // ensure null variant IDs are excluded
    .ToList();

    if (lineItems.Count == 0)
      throw new InvalidOperationException("No valid variants found for checkout.");

    var variables = new { lineItems };

    var response = await _graphqlClient.ExecuteAsync<CheckoutCreateResponse>(mutation, variables);

    var userErrors = response.CheckoutCreate?.UserErrors;
    if (userErrors is { Count: > 0 })
    {
      var message = string.Join("; ", userErrors.Select(e => $"{e.Field?.FirstOrDefault() ?? "unknown"}: {e.Message}"));
      throw new InvalidOperationException($"Checkout failed: {message}");
    }

    var checkout = response.CheckoutCreate?.Checkout;
    return checkout != null
        ? new CheckoutResult { CheckoutId = checkout.Id, WebUrl = checkout.WebUrl }
        : null;
  }
}