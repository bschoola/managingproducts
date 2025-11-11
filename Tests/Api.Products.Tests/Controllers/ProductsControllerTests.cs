using Api.Products.Controllers;
using Domain.Products.Contracts;
using Domain.Products.Dto;
using FluentAssertions;
using Infrastructure.Products.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Products.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _controller = new ProductsController(_productServiceMock.Object);
        }

        [Fact]
        public async Task Products_ShouldReturnOkWithListOfProducts()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = 10.99m },
                new Product { Id = 2, Name = "Product 2", Description = "Description 2", Price = 20.99m }
            };

            _productServiceMock
                .Setup(x => x.GetProducts())
                .ReturnsAsync(expectedProducts);

            // Act
            var result = await _controller.Products();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().BeEquivalentTo(expectedProducts);
            _productServiceMock.Verify(x => x.GetProducts(), Times.Once);
        }

        [Fact]
        public async Task GetById_WithValidId_ShouldReturnOkWithProduct()
        {
            // Arrange
            var productId = 1;
            var expectedProduct = new Product 
            { 
                Id = productId, 
                Name = "Test Product", 
                Description = "Test Description", 
                Price = 15.99m 
            };

            _productServiceMock
                .Setup(x => x.GetById(productId))
                .ReturnsAsync(expectedProduct);

            // Act
            var result = await _controller.GetById(productId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().BeEquivalentTo(expectedProduct);
            _productServiceMock.Verify(x => x.GetById(productId), Times.Once);
        }

        [Fact]
        public async Task Create_WithValidProduct_ShouldReturnOkWithCreatedProduct()
        {
            // Arrange
            var productDto = new ProductDto
            {
                Name = "New Product",
                Description = "New Description",
                Price = 25.99m
            };

            var createdProduct = new Product
            {
                Id = 1,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price
            };

            _productServiceMock
                .Setup(x => x.CreateProduct(productDto))
                .ReturnsAsync(createdProduct);

            // Act
            var result = await _controller.Create(productDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().BeEquivalentTo(createdProduct);
            _productServiceMock.Verify(x => x.CreateProduct(productDto), Times.Once);
        }

        [Fact]
        public async Task Create_WithInvalidModelState_ShouldReturnBadRequest()
        {
            // Arrange
            var productDto = new ProductDto
            {
                Name = "Test",
                Description = "Test",
                Price = 10.99m
            };

            _controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await _controller.Create(productDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _productServiceMock.Verify(x => x.CreateProduct(It.IsAny<ProductDto>()), Times.Never);
        }

        [Fact]
        public async Task Update_WithValidProductAndId_ShouldReturnOkWithUpdatedProduct()
        {
            // Arrange
            var productId = 1;
            var productDto = new ProductDto
            {
                Name = "Updated Product",
                Description = "Updated Description",
                Price = 30.99m
            };

            var updatedProduct = new Product
            {
                Id = productId,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price
            };

            _productServiceMock
                .Setup(x => x.UpdateProduct(productId, productDto))
                .ReturnsAsync(updatedProduct);

            // Act
            var result = await _controller.Update(productId, productDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().BeEquivalentTo(updatedProduct);
            _productServiceMock.Verify(x => x.UpdateProduct(productId, productDto), Times.Once);
        }

        [Fact]
        public async Task Update_WithInvalidModelState_ShouldReturnBadRequest()
        {
            // Arrange
            var productId = 1;
            var productDto = new ProductDto
            {
                Name = "Test",
                Description = "Test",
                Price = 10.99m
            };

            _controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await _controller.Update(productId, productDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _productServiceMock.Verify(x => x.UpdateProduct(It.IsAny<int>(), It.IsAny<ProductDto>()), Times.Never);
        }

        [Fact]
        public async Task Delete_WithValidId_ShouldReturnNoContent()
        {
            // Arrange
            var productId = 1;

            _productServiceMock
                .Setup(x => x.Delete(productId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(productId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            _productServiceMock.Verify(x => x.Delete(productId), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenServiceThrowsException_ShouldPropagateException()
        {
            // Arrange
            var productId = 999;

            _productServiceMock
                .Setup(x => x.Delete(productId))
                .ThrowsAsync(new InvalidOperationException("Product does not exists"));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await _controller.Delete(productId));

            _productServiceMock.Verify(x => x.Delete(productId), Times.Once);
        }
    }
}
