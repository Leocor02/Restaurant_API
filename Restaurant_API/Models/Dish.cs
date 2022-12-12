using System;
using System.Collections.Generic;

namespace Restaurant_API.Models
{
    public partial class Dish
    {
        public int Iddish { get; set; }
        public string ItemPictureUrl { get; set; } = null!;
        public string DishDescription { get; set; } = null!;
        public int Idcountry { get; set; }

        public virtual Country? IdcountryNavigation { get; set; } = null!;
    }
}
