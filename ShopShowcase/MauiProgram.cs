using Microsoft.Extensions.Logging;
using ShopShowcase.Services;
using ShopShowcase.ViewModel;

namespace ShopShowcase;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Register ViewModels
		builder.Services.AddSingleton<ProductsViewModel>();

		builder.Services.AddSingleton<CartService>();
		builder.Services.AddTransient<CartViewModel>();
		builder.Services.AddTransient<CartPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
