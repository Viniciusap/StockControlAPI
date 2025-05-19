using AutoMapper;
using StockControlAPI.Domain.Dtos;
using StockControlAPI.Domain.Model;
using StockControlAPI.Service.Interfaces;

namespace StockControlAPI.Service.Service
{
    public class ProductService(IProductRepository repository, IMapper mapper, IBaseValidator<ProductDto> validator) : IProductService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IProductRepository _repository = repository;
        private readonly IBaseValidator<ProductDto> _validator = validator;

        public Product AddProduct(ProductDto productDto)
        {
            var validationResult = _validator.Validate(productDto);
            if (validationResult.IsFailed)
                throw new ArgumentException(string.Join("; ", validationResult.Errors.Select(e => e.Message)));

            var product = _mapper.Map<Product>(productDto);
            return _repository.AddProduct(product);
        }

        public List<Product> GetProducts(bool active, int skip, int take)
        {
            var products = _repository.GetProducts(active, skip, take);
            if (products == null || !products.Any())
                throw new KeyNotFoundException($"No {(active ? "active" : "inactive")} products were found.");

            return products;
        }

        public Product GetProductById(int id)
        {
            var product = _repository.GetProductById(id);
            return product ?? throw new KeyNotFoundException($"Product with id {id} not found.");
        }

        public Product UpdateProduct(int id, ProductDto productDto)
        {
            var validationResult = _validator.Validate(productDto);
            if (validationResult.IsFailed)
                throw new ArgumentException(string.Join("; ", validationResult.Errors.Select(e => e.Message)));

            var existingProduct = _repository.GetProductById(id) ?? throw new KeyNotFoundException($"Product with id {id} not found.");
            _mapper.Map(productDto, existingProduct);
            return _repository.UpdateProduct(existingProduct);
        }

        public bool DeleteProduct(int id)
        {
            var product = _repository.GetProductById(id);
            return product == null ? throw new KeyNotFoundException($"Product with id {id} not found.") : _repository.DeleteProduct(product);
        }
    }
}