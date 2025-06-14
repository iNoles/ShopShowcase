namespace ShopShowcase.GraphQL;

public class GraphQLResponse<T>
{
    public T? Data { get; set; }
    public List<GraphQLError>? Errors { get; set; }
}

public class GraphQLError
{
    public string Message { get; set; } = string.Empty;
    public List<string>? Path { get; set; }
}

