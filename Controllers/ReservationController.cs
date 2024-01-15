using Microsoft.AspNetCore.Mvc;
using TravelAgencyAPI.DTO;
using TravelAgencyAPI.Services;

namespace TravelAgencyAPI.Controllers
{
    [ApiController]
    [Route("travel-agency/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationsService _reservationsService;
        private readonly HolidayService _holidayService;
        public ReservationController(ReservationsService reservationService, HolidayService holidayService)
        {
            _reservationsService = reservationService;
            _holidayService = holidayService;
        }

        [HttpPost]
        public IActionResult CreateReservation([FromBody] CreateReservationDTO reservation)
        {
            if (!reservation.PhoneNumber.All(char.IsDigit))
            {
                return BadRequest("That's an invalid phone number");
            }

            if (_holidayService.ReserveHolidaySlot(reservation.Holiday))
            {
                ResponseReservationDTO responseReservationDTO = _reservationsService.AddReservation(reservation);
                return Ok(responseReservationDTO);
            }

             return BadRequest("No available slots for the selected holiday.");
            
        }

        [HttpDelete("{reservationId}")]
        public IActionResult DeleteReservation(int reservationId)
        {
            bool success = _reservationsService.DeleteReservation(reservationId);

            if (success)
            {
                return Ok(success);
            }

            return BadRequest("Failed Request");
        }

        [HttpGet]
        public IActionResult GetReservations()
        {
            var reservations = _reservationsService.GetReservations();
            return Ok(reservations);
        }

        [HttpGet("{reservationId}")]
        public IActionResult GetReservation(int reservationId)
        {
            ResponseReservationDTO reservations = _reservationsService.GetReservation(reservationId);

            if (reservations != null)
            {
                return Ok(reservations);
            }
            return BadRequest("Failed Request");
        }

        [HttpPut]
        public IActionResult UpdateReservation([FromBody] UpdateReservationDTO updateReservationDTO)
        {
            ResponseReservationDTO updatedReservation = _reservationsService.UpdateReservation(updateReservationDTO);

            if (updatedReservation != null)
            {
                return Ok(updatedReservation);
            }

            return BadRequest("Failed Request");
        }
    }
}
