﻿using System.ComponentModel.DataAnnotations;

namespace ECommerce_Case_Study.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        // Relationships
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
