using System.ComponentModel.DataAnnotations;

namespace StockControlAPI.Domain.Model
{
    public class Stock
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}