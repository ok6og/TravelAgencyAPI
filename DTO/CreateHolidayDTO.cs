using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.DTO
{
    public class CreateHolidayDTO
    {
        public int Duration { get; set; }
        public int FreeSlots { get; set; }
        public decimal Price { get; set; }
        public int Location { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
    }
}
