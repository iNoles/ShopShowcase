using AndroidX.Browser.CustomTabs;

namespace ShopShowcase.Services;

public partial class BrowserService
{
    static partial void OpenBrowser(string url)
    {
        var context = Android.App.Application.Context;

        var builder = new CustomTabsIntent.Builder();
        var customTabsIntent = builder.Build();

        customTabsIntent.LaunchUrl(context, Android.Net.Uri.Parse(url)!);
    }
}
