namespace TravelAgencyAPI.DTO
{
    public class CreateReservationDTO
    {
        public string PhoneNumber { get; set; }
        public string ContactName { get; set; }
        public int Id { get; set; }
        public ResponseHolidayDTO Holiday { get; set; }
    }
}
