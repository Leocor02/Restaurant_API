using System;
using System.Collections.Generic;

namespace Restaurant_API.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Idcurrency { get; set; }
        public string CurrencyCode { get; set; } = null!;
        public string CurrencyName { get; set; } = null!;
        public bool Active { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
