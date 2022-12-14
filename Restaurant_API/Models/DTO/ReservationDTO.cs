namespace Restaurant_API.Models.DTO
{
    public class ReservationDTO
    {
        public int Idreservation { get; set; }
        public DateTime Date { get; set; }
        public int DinersQuantity { get; set; }
        public int Iduser { get; set; }
        public int Idtable { get; set; }
    }
}
