namespace TravelAgencyAPI.DTO
{
    public class ResponseLocationDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string? ImageUrl { get; set; }
    }
}
