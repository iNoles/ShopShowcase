# 🛍️ ShopShowcase

**ShopShowcase** is a modern, cross-platform e-commerce mobile app built with **.NET MAUI**. It connects to the **Shopify Storefront API** via GraphQL to fetch product data, display variants and options, and manage a cart experience, making it a great starting point for mobile Shopify clients.

## 🚀 Features

- 📦 Displays real-time products from Shopify
- 🎨 Supports variant selection (e.g. Size, Color)
- 🛒 Add-to-cart functionality with quantity controls
- 📱 Cross-platform: Android, iOS, macOS, and Windows
- ⚡ MVVM architecture with data binding
- 🔗 Built-in GraphQL integration with error handling

## 📸 Screenshots

Coming Soon

## 🧰 Tech Stack

- [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/) for UI
- [Shopify Storefront API](https://shopify.dev/docs/api/storefront) via GraphQL
- `HttpClient` for API requests
- `System.Text.Json` for serialization
- `MVVM` Pattern

## ⚙️ Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- Visual Studio 2022+ with MAUI workload installed
- A Shopify Storefront Access Token & Storefront URL

### Setup

1. Clone the repo:

```bash
git clone https://github.com/iNoles/ShopShowcase.git
cd ShopShowcase
```

2. Configure your Shopify credentials in `AppConfig.cs`

3. Run the app:

```bash
dotnet build
dotnet maui run
```
