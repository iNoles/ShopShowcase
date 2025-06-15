using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ShopShowcase.GraphQL;

public class GraphQLClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly string _baseUrl;

    public GraphQLClient(HttpClient httpClient, string baseUrl)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        _baseUrl = baseUrl;

        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<T> ExecuteAsync<T>(string query, object? variables = null)
    {
        var requestBody = new
        {
            query,
            variables
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody, _jsonOptions), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_baseUrl, content);
        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync();

        var graphQLResponse = await JsonSerializer.DeserializeAsync<GraphQLResponse<T>>(stream, _jsonOptions) ?? throw new Exception("Invalid GraphQL response.");
        if (graphQLResponse.Errors?.Count > 0)
            throw new Exception("GraphQL errors: " + string.Join(", ", graphQLResponse.Errors.Select(e => e.Message)));

        return graphQLResponse.Data!;
    }
}
