using System.ComponentModel.DataAnnotations;

namespace StockControlAPI.Domain.Model
{
    public class Product
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime ManufactureDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public double Value { get; set; }
    }
}