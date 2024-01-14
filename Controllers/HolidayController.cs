using Microsoft.AspNetCore.Mvc;
using TravelAgencyAPI.DTO;
using TravelAgencyAPI.Models;
using TravelAgencyAPI.Services;

namespace TravelAgencyAPI.Controllers
{
    [ApiController]
    [Route("travel-agency/holidays")]
    public class HolidayController : ControllerBase
    {
        private readonly HolidayService _holidayService;
        private readonly LocationsService _locationsService;
        public HolidayController(HolidayService holidayService,LocationsService locationsService)
        {
            _holidayService = holidayService;
            _locationsService = locationsService;
        }

        [HttpPost]
        public IActionResult CreateHoliday([FromBody] CreateHolidayDTO holiday)
        {
            if (_locationsService.GetLocation(holiday.Location) == null)
            {
                return BadRequest("Invalid location ID");
            }

            ResponseHolidayDTO result = _holidayService.AddHoliday(holiday);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetHolidays(string? location, DateTime? startDate, int? duration)
        {
            var holidays = _holidayService.GetHolidays();            
            return Ok(holidays);
        }

        [HttpGet("{holidayId}")]
        public IActionResult GetHoliday(int holidayId)
        {
            ResponseHolidayDTO holidays = _holidayService.GetHoliday(holidayId);

            if (holidays != null)
            {
                return Ok(holidays);
            }
            return BadRequest("Failed Request");
        }

        [HttpDelete("{holidayId}")]
        public IActionResult DeleteHoliday(int holidayId)
        {
            bool success = _holidayService.DeleteHoliday(holidayId);

            if (success)
            {
                return Ok(success);
            }
            
            return BadRequest("Failed Request");
        }

        [HttpPut]
        public IActionResult UpdateHoliday([FromBody] UpdateHolidayDTO updateHolidayDTO)
        {
            ResponseHolidayDTO updatedHoliday = _holidayService.UpdateHoliday(updateHolidayDTO);

            if (updatedHoliday != null)
            {
                return Ok(updatedHoliday);
            }

            return BadRequest("Failed Request");
        }
    }
}
