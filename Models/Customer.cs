using System.ComponentModel.DataAnnotations;

namespace FlashpointStudioManager.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? EventType { get; set; }
    }
}
