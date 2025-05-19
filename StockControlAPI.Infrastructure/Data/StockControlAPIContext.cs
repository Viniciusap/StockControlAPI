using Microsoft.EntityFrameworkCore;
using StockControlAPI.Domain.Model;

namespace StockControlAPI.Infrastruture.Data
{
    public class StockControlAPIContext(DbContextOptions<StockControlAPIContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}