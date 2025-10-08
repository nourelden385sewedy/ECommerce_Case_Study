using System.ComponentModel.DataAnnotations;

namespace ECommerce_Case_Study.Data.Models
{
    public class CustomerProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        // Relationship (1-to-1 with Customer)
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
