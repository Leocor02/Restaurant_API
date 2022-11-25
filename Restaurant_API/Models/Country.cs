using System;
using System.Collections.Generic;

namespace Restaurant_API.Models
{
    public partial class Country
    {
        public Country()
        {
            Dishes = new HashSet<Dish>();
            Users = new HashSet<User>();
        }

        public int Idcountry { get; set; }
        public int CountryCode { get; set; }
        public string CountryName { get; set; } = null!;

        public virtual ICollection<Dish> Dishes { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
