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
    public class StockServiceTests
    {
        private readonly Mock<IStockRepository> _repositoryMock = new();
        private readonly Mock<IProductRepository> _repositoryProductMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IBaseValidator<StockDto>> _validatorMock = new();
        private readonly StockService _service;

        public StockServiceTests()
        {
            _service = new StockService(_repositoryMock.Object, _repositoryProductMock.Object, _mapperMock.Object, _validatorMock.Object);
        }

        [Fact]
        public void AddStock_WithValidData_ShouldAddNewStock()
        {
            var dto = new StockDto { ProductId = 1, Quantity = 10 };
            var product = new Product { Id = 1 };
            var stock = new Stock { Id = 1, Product = product, Quantity = 10 };

            _validatorMock.Setup(v => v.Validate(dto)).Returns(Result.Ok());
            _mapperMock.Setup(m => m.Map<Stock>(dto)).Returns(stock);
            _repositoryProductMock.Setup(r => r.GetById(dto.ProductId)).Returns(product);
            _repositoryMock.Setup(r => r.AddStock(stock)).Returns(stock);

            var result = _service.AddStock(dto);

            result.Should().Be(stock);
            _repositoryMock.Verify(r => r.AddStock(stock), Times.Once);
        }

        [Fact]
        public void AddStock_WhenStockExists_ShouldUpdateQuantity()
        {
            var dto = new StockDto { ProductId = 1, Quantity = 10 };
            var product = new Product { Id = 1 };
            var existingStock = new Stock { Id = 1, Product = product, Quantity = 5 };

            var mappedStock = new Stock { Product = product, Quantity = dto.Quantity };

            _validatorMock.Setup(v => v.Validate(dto)).Returns(Result.Ok());
            _mapperMock.Setup(m => m.Map<Stock>(dto)).Returns(mappedStock);
            _repositoryProductMock.Setup(r => r.GetById(dto.ProductId)).Returns(product);
            _repositoryMock.Setup(r => r.GetById(dto.ProductId)).Returns(existingStock);
            _repositoryMock.Setup(r => r.Update(existingStock)).Returns(existingStock);

            var result = _service.AddStock(dto);

            result.Quantity.Should().Be(15);
            _repositoryMock.Verify(r => r.Update(existingStock), Times.Once);
        }

        [Fact]
        public void AddStock_WithInvalidData_ShouldThrowArgumentException()
        {
            var dto = new StockDto { ProductId = 1, Quantity = -5 };

            _validatorMock.Setup(v => v.Validate(dto)).Returns(Result.Fail("Invalid quantity"));

            var ex = Assert.Throws<ArgumentException>(() => _service.AddStock(dto));
            Assert.Contains("Invalid quantity", ex.Message);
        }

        [Fact]
        public void AddStock_WithNonExistentProduct_ShouldThrowKeyNotFoundException()
        {
            var dto = new StockDto { ProductId = 99, Quantity = 10 };

            _validatorMock.Setup(v => v.Validate(dto)).Returns(Result.Ok());
            _repositoryProductMock.Setup(r => r.GetById(dto.ProductId)).Returns((Product?)null);

            var ex = Assert.Throws<KeyNotFoundException>(() => _service.AddStock(dto));
            Assert.Contains("Product with ID 99 not found", ex.Message);
        }

        [Fact]
        public void DeleteStock_ShouldReturnTrue_WhenDeleted()
        {
            var stock = new Stock { Id = 1, Product = new Product { Id = 1 }, Quantity = 10 };
            _repositoryMock.Setup(r => r.GetById(1)).Returns(stock);
            _repositoryMock.Setup(r => r.Delete(stock)).Returns(true);

            var result = _service.DeleteStock(1);

            Assert.True(result);
        }

        [Fact]
        public void DeleteStock_ShouldThrow_WhenNotFound()
        {
            _repositoryMock.Setup(r => r.GetById(1)).Returns((Stock?)null);

            var ex = Assert.Throws<KeyNotFoundException>(() => _service.DeleteStock(1));
            Assert.Contains("Stock with ID 1 not found", ex.Message);
        }
    }
}
