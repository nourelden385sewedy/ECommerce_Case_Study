using System.ComponentModel.DataAnnotations;

namespace ECommerce_Case_Study.Data.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Phone]
        public string Contact { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        // Relationships
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public CustomerProfile? CustomerProfile { get; set; }
    }
}
