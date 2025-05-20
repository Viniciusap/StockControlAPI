using StockControlAPI.Domain.Model;

namespace StockControlAPI.Domain.Dtos
{
    public class StockDto
    {
        public StockDto() { }

        public StockDto(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}