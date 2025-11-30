using System;

namespace EVCS.BusinessOjects.Shared.Models.trinm.Models
{
    public partial class Payment
    {
        public long PaymentId { get; set; }

        public long TransactionId { get; set; }

        public string PaymentMethod { get; set; }

        public decimal Amount { get; set; }

        public DateTime? PaidDate { get; set; }

        public string PaymentStatus { get; set; }

        public virtual ChargingTransaction Transaction { get; set; }
    }
}

