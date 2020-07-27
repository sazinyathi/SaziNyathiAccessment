using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TritonExpress.Interfaces.Services;
using TritonExpress.Models;

namespace TritonExpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchesService branchesService;
        public BranchesController(IBranchesService branchesService)
        {
            this.branchesService = branchesService;
        }
        // GET: api/Branches
        [HttpGet]
        public async Task<IActionResult> GetAllBranchesAsync()
        {
            return Ok(await branchesService.GetAllBranchesAsync());
        }

        // GET: api/Branches/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranchesAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var province = await branchesService.GetBranchesIDAsync(id);

            if (province == null)
            {
                return NotFound();
            }

            return Ok(province);
        }

        // POST: api/Branches
        [HttpPost]
        public async Task<IActionResult> PostBranchAsync([FromBody] Branches branches)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _id = await branchesService.CreateBranchesAsync(branches);
            return Ok();
        }

        // PUT: api/Branches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBranchAsync([FromRoute] int id, [FromBody] Branches branches)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            branches.Id = id;
            await branchesService.UpdateBranchesAsync(branches);

            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranchAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventToDelete = await branchesService.GetBranchesIDAsync(id);
            if (eventToDelete == null)
            {
                return NotFound();
            }

            await branchesService.DeleteBranchesAsync(id);

            return Ok(eventToDelete);
        }
    }
}
