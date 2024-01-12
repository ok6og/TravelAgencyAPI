namespace TravelAgencyAPI.DTO
{
    public class ResponseReservationDTO
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactName { get; set; }
        public ResponseHolidayDTO Holiday { get; set; }
    }
}
