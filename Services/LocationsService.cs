using TravelAgencyAPI.DTO;
using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Services
{
    public class LocationsService
    {
        private static int lastLocationId = 0;
        private static readonly List<Location> _locations = new List<Location>();

        public IEnumerable<ResponseLocationDTO> GetLocations() => _locations.Select(MapToResponseLocationDTO);

        public ResponseLocationDTO GetLocation(int locationId)
        {
            var location = _locations.FirstOrDefault(x => x.Id == locationId);
            if (location == null)
            {
                return null;
            }
            return MapToResponseLocationDTO(location);
        }

        public ResponseLocationDTO AddLocation(CreateLocationDTO locationDTO)
        {
            Location location = new Location();

            location.Id = ++lastLocationId;
            location.Number = locationDTO.Number;
            location.City = locationDTO.City;
            location.Country = locationDTO.Country;
            location.Street = locationDTO.Street;
            location.ImageUrl = locationDTO.ImageUrl;

            _locations.Add(location);

            return MapToResponseLocationDTO(location);
        }

        public bool DeleteLocation(int locationId)
        {
            var location = _locations.FirstOrDefault(x => x.Id == locationId);

            if (location != null)
            {
                _locations.Remove(location);
                return true;
            }
            return false;
        }

        public ResponseLocationDTO UpdateLocation(UpdateLocationDTO updateHolidayDTO)
        {
            var existingLocation = _locations.FirstOrDefault(x => x.Id == updateHolidayDTO.Id);
            if (existingLocation != null)
            {
                existingLocation.Number = updateHolidayDTO.Number;
                existingLocation.City = updateHolidayDTO.City;
                existingLocation.Country = updateHolidayDTO.Country;
                existingLocation.Street = updateHolidayDTO.Street;
                existingLocation.ImageUrl = updateHolidayDTO.ImageUrl;

                return MapToResponseLocationDTO(existingLocation);              
            }

            return null;
        }

        private ResponseLocationDTO MapToResponseLocationDTO(Location location)
        {
            return new ResponseLocationDTO
            {
                Id = location.Id,
                Number = location.Number,
                Street = location.Street,
                City = location.City,
                Country = location.Country,
                ImageUrl = location.ImageUrl
            };
        }
    }
}
