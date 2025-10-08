using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ECommerce_Case_Study.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        [Precision(8, 2)]
        public decimal TotalPrice { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateOfOrder { get; set; }

        // Relationships
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
