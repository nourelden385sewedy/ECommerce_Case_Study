using ECommerce_Case_Study.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ECommerce_Case_Study.DTOs
{
    public class CreateOrderDto
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        [Precision(8, 2)]
        public decimal TotalPrice { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateOfOrder { get; set; } = DateTime.UtcNow;
        public int CustomerId { get; set; }
        public List<int> Products { get; set; }
    }
}
