using StockControlAPI.Domain.Dtos;
using StockControlAPI.Domain.Model;

namespace StockControlAPI.Service.Interfaces
{
    public interface IStockService
    {
        Stock AddStock(StockDto productDto);
        bool DeleteStock(int id);
    }
}