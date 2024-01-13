using TravelAgencyAPI.DTO;
using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Services
{
    public class ReservationsService
    {
        private static int lastReservationId = 0;
        private static readonly List<Reservation> _reservations = new List<Reservation>();

        public IEnumerable<ResponseReservationDTO> GetReservations() => _reservations.Select(MapToResponseReservationDTO);

        public ResponseReservationDTO GetReservation(int reservationId)
        {
            var reservation = _reservations.FirstOrDefault(x => x.Id == reservationId);
            if (reservation == null)
            {
                return null;
            }
            return MapToResponseReservationDTO(reservation);
        }

        public ResponseReservationDTO AddReservation(CreateReservationDTO reservationDTO)
        {
            Reservation reservation = new Reservation();

            reservation.ContactName = reservationDTO.ContactName;
            reservation.PhoneNumber = reservationDTO.PhoneNumber;
            reservation.Holiday = reservation.Holiday;
            reservation.Id = ++lastReservationId;

            _reservations.Add(reservation);

            return MapToResponseReservationDTO(reservation);
        }

        public bool DeleteReservation(int reservationId)
        {
            var reservationToRemove = _reservations.FirstOrDefault(x => x.Id == reservationId);
            if (reservationToRemove != null)
            {
                _reservations.Remove(reservationToRemove);
                return true;
            }
            return false;
        }

        public ResponseReservationDTO UpdateReservation(UpdateReservationDTO updateReservationDTO)
        {
            var existingReservation = _reservations.FirstOrDefault(x=> x.Id == updateReservationDTO.Id);

            if (existingReservation != null)
            {
                existingReservation.PhoneNumber = updateReservationDTO.PhoneNumber;
                existingReservation.Holiday = updateReservationDTO.Holiday;
                existingReservation.ContactName = updateReservationDTO.ContactName;

                return MapToResponseReservationDTO(existingReservation);
            }
            return null;
        }
        private ResponseReservationDTO MapToResponseReservationDTO(Reservation reservation)
        {
            return new ResponseReservationDTO
            {
                PhoneNumber = reservation.PhoneNumber,
                ContactName = reservation.ContactName,
                Id = reservation.Id,
                Holiday = reservation.Holiday
            };
        }
    }
}
