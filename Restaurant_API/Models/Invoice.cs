using System;
using System.Collections.Generic;

namespace Restaurant_API.Models
{
    public partial class Invoice
    {
        public int Idinvoice { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public string? InvoiceNotes { get; set; }
        public int Iduser { get; set; }
        public int IdpaymentMethod { get; set; }
        public int Idcurrency { get; set; }

        public virtual Currency IdcurrencyNavigation { get; set; } = null!;
        public virtual PaymentMethod IdpaymentMethodNavigation { get; set; } = null!;
        public virtual User IduserNavigation { get; set; } = null!;
    }
}
