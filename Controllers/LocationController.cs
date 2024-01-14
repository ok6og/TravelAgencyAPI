using Microsoft.AspNetCore.Mvc;
using TravelAgencyAPI.DTO;
using TravelAgencyAPI.Services;

namespace TravelAgencyAPI.Controllers
{
    [ApiController]
    [Route("travel-agency/locations")]
    public class LocationController : ControllerBase
    {
        private readonly LocationsService _locationService;

        public LocationController(LocationsService locationService)
        {
            _locationService = locationService;
        }

        [HttpPost]
        public IActionResult CreateLocation([FromBody] CreateLocationDTO locationDTO)
        {
            ResponseLocationDTO responseLocationDTO = _locationService.AddLocation(locationDTO);

            if (responseLocationDTO == null)
            {
                return BadRequest();
            }
            return Ok(responseLocationDTO);
        }

        [HttpDelete]
        public IActionResult DeleteLocation(int locationId)
        {
            bool success = _locationService.DeleteLocation(locationId);
            if (success)
            {
                return Ok(success);
            }
            return BadRequest("Failed Request");
        }

        [HttpGet]
        public IActionResult GetLocations()
        {
            var locations = _locationService.GetLocations();
            return Ok(locations);
        }

        [HttpGet("{locationId}")]
        public IActionResult GetLocation(int locationId)
        {
            ResponseLocationDTO locationDTO = _locationService.GetLocation(locationId);

            if (locationDTO != null)
            {
                return Ok(locationDTO);
            }
            return BadRequest("Not found location");
        }

        [HttpPut]
        public IActionResult UpdateLoaction([FromBody] UpdateLocationDTO updateLocationDTO)
        {
            ResponseLocationDTO updatedLocationDTO = _locationService.UpdateLocation(updateLocationDTO);

            if (updateLocationDTO != null)
            {
                return Ok(updateLocationDTO);
            }
            return BadRequest("Failed Request");
        }
    }
}
