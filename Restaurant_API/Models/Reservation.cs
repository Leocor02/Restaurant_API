using System;
using System.Collections.Generic;

namespace Restaurant_API.Models
{
    public partial class Reservation
    {
        public int Idreservation { get; set; }
        public DateTime Date { get; set; }
        public int DinersQuantity { get; set; }
        public int Iduser { get; set; }
        public int Idtable { get; set; }

        public virtual Table IdtableNavigation { get; set; } = null!;
        public virtual User IduserNavigation { get; set; } = null!;
    }
}
