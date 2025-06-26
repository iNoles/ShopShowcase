namespace ShopShowcase.GraphQL;

public class CheckoutCreateResponse
{
    public CheckoutCreatePayload? CheckoutCreate { get; set; }
}

public class CheckoutCreatePayload
{
    public CheckoutObject? Checkout { get; set; }
    public List<UserError>? UserErrors { get; set; }
}

public class CheckoutObject
{
    public string Id { get; set; } = string.Empty;
    public string WebUrl { get; set; } = string.Empty;
}

public class UserError
{
    public List<string>? Field { get; set; }
    public string Message { get; set; } = string.Empty;
}
