namespace ShopShowcase.Services;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceHelper
{
    public static T GetService<T>() where T : class
    {
        return IPlatformApplication.Current?.Services.GetRequiredService<T>()
            ?? throw new InvalidOperationException($"Service of type {typeof(T)} not found.");
    }
}
