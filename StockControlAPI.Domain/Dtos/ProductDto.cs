namespace StockControlAPI.Domain.Dtos
{
    public class ProductDto
    {
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime ManufactureDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public double Value { get; set; }
    }
}