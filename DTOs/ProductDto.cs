using System.ComponentModel.DataAnnotations;
using ECommerce_Case_Study.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_Case_Study.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
    }
    public class Cu
    {
        public string Category_Name { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
