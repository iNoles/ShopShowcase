using Foundation;
using SafariServices;
using UIKit;

namespace ShopShowcase.Services;

public partial class BrowserService
{
    static partial void OpenBrowser(string url)
    {
        // Get the current active window scene's key window's root view controller
        var windowScene = UIApplication.SharedApplication
            .ConnectedScenes
            .OfType<UIWindowScene>()
            .FirstOrDefault(ws => ws.ActivationState == UISceneActivationState.ForegroundActive);

        var window = windowScene?.Windows.FirstOrDefault(w => w.IsKeyWindow);
        var rootViewController = (window?.RootViewController) ?? throw new InvalidOperationException("Unable to find root view controller to present SFSafariViewController.");

        // Traverse to top-most view controller
        while (rootViewController.PresentedViewController != null)
        {
            rootViewController = rootViewController.PresentedViewController;
        }

        var safariViewController = new SFSafariViewController(new NSUrl(url));
        rootViewController.PresentViewController(safariViewController, true, null);
    }
}
