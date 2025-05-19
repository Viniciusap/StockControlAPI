using Microsoft.EntityFrameworkCore;
using StockControlAPI.Domain.Model;
using StockControlAPI.Infrastruture.Data;
using StockControlAPI.Service.Interfaces;

namespace StockControlAPI.Service.Repositories
{
    public class ProductRepository(StockControlAPIContext context) : BaseRepository<Product>(context), IProductRepository
    {
        public List<Product> GetProducts(bool isActive, int skip, int take)
        {
            return [.. _context.Products
                .Where(p => p.IsActive == isActive)
                .Skip(skip)
                .Take(take)
                .AsNoTracking()];
        }

        public Product AddProduct(Product product)
        {
            base.Add(product);
            return product;
        }

        public Product GetProductById(int id)
        {
            return _context.Products
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);
        }

        public Product UpdateProduct(Product product)
        {
            return base.Update(product);
        }

        public bool DeleteProduct(Product product)
        {
            return base.Delete(product);
        }
    }
}
