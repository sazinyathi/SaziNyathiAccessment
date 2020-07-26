using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TritonExpress.Interfaces.Services;
using TritonExpress.Models;

namespace TritonExpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelsController : ControllerBase
    {
        private readonly IParcelService  parcelsService;
        public ParcelsController(IParcelService  parcelsService)
        {
            this.parcelsService = parcelsService;
        }

  

        // POST: api/Parcels
        [HttpPost]
        public async Task<IActionResult> PostParcelAsync([FromBody] Parcel parcel )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _id = await parcelsService.CreateParcelAsync(parcel);
            return CreatedAtAction("GetParcel", new { id = _id }, parcel);
        }

    }
}
