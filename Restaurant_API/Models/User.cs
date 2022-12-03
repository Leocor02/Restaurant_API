using System;
using System.Collections.Generic;

namespace Restaurant_API.Models
{
    public partial class User
    {
        public User()
        {
            Invoices = new HashSet<Invoice>();
            Reservations = new HashSet<Reservation>();
        }

        public int Iduser { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string BackUpEmail { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public bool Active { get; set; }
        public int IduserRole { get; set; }
        public int Idcountry { get; set; }

        public virtual Country? IdcountryNavigation { get; set; } = null!;
        public virtual UserRole? IduserRoleNavigation { get; set; } = null!;
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
