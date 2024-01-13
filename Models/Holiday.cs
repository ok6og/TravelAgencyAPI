namespace TravelAgencyAPI.Models
{
    public class Holiday
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
