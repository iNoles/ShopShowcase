namespace ShopShowcase.GraphQL;

public class CheckoutCreateResponse
{
    public CheckoutPayload? CheckoutCreate { get; set; }
}

public class CheckoutUpdateResponse
{
    public CheckoutPayload? CheckoutLineItemsUpdate { get; set; }
}

public class CheckoutPayload
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
