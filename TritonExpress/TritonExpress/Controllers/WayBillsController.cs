using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TritonExpress.Models;
using TritonExpress.ViewModels;

namespace TritonExpress.Controllers
{
    public class WayBillsController : Controller
    {
        private readonly IConfiguration configuration;
        private IEnumerable<Status> statuses = null;
        private IEnumerable<Vehicle> vehicles = null;
        private IEnumerable<WayBills> wayBills = null;
        public WayBillsController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // GET: WayBills
        public async Task<IActionResult> Index(string searchString)
        {

            var uriString = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "WayBills");

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                var readJob = response.Content.ReadAsAsync<IList<WayBills>>();
                wayBills = readJob.Result;
                if (!String.IsNullOrEmpty(searchString))
                {
                    wayBills = readJob.Result.Where(
                        s => s.Weight.ToLower().Contains(searchString.ToLower())
                       //|| s.Quantity.ToLower().Contains(searchString.ToLower())
                       //|| s..ToLower().Contains(searchString.ToLower())
                       );
                }

            }
            return View(wayBills);
        }

        // GET: WayBills/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var wayBills = await _context.WayBills
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (wayBills == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(wayBills);
        //}

        // GET: WayBills/Create
        public async Task<IActionResult> Create()
        {
            var uriString = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Status");

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                statuses = response.Content.ReadAsAsync<IList<Status>>().Result;
            }

            var _vehicleTypes = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Vehicles");

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_vehicleTypes);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                vehicles = response.Content.ReadAsAsync<IList<Vehicle>>().Result;
            }
            var vayBillsFormViewModel = new WayBillsFormViewModel
            {
               Statuses  = statuses,
               Vehicles  = vehicles
            };
            return View(vayBillsFormViewModel);
        }

        // POST: WayBills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Weight,Quantity,Status,Vehicle")] WayBillsFormViewModel wayBillsFormViewModel)
        {
            if (ModelState.IsValid)
            {
                var wayBills = new WayBills
                {
                  Quantity = wayBillsFormViewModel.Quantity,
                  StatusId = wayBillsFormViewModel.Status,
                  VehicleId = wayBillsFormViewModel.Vehicle,
                  Weight  = wayBillsFormViewModel.Weight
                };
                var uriString = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "WayBills");
                string jsonString = JsonSerializer.Serialize(wayBills);
                using (var client = new HttpClient())
                {
                    var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(uriString, httpContent);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        ViewBag.Error = "Error : " + response.StatusCode;
                        return View(wayBillsFormViewModel);
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(wayBillsFormViewModel);
        }

        //// GET: WayBills/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var wayBills = await _context.WayBills.FindAsync(id);
        //    if (wayBills == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(wayBills);
        //}

        //// POST: WayBills/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Weight,Quantity,StatusId,VehicleId")] WayBills wayBills)
        //{
        //    if (id != wayBills.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(wayBills);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!WayBillsExists(wayBills.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(wayBills);
        //}

        //// GET: WayBills/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var wayBills = await _context.WayBills
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (wayBills == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(wayBills);
        //}

        //// POST: WayBills/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var wayBills = await _context.WayBills.FindAsync(id);
        //    _context.WayBills.Remove(wayBills);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool WayBillsExists(int id)
        //{
        //    return _context.WayBills.Any(e => e.Id == id);
        //}
    }
}
