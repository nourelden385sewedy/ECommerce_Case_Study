using System.ComponentModel.DataAnnotations;
using ECommerce_Case_Study.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_Case_Study.DTOs
{
    public class CreateProductDto
    {

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Description { get; set; }

        [Required]
        public int StockQuantity { get; set; } = 1;

        [Range(1, 100000, ErrorMessage = "Price must be between 1 and 100000.")]
        [Precision(8, 2)]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
