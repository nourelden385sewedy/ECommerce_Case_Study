using System.ComponentModel.DataAnnotations;

namespace ECommerce_Case_Study.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public CustomerProfileDto? ProfileDto { get; set; }
    }
}
