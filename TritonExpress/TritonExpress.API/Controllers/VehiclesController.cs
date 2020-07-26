using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TritonExpress.Interfaces.Services;
using TritonExpress.Models;

namespace TritonExpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService vehicleService;
        public VehiclesController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }
        // GET: api/Vehicles
        [HttpGet]
        public async Task<IActionResult> GetAllVehicleAsync()
        {
            return Ok(await vehicleService.GetAllVehicleAsync());
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleByIdAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var province = await vehicleService.GetVehicleIDAsync(id);

            if (province == null)
            {
                return NotFound();
            }

            return Ok(province);
        }

        // POST: api/Vehicles
        [HttpPost]
        public async Task<IActionResult> PostVechicleAsync([FromBody] Vehicle vehicle)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _id = await vehicleService.CreateVehicleAsync(vehicle);
            return CreatedAtAction("GetVehicle", new { id = _id }, vehicle);
        }

        // PUT: api/Vehicles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleAsync(int id, [FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            vehicle.Id = id;
            await vehicleService.UpdateVehicleAsync(vehicle);

            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVechicleAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventToDelete = await vehicleService.GetVehicleIDAsync(id);
            if (eventToDelete == null)
            {
                return NotFound();
            }

            await vehicleService.DeleteVehicleAsync(id);

            return Ok(eventToDelete);
        }
    }
}
