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
    public class WayBillsController : ControllerBase
    {

        private readonly IWayBillsService wayBillsService;
        public WayBillsController(IWayBillsService wayBillsService)
        {
            this.wayBillsService = wayBillsService;
        }


        // GET: api/WayBills
        [HttpGet]
        public async Task<IActionResult> GetAllWayBillsAsync()
        {
            return Ok(await wayBillsService.GetAllWayBillsAsync());
        }

 
        // POST: api/WayBills
        [HttpPost]
        public async Task<IActionResult> PostWayBillsAsync([FromBody] WayBills wayBills)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _id = await wayBillsService.CreateWayBillsAsync(wayBills);
            return Ok();
        }

    
    }
}
