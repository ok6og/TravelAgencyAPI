using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.DTO
{
    public class UpdateHolidayDTO
    {
        public int Duration { get; set; }
        public int FreeSlots { get; set; }
        public decimal Price { get; set; }
        public Location Location { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
    }
}
