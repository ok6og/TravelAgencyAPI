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
        public ReservationController(ReservationsService reservationService)
        {
            _reservationsService = reservationService;
        }

        [HttpPost]
        public IActionResult CreateReservation([FromBody] CreateReservationDTO reservation)
        {
            ResponseReservationDTO responseReservationDTO = _reservationsService.AddReservation(reservation);

            if (responseReservationDTO == null)
            {
                return BadRequest();
            }
            return Ok(responseReservationDTO);
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
