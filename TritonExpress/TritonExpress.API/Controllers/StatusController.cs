using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TritonExpress.Interfaces.Services;
using TritonExpress.Models;

namespace TritonExpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {

        private readonly IStatusService statusService;
        public StatusController(IStatusService statusService)
        {
            this.statusService = statusService;
        }
        // GET: api/Status
        [HttpGet]
        public async Task<IActionResult> GetAllStatusAsync()
        {
            return Ok(await statusService.GetAllStatusAsync());
        }

        // GET: api/Status/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatusByIdAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var status = await statusService.GetStatusIDAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return Ok(status);
        }

        // POST: api/Status
        [HttpPost]
        public async Task<IActionResult> PostStatusAsync([FromBody] Status status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _id = await statusService.CreateStatusAsync(status);
            return CreatedAtAction("GetStatus", new { id = _id }, status);
        }

        // PUT: api/Status/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusAsync([FromRoute] int id, [FromBody] Status status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            status.Id = id;
            await statusService.UpdateStatusAsync(status);

            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatusAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventToDelete = await statusService.GetStatusIDAsync(id);
            if (eventToDelete == null)
            {
                return NotFound();
            }

            await statusService.DeleteStatusAsync(id);

            return Ok(eventToDelete);
        }
    }
}
