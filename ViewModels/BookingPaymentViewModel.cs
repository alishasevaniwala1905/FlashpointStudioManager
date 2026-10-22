
using System;

namespace FlashpointStudioManager.ViewModels
{
    public class BookingPaymentViewModel
    {
        public int BookingId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string EventType { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal AmountPaid { get; set; }

        public decimal RemainingBalance
        {
            get { return Math.Max(0, TotalAmount - AmountPaid); }
        }

        public string PaymentStatus
        {
            get
            {
                if (TotalAmount <= 0 || AmountPaid <= 0)
                    return "Pending";

                if (AmountPaid >= TotalAmount)
                    return "Paid";

                return "Partially Paid";
            }
        }
    }
}
