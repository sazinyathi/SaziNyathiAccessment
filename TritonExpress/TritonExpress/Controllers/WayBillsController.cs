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
            var _status = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Status");
            var _vehicles = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Vehicles");
            var wayBillsViewModel = new List<WayBillsViewModel>();

            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                var readJob = response.Content.ReadAsAsync<IList<WayBills>>();

                foreach (var item in readJob.Result)
                {
                    HttpResponseMessage responseStatus = await client.GetAsync(string.Format("{0}{1}", _status, "/" + item.StatusId));
                    var status = responseStatus.Content.ReadAsAsync<Status>().Result;
                    HttpResponseMessage responseVehicle = await client.GetAsync(string.Format("{0}{1}", _vehicles, "/" + item.VehicleId));
                    var vehicle = responseVehicle.Content.ReadAsAsync<Vehicle>().Result;
                    var war = new WayBillsViewModel
                    {
                        Quantity = item.Quantity,
                        Weight = item.Weight,
                        RegistrationNumber = vehicle.RegistrationNumber,
                        Status = status.Name,
                        CarName = vehicle.Name
                    };

                wayBillsViewModel.Add(war);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                    wayBillsViewModel = wayBillsViewModel.Where(s => s.Weight.ToLower().Contains(searchString.ToLower())
                       || s.RegistrationNumber.ToLower().Contains(searchString.ToLower())
                       || s.CarName.ToLower().Contains(searchString.ToLower())
                       || s.Status.ToLower().Contains(searchString.ToLower())
                       || s.Quantity.ToString().ToLower().Contains(searchString.ToLower())
                       ).ToList();
                }

        }
            return View(wayBillsViewModel);
    }


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
            Statuses = statuses,
            Vehicles = vehicles
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
                Weight = wayBillsFormViewModel.Weight
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
                     var uriStatus = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Status");
                     HttpResponseMessage response1 = await client.GetAsync(uriStatus);
                        
                     statuses = response1.Content.ReadAsAsync<IList<Status>>().Result;
                     var _vehicleTypes = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Vehicles");

                      HttpResponseMessage response3 = await client.GetAsync(_vehicleTypes); 
                       vehicles = response3.Content.ReadAsAsync<IList<Vehicle>>().Result;
                        var vayBillsFormViewModel = new WayBillsFormViewModel
                        {
                            Statuses = statuses,
                            Vehicles = vehicles
                        };
                        return View(vayBillsFormViewModel);
                    }
                       
                }
                return RedirectToAction(nameof(Index));
            }
        
        return View(wayBillsFormViewModel);
    }

    
}
}
