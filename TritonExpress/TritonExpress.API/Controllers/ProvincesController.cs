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
    public class ProvincesController : ControllerBase
    {
        private readonly IProvincesServices provincesServices;
        public ProvincesController(IProvincesServices provincesServices)
        {
            this.provincesServices = provincesServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProvincesAync()
        {
            return Ok(await provincesServices.GetAllProvinceAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProvinceID([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var province = await provincesServices.GetProvinceIDAsync(id);

            if (province == null)
            {
                return NotFound();
            }

            return Ok(province);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvinceAsync([FromRoute] int id, [FromBody] Province province)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //province.Id = id;
            await provincesServices.UpdateProvinceAsync(province);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostProvinceAsync([FromBody] Province province)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _id = await provincesServices.CreateProvinceAsync(province);
            return CreatedAtAction("GetProvince", new { id = _id }, province);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvinceAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventToDelete = await provincesServices.GetProvinceIDAsync(id);
            if (eventToDelete == null)
            {
                return NotFound();
            }

            await provincesServices.DeleteProvinceAsync(id);

            return Ok(eventToDelete);
        }

    }
}