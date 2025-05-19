using FluentResults;
using StockControlAPI.Domain.Dtos;
using StockControlAPI.Service.Interfaces;

namespace StockControlAPI.Service.Service
{
    public class ProductValidator : IBaseValidator<ProductDto>
    {
        public Result Validate(ProductDto product)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(product.Description))
                errors.Add("Description is required and cannot be empty.");

            if (product.Description?.Length > 200)
                errors.Add("Description must not exceed 200 characters.");

            if (product.ManufactureDate >= product.ExpiryDate)
                errors.Add("Manufacture date cannot be equal to or later than expiry date.");

            if (product.ManufactureDate > DateTime.UtcNow)
                errors.Add("Manufacture date cannot be in the future.");

            if (product.ExpiryDate <= DateTime.UtcNow)
                errors.Add("Expiry date must be in the future.");

            if (product.Value <= 0)
                errors.Add("Product value must be greater than zero.");

            return errors.Count != 0 ? Result.Fail(string.Join(" ", errors)) : Result.Ok();
        }
    }
}