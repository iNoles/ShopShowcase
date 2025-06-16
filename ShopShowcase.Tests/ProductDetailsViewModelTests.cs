using Xunit;
using ShopShowcase.ViewModels;
using ShopShowcase.Models;
using System.Collections.Generic;

namespace ShopShowcase.Tests.ViewModels
{
    public class ProductDetailsViewModelTests
    {
        [Fact]
        public void SelectedVariant_ShouldUpdate_WhenOptionChanges()
        {
            // Arrange
            var viewModel = new ProductDetailsViewModel
            {
                Product = new Product
                {
                    Id = "p1",
                    Title = "Test Product",
                    Options = new List<ProductOption>
                    {
                        new ProductOption { Name = "Size", Values = new List<string> { "S", "M", "L" } }
                    },
                    Variants = new List<ProductVariant>
                    {
                        new ProductVariant
                        {
                            Id = "v1",
                            Title = "Medium",
                            Price = 19.99m,
                            SelectedOptions = new List<SelectedOption>
                            {
                                new SelectedOption { Name = "Size", Value = "M" }
                            }
                        }
                    }
                }
            };

            // Act
            viewModel.SelectOption("Size", "M");

            // Assert
            Assert.NotNull(viewModel.SelectedVariant);
            Assert.Equal("v1", viewModel.SelectedVariant!.Id);
            Assert.Equal("M", viewModel.SelectedVariant.SelectedOptions[0].Value);
        }
    }
}
