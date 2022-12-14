namespace Restaurant_API.Models.DTO
{
    public class DishDTO
    {
        public int Iddish { get; set; }
        public string ItemPictureUrl { get; set; } = null!;
        public string DishDescription { get; set; } = null!;
        public int Idcountry { get; set; }

    }
}
