namespace TravelAgencyAPI.Models
{
    public class Reservation
    {
        public string PhoneNumber { get; set; }
        public string ContactName { get; set; }
        public int Id { get; set; }
        public Holiday Holiday { get; set; }
    }
}
