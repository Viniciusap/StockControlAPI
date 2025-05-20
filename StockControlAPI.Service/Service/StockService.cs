using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StockControlAPI.Domain.Dtos;
using StockControlAPI.Domain.Model;
using StockControlAPI.Service.Interfaces;

namespace StockControlAPI.Service.Service
{
    public class StockService(IStockRepository repository, IProductRepository productRepository, IMapper mapper, IBaseValidator<StockDto> validator) : IStockService
    {
        private readonly IStockRepository _repository = repository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IBaseValidator<StockDto> _validator = validator;

        public Stock AddStock(StockDto stockDto)
        {
            var validationResult = _validator.Validate(stockDto);
            if (validationResult.IsFailed)
                throw new ArgumentException(string.Join("; ", validationResult.Errors.Select(e => e.Message)));

            var stock = _mapper.Map<Stock>(stockDto);

            var product = _productRepository.GetById(stockDto.ProductId);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {stockDto.ProductId} not found.");

            stock.Product = product;

            var existingStock = _repository.GetById(stockDto.ProductId);
            if (existingStock != null)
            {
                existingStock.Quantity += stock.Quantity;
                _repository.Update(existingStock);
                return existingStock;
            }

            return _repository.AddStock(stock);
        }

        public bool DeleteStock(int id)
        {
            var stock = _repository.GetById(id);
            if (stock == null)
                throw new KeyNotFoundException($"Stock with ID {id} not found.");

            return _repository.Delete(stock);
        }
    }
}
