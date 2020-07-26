using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TritonExpress.Interfaces.Services;
using TritonExpress.Models;
using TritonExpress.Services;

namespace TritonExpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypesController : ControllerBase
    {
        private readonly IVehicleTypeService vehicleTypesService;
     
        public VehicleTypesController(IVehicleTypeService vehicleTypesService)
        {
            this.vehicleTypesService = vehicleTypesService;
        }
        // GET: api/VehicleTypes
        [HttpGet]
        public async Task<IActionResult>  GetAllVehicleTypesAsync()
        {
            return Ok(await vehicleTypesService.GetAllVehicleTypeAsync());
        }

        // GET: api/VehicleTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleTypeById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var province = await vehicleTypesService.GetVehicleTypeIDAsync(id);

            if (province == null)
            {
                return NotFound();
            }

            return Ok(province);
        }

        // POST: api/VehicleTypes
        [HttpPost]
        public async Task<IActionResult> PostVechicleType([FromBody] VehicleType vehicleType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _id = await vehicleTypesService.CreateVehicleTypeAsync(vehicleType);
            return CreatedAtAction("GetvehicleType", new { id = _id }, vehicleType);
        }

        // PUT: api/VehicleTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVechicleType([FromRoute] int id, [FromBody] VehicleType vehicleType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            vehicleType.Id = id;
            await vehicleTypesService.UpdateVehicleTypeAsync(vehicleType);

            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleType(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventToDelete = await vehicleTypesService.GetVehicleTypeIDAsync(id);
            if (eventToDelete == null)
            {
                return NotFound();
            }

            await vehicleTypesService.DeleteVehicleTypeAsync(id);

            return Ok(eventToDelete);
        }
    }
}
