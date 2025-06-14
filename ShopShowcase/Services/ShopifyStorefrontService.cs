using System.Net.Http.Headers;
using ShopShowcase.GraphQL;
using ShopShowcase.Models;

namespace ShopShowcase.Services;

public class ShopifyStorefrontService : IProductService
{
  private readonly GraphQLClient _graphqlClient;

  public ShopifyStorefrontService()
  {
    var httpClient = new HttpClient
    {
      BaseAddress = new Uri(AppConfig.ShopifyUrl)
    };

    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    httpClient.DefaultRequestHeaders.Add("X-Shopify-Storefront-Access-Token", AppConfig.ShopifyToken);

    _graphqlClient = new GraphQLClient(httpClient);
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

    return result.Products.Edges.Select(edge =>
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
          SelectedOptions = v.Node.SelectedOptions.Select(so => new SelectedOption
          {
            Name = so.Name,
            Value = so.Value
          }).ToList()
        }).ToList() ?? []
      };
    }).ToList();
  }
}
