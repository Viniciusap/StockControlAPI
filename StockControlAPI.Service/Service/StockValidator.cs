using FluentResults;
using StockControlAPI.Domain.Dtos;
using StockControlAPI.Service.Interfaces;

namespace StockControlAPI.Service.Service
{
    public class StockValidator(IProductRepository productRepository) : IBaseValidator<StockDto>
    {
        private readonly IProductRepository _productRepository = productRepository;

        public Result Validate(StockDto stock)
        {
            var errors = new List<string>();

            if (stock.Quantity <= 0)
                errors.Add("Stock added to the quantity must be greater than zero.");

            return errors.Count != 0 ? Result.Fail(string.Join(" ", errors)) : Result.Ok();
        }
    }
}