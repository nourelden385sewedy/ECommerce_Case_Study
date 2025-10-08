using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce_Case_Study.Data.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Precision(8, 2)]
        public decimal PriceAtOrder { get; set; }
        public int Quantity { get; set; } = 1;


        // Relationship (1-to-1 with Order)
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; } = null!;

        // Relationship (1-to-1 with Product)
        public int? ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
