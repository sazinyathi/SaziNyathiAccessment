using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TritonExpress.Models;

namespace TritonExpress.Controllers
{
    public class VehicleTypesController : Controller
    {
        private readonly IConfiguration configuration;
        private IEnumerable<VehicleType>  vehicleTypes = null;

        public VehicleTypesController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // GET: VehicleTypes
        public async Task<IActionResult> Index(string searchString)
        {
            var uriString = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "VehicleTypes");

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                var readJob = response.Content.ReadAsAsync<IList<VehicleType>>();
                vehicleTypes = readJob.Result;
                if (!String.IsNullOrEmpty(searchString))
                {
                    vehicleTypes = readJob.Result.Where(
                        s => s.Name.ToLower().Contains(searchString.ToLower())
                       || s.Name.ToLower().Contains(searchString.ToLower())
                       || s.Descriptions.ToLower().Contains(searchString.ToLower())
                       );
                }

            }
            return View(vehicleTypes);
        }

        // GET: VehicleTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleType = new VehicleType();
            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "VehicleTypes/", id);
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                vehicleType = await response.Content.ReadAsAsync<VehicleType>();

            }
            if (vehicleType == null)
            {
                return NotFound();
            }

            return View(vehicleType);
        }

        // GET: VehicleTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Descriptions,IsDeleted")] VehicleType vehicleType)
        {
            if (ModelState.IsValid)
            {
                var uriString = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "VehicleTypes");
                string jsonString = JsonSerializer.Serialize(vehicleType);
                using (var client = new HttpClient())
                {
                    var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(uriString, httpContent);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        ViewBag.Error = "Error : " + response.StatusCode;
                        return View();
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(vehicleType);
        }

        // GET: VehicleTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "VehicleTypes/", id);
            var vehicleType = new VehicleType();
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                vehicleType = response.Content.ReadAsAsync<VehicleType>().Result;
            }
            if (vehicleType == null)
            {
                return NotFound();
            }
            return View(vehicleType);
        }

        // POST: VehicleTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Descriptions,IsDeleted")] VehicleType vehicleType)
        {
            if (id != vehicleType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "VehicleTypes/", id);
                string jsonString = JsonSerializer.Serialize(vehicleType);
                using (var client = new HttpClient())
                {
                    var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync(uriString, httpContent);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        ViewBag.Error = "Error : " + response.StatusCode;
                        return View();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleType);
        }

        // GET: VehicleTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "VehicleTypes/", id);
            var vehicleType = new VehicleType();
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.DeleteAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                vehicleType = response.Content.ReadAsAsync<VehicleType>().Result;
            }
            if (vehicleType == null)
            {
                return NotFound();
            }

            return View(vehicleType);
        }

        // POST: VehicleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "VehicleTypes/", id);

            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.DeleteAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        
    }
}
