using StockControlAPI.Domain.Model;
using StockControlAPI.Infrastruture.Data;
using StockControlAPI.Service.Interfaces;

namespace StockControlAPI.Service.Repositories
{
    public class StockRepository(StockControlAPIContext context) : BaseRepository<Stock>(context), IStockRepository
    {
        public Stock AddStock(Stock stock)
        {
            base.Add(stock);
            return stock;
        }

        public bool DeleteStock(Stock product)
        {
            return base.Delete(product);
        }
    }
}
