using System;
using System.Collections.Generic;

namespace Restaurant_API.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            Users = new HashSet<User>();
        }

        public int IduserRole { get; set; }
        public string Role { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
