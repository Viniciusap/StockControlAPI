using StockControlAPI.Domain.Model;

namespace StockControlAPI.Service.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        List<Product> GetProducts(bool isActive, int skip, int take);
        Product AddProduct(Product Product);
        Product GetProductById(int id);
        Product UpdateProduct(Product Product);
        bool DeleteProduct(Product Product);
    }
}