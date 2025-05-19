using AutoMapper;
using FluentAssertions;
using FluentResults;
using Moq;
using StockControlAPI.Domain.Dtos;
using StockControlAPI.Domain.Model;
using StockControlAPI.Service.Interfaces;
using StockControlAPI.Service.Service;

namespace StockControlAPI.Tests.Service
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IBaseValidator<ProductDto>> _validatorMock = new();
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _service = new ProductService(_repositoryMock.Object, _mapperMock.Object, _validatorMock.Object);
        }

        [Fact]
        public void AddProduct_WithValidDates_ShouldAddAndReturnProduct()
        {
            var dto = new ProductDto { ManufactureDate = DateTime.Today, ExpiryDate = DateTime.Today.AddDays(30) };
            var product = new Product { Id = 1, Description = "Test Product" };

            _validatorMock.Setup(v => v.Validate(dto)).Returns(Result.Ok());
            _mapperMock.Setup(m => m.Map<Product>(dto)).Returns(product);
            _repositoryMock.Setup(r => r.AddProduct(product)).Returns(product);

            var result = _service.AddProduct(dto);

            result.Should().Be(product);
            _repositoryMock.Verify(r => r.AddProduct(product), Times.Once);
        }

        [Fact]
        public void AddProduct_WithInvalidDates_ShouldThrowArgumentException()
        {
            var dto = new ProductDto { ManufactureDate = DateTime.Today, ExpiryDate = DateTime.Today.AddDays(-1) };

            _validatorMock.Setup(v => v.Validate(dto)).Returns(Result.Fail("Invalid dates"));

            var ex = Assert.Throws<ArgumentException>(() => _service.AddProduct(dto));
            Assert.Contains("Invalid", ex.Message);
        }

        [Fact]
        public void GetProducts_ShouldReturnList_WhenFound()
        {
            var list = new List<Product> { new Product { Id = 1 } };
            _repositoryMock.Setup(r => r.GetProducts(true, 0, 10)).Returns(list);

            var result = _service.GetProducts(true, 0, 10);

            Assert.Equal(list, result);
        }

        [Fact]
        public void GetProducts_ShouldThrow_WhenNoneFound()
        {
            _repositoryMock.Setup(r => r.GetProducts(false, 0, 10)).Returns(new List<Product>());

            var ex = Assert.Throws<KeyNotFoundException>(() => _service.GetProducts(false, 0, 10));
            Assert.Contains("inactive", ex.Message);
        }

        [Fact]
        public void GetProductById_ShouldReturn_WhenExists()
        {
            var product = new Product { Id = 1 };
            _repositoryMock.Setup(r => r.GetProductById(1)).Returns(product);

            var result = _service.GetProductById(1);

            Assert.Equal(product, result);
        }

        [Fact]
        public void GetProductById_ShouldThrow_WhenNotExists()
        {
            _repositoryMock.Setup(r => r.GetProductById(99)).Returns((Product?)null);

            var ex = Assert.Throws<KeyNotFoundException>(() => _service.GetProductById(99));
            Assert.Contains("id 99", ex.Message);
        }

        [Fact]
        public void UpdateProduct_ShouldUpdate_WhenValid()
        {
            var dto = new ProductDto
            {
                Description = "Updated",
                ManufactureDate = DateTime.UtcNow.AddDays(-1),
                ExpiryDate = DateTime.UtcNow.AddDays(5),
                Value = 15
            };

            var existing = new Product { Id = 1, Description = "Old" };

            _validatorMock.Setup(v => v.Validate(dto)).Returns(Result.Ok());
            _repositoryMock.Setup(r => r.GetProductById(1)).Returns(existing);
            _mapperMock.Setup(m => m.Map(dto, existing));

            existing.Description = dto.Description;
            existing.ManufactureDate = dto.ManufactureDate;
            existing.ExpiryDate = dto.ExpiryDate;
            existing.Value = dto.Value;

            _repositoryMock.Setup(r => r.UpdateProduct(existing)).Returns(existing);

            var result = _service.UpdateProduct(1, dto);

            Assert.Equal("Updated", result.Description);
            Assert.Equal(dto.Value, result.Value);
        }

        [Fact]
        public void UpdateProduct_ShouldThrow_WhenInvalid()
        {
            var dto = new ProductDto();
            _validatorMock.Setup(v => v.Validate(dto)).Returns(Result.Fail("Invalid"));

            var ex = Assert.Throws<ArgumentException>(() => _service.UpdateProduct(1, dto));
            Assert.Contains("Invalid", ex.Message);
        }

        [Fact]
        public void UpdateProduct_WhenNotFound_ShouldThrowKeyNotFoundException()
        {
            var dto = new ProductDto
            {
                ManufactureDate = DateTime.Today,
                ExpiryDate = DateTime.Today.AddDays(10)
            };

            _validatorMock.Setup(v => v.Validate(dto)).Returns(Result.Ok());
            _repositoryMock.Setup(r => r.GetProductById(1)).Returns((Product?)null);

            var ex = Assert.Throws<KeyNotFoundException>(() => _service.UpdateProduct(1, dto));
            Assert.Contains("id 1", ex.Message);
        }

        [Fact]
        public void DeleteProduct_ShouldReturnTrue_WhenDeleted()
        {
            var product = new Product { Id = 1 };
            _repositoryMock.Setup(r => r.GetProductById(1)).Returns(product);
            _repositoryMock.Setup(r => r.DeleteProduct(product)).Returns(true);

            var result = _service.DeleteProduct(1);

            Assert.True(result);
        }

        [Fact]
        public void DeleteProduct_ShouldThrow_WhenNotFound()
        {
            _repositoryMock.Setup(r => r.GetProductById(1)).Returns((Product?)null);

            var ex = Assert.Throws<KeyNotFoundException>(() => _service.DeleteProduct(1));
            Assert.Contains("id 1", ex.Message);
        }
    }
}