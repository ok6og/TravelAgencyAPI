using TravelAgencyAPI.DTO;
using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Services
{
    public class HolidayService
    {
        private static int lastHolidayId = 0;
        private static readonly List<Holiday> _holidays = new List<Holiday>();

        public IEnumerable<ResponseHolidayDTO> GetHolidays() => _holidays.Select(MapToResponseHolidayDTO);

        public ResponseHolidayDTO GetHoliday(int holidayId)
        {
            var holiday = _holidays.FirstOrDefault(x => x.Id == holidayId);
            if (holiday == null)
            {
                return null;
            }
            return MapToResponseHolidayDTO(holiday);
        }
        public ResponseHolidayDTO AddHoliday(CreateHolidayDTO holidayDTO)
        { 
            Holiday holiday = new Holiday();

            holiday.Duration = holidayDTO.Duration;
            holiday.FreeSlots = holidayDTO.FreeSlots;
            holiday.Price = holidayDTO.Price;
            holiday.Location = holidayDTO.Location;
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
                existingHoliday.Location = updateHolidayDTO.Location;
                existingHoliday.Title = updateHolidayDTO.Title;
                existingHoliday.StartDate = updateHolidayDTO.StartDate;

                return MapToResponseHolidayDTO(existingHoliday);
            }

            return null;
        }

        private ResponseHolidayDTO MapToResponseHolidayDTO(Holiday holiday)
        {
            return new ResponseHolidayDTO
            {
                Duration = holiday.Duration,
                FreeSlots = holiday.FreeSlots,
                Price = holiday.Price,
                Location = holiday.Location,
                Title = holiday.Title,
                StartDate = holiday.StartDate,
                Id = holiday.Id
            };
        }
    }
}
