
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlashpointStudioManager.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        [Required]
        public string EventType { get; set; } = string.Empty;

        [Required]
        public DateTime EventDate { get; set; }

        [Range(0, 10000000)]
        public decimal PackageAmount { get; set; }

        public string Status { get; set; } = "Pending";

        public string? Notes { get; set; }

        // Payments recorded for this booking
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
