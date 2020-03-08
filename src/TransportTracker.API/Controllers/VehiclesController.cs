using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransportTracker.API.Infrastructure.Services.Interfaces;
using TransportTracker.API.Models.Dtos;

namespace TransportTracker.API.Controllers
{
    [Route("api/v{version:apiVersion}/vehicles")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehiclesService vehiclesService;

        public VehiclesController(IVehiclesService vehiclesService)
        {
            this.vehiclesService = vehiclesService;
        }

        /// <summary>
        /// Get list of vehicles.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<VehicleResponse>))]
        public async Task<IActionResult> GetVehicles()
        {
            var responses = await vehiclesService.GetVehicleListAsync();

            return Ok(responses);
        }

        /// <summary>
        /// Get individual vehicle
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        [HttpGet("{vehicleId}", Name = "GetVehicleById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VehicleResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetVehicleById(int vehicleId)
        {
            var response = await vehiclesService.GetVehicleAsync(vehicleId);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// Register new vehicle
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VehicleResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleRequest request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            var vehicleExists = await vehiclesService.VehicleExistsAsync(request.VehicleId);

            if (vehicleExists)
            {
                ModelState.AddModelError("", "Vehicle id exists");
                return StatusCode(404, ModelState);
            }

            var response = await vehiclesService.AddVehicleAsync(request);

            return Ok(response);
        }
    }
}