using StockControlAPI.Domain.Dtos;
using StockControlAPI.Domain.Model;

namespace StockControlAPI.Service.Interfaces
{
    public interface IProductService
    {
        Product AddProduct(ProductDto productDto);
        Product UpdateProduct(int id, ProductDto productDto);
        bool DeleteProduct(int id);
        Product GetProductById(int id);
        List<Product> GetProducts(bool active, int skip, int take);
    }
}