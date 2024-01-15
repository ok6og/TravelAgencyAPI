using TravelAgencyAPI.DTO;
using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Services
{
    public class HolidayService
    {
        private static int lastHolidayId = 0;
        private static readonly List<Holiday> _holidays = new List<Holiday>();
        private readonly LocationsService _locationService;

        public HolidayService(LocationsService locationService)
        {
            _locationService = locationService;
        }

        public IEnumerable<ResponseHolidayDTO> GetHolidays(string? location, DateTime? startDate, int? duration)
        {
            //Getting all the holidays

            var holidays = _holidays.Select(MapToResponseHolidayDTO);

            //Filtering them out

            if (!string.IsNullOrEmpty(location))
            {
                holidays = holidays.Where(x => (x.Location.City.Contains(location, StringComparison.OrdinalIgnoreCase) ||
                                                x.Location.Country.Contains(location, StringComparison.OrdinalIgnoreCase)));
            }

            if (startDate.HasValue)
            {
                holidays = holidays.Where(h => DateTime.Compare(h.StartDate.Date, startDate.Value.Date) == 0);
                Console.WriteLine(holidays);
            }

            if (duration.HasValue)
            {
                holidays = holidays.Where(h => h.Duration == duration);
            }

            return holidays;
        }

        public ResponseHolidayDTO GetHoliday(long holidayId) => MapToResponseHolidayDTO(_holidays.FirstOrDefault(x => x.Id == holidayId));      
        
        public ResponseHolidayDTO AddHoliday(CreateHolidayDTO holidayDTO)
        { 
            Holiday holiday = new Holiday();

            holiday.Duration = holidayDTO.Duration;
            holiday.FreeSlots = holidayDTO.FreeSlots;
            holiday.Price = holidayDTO.Price;
            holiday.Location = _locationService.MapToLocation(_locationService.GetLocation(holidayDTO.Location));
            holiday.Title = holidayDTO.Title;
            holiday.StartDate = holidayDTO.StartDate;
            holiday.Id = ++lastHolidayId;

            _holidays.Add(holiday);

            return MapToResponseHolidayDTO(holiday);
        }

        public bool DeleteHoliday(int holidayId)
        {
            var holidayToRemove = _holidays.FirstOrDefault(h => h.Id == holidayId);

            if (holidayToRemove != null)
            {
                _holidays.Remove(holidayToRemove);
                return true;
            }

            return false;
        }

        public ResponseHolidayDTO UpdateHoliday(UpdateHolidayDTO updateHolidayDTO)
        {
            var existingHoliday = _holidays.FirstOrDefault(h => h.Id == updateHolidayDTO.Id);

            if (existingHoliday != null)
            {
                existingHoliday.Duration = updateHolidayDTO.Duration;
                existingHoliday.FreeSlots = updateHolidayDTO.FreeSlots;
                existingHoliday.Price = updateHolidayDTO.Price;
                existingHoliday.Location = _locationService.MapToLocation(_locationService.GetLocation(updateHolidayDTO.Location));
                existingHoliday.Title = updateHolidayDTO.Title;
                existingHoliday.StartDate = updateHolidayDTO.StartDate;

                return MapToResponseHolidayDTO(existingHoliday);
            }

            return null;
        }

        public bool ReserveHolidaySlot(long holidayId)
        {
            var holiday = _holidays.FirstOrDefault(x => x.Id == holidayId);

            if (holiday != null && holiday.FreeSlots > 0)
            {
                holiday.FreeSlots--;
                return true;
            }

            return false;
        }

        public ResponseHolidayDTO MapToResponseHolidayDTO(Holiday holiday)
        {
            if (holiday == null)
            {
                return null;
            }

            return new ResponseHolidayDTO
            {
                Duration = holiday.Duration,
                FreeSlots = holiday.FreeSlots,
                Price = holiday.Price,
                Location = _locationService.MapToResponseLocationDTO(holiday.Location),
                Title = holiday.Title,
                StartDate = holiday.StartDate,
                Id = holiday.Id
            };
        }

        public Holiday MapToHoliday(ResponseHolidayDTO holiday)
        {
            if (holiday == null)
            {
                return null;
            }

            return new Holiday
            {
                Duration = holiday.Duration,
                FreeSlots = holiday.FreeSlots,
                Price = holiday.Price,
                Location = _locationService.MapToLocation(holiday.Location),
                Title = holiday.Title,
                StartDate = holiday.StartDate,
                Id = holiday.Id
            };
        }
    }
}
