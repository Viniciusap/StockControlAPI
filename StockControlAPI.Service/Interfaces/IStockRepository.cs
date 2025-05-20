using StockControlAPI.Domain.Model;

namespace StockControlAPI.Service.Interfaces
{
    public interface IStockRepository : IBaseRepository<Stock>
    {
        Stock AddStock(Stock Product);
    }
}