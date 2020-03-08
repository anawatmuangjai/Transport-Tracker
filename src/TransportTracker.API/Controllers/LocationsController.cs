using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransportTracker.API.Infrastructure.Services.Interfaces;
using TransportTracker.API.Models.Dtos;

namespace TransportTracker.API.Controllers
{
    [Route("api/v{version:apiVersion}/locations")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsService locationsService;

        public LocationsController(ILocationsService locationsService)
        {
            this.locationsService = locationsService;
        }

        /// <summary>
        /// Get individual location by id
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        [HttpGet("{locationId}", Name = "GetLocationById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LocationResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetLocationById(Guid locationId)
        {
            var response = await locationsService.GetLocationByIdAsync(locationId);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// Get current location by vehicle id
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        [HttpGet("current/{vehicleId}", Name = "GetCurrentLocationByVehicleId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LocationResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCurrentLocationByVehicleId(int vehicleId)
        {
            var response = await locationsService.GetCurrentLocationByVehicleIdAsync(vehicleId);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// get location by vehicle id and period time
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("period")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<LocationResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLocationByVehicleIdAndPeriodTime([FromBody] LocationFilterRequest request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            var responses = await locationsService.GetLocationByVehicleIdAndPeriodTimeAsync(request);

            if (responses == null)
            {
                return NotFound();
            }

            return Ok(responses);
        }

        /// <summary>
        /// Register new location
        /// </summary>
        /// <param name="locationCreateDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LocationResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddLocation([FromBody] LocationRequest locationCreateDto)
        {
            if (locationCreateDto == null)
            {
                return BadRequest(ModelState);
            }

            var response = await locationsService.AddLocationAsync(locationCreateDto);

            if (response == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(response);
        }
    }
}