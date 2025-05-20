using StockControlAPI.Domain.Model;

namespace StockControlAPI.Domain.Dtos
{
    public class StockDto
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}