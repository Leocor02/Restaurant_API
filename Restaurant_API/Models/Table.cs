using System;
using System.Collections.Generic;

namespace Restaurant_API.Models
{
    public partial class Table
    {
        public Table()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Idtable { get; set; }
        public int Floor { get; set; }
        public int ChairQuantity { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
