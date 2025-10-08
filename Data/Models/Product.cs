using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce_Case_Study.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Description { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        [Range(1, 100000, ErrorMessage = "Price must be between 1 and 100000.")]
        [Precision(8, 2)]
        public decimal Price { get; set; }

        // Relationships
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // Category relationship (Many Products → 1 Category)
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
