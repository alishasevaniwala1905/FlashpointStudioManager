
using System;
using System.ComponentModel.DataAnnotations;

namespace FlashpointStudioManager.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public int BookingId { get; set; }

        public Booking? Booking { get; set; }

        [Required]
        [Range(0.01, 10000000)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.Today;

        [Required]
        public string PaymentMethod { get; set; } = "Cash";

        public string? Notes { get; set; }
    }
}
