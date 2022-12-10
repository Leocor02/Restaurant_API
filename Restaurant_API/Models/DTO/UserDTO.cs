namespace Restaurant_API.Models.DTO
{
    public class UserDTO
    {
        public int Iduser { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string BackUpEmail { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public bool Active { get; set; }
        public int IduserRole { get; set; }
        public int Idcountry { get; set; }

    }
}
