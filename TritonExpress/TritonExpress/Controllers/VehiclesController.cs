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
    public class VehiclesController : Controller
    {
        private readonly IConfiguration configuration;
        private IEnumerable<Vehicle> vehicles = null;
        private IEnumerable<Branches> branches = null;
        private IEnumerable<VehicleType> vehicleTypes = null;
        public VehiclesController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
       

        // GET: Vehicles
        public async Task<IActionResult> Index(string searchString)
        {
            var uriString = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Vehicles");

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                var readJob = response.Content.ReadAsAsync<IList<Vehicle>>();
                vehicles = readJob.Result;
                if (!String.IsNullOrEmpty(searchString))
                {
                    vehicles = readJob.Result.Where(
                        s => s.Name.ToLower().Contains(searchString.ToLower())
                       || s.Make.ToLower().Contains(searchString.ToLower())
                       || s.Model.ToLower().Contains(searchString.ToLower())
                       );
                }

            }
            return View(vehicles);
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = new Vehicle();
            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Vehicles/", id);
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                vehicle = await response.Content.ReadAsAsync<Vehicle>();

            }
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public async Task<IActionResult> Create()
        {
            var uriString = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Branches");

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                branches = response.Content.ReadAsAsync<IList<Branches>>().Result;
            }

            var _vehicleTypes = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "VehicleTypes");

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_vehicleTypes);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                vehicleTypes = response.Content.ReadAsAsync<IList<VehicleType>>().Result;
            }
            var vehicleFormViewModel = new VehicleFormViewModel
            {
                
                Branches  = branches,
                VehicleTypes  = vehicleTypes

            };


            return View(vehicleFormViewModel);
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Model,Make,RegistrationNumber,IsDeleted,Branch,VehicleType")] VehicleFormViewModel vehicleFormViewModel)
        {
            if (ModelState.IsValid)
            {
                var _vechicle = new Vehicle
                {
                  Make = vehicleFormViewModel.Make,
                  Name = vehicleFormViewModel.Name,
                  Model = vehicleFormViewModel.Model,
                  BranchesId = vehicleFormViewModel.Branch,
                  RegistrationNumber = vehicleFormViewModel.RegistrationNumber,
                  VehicleTypeId = vehicleFormViewModel.VehicleType
                };
                var uriString = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Vehicles");
                string jsonString = JsonSerializer.Serialize(_vechicle);
                using (var client = new HttpClient())
                {
                    var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(uriString, httpContent);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        ViewBag.Error = "Error :" + response.StatusCode+  " Please ensure that the Braches Dropdown List is Populated - To Add Braches use Admin Tools " + response.StatusCode;
                        var uriStringBranches = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Branches");
                            HttpResponseMessage responseBranches = await client.GetAsync(uriStringBranches);
                            branches = responseBranches.Content.ReadAsAsync<IList<Branches>>().Result;
                        var _vehicleTypes = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "VehicleTypes");

                        
                            HttpResponseMessage responseVehicleTypes = await client.GetAsync(_vehicleTypes);
                            
                            vehicleTypes = responseVehicleTypes.Content.ReadAsAsync<IList<VehicleType>>().Result;
                        
                        var _vehicleFormViewModel = new VehicleFormViewModel
                        {

                            Branches = branches,
                            VehicleTypes = vehicleTypes

                        };
                        return View(_vehicleFormViewModel);
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(vehicleFormViewModel);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Vehicles/", id);
            var vehicle = new Vehicle();
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                vehicle = response.Content.ReadAsAsync<Vehicle>().Result;
            }
            if (vehicle == null)
            {
                return NotFound();
            }
            var _branches = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Branches");

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_branches);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                branches = response.Content.ReadAsAsync<IList<Branches>>().Result;
            }

            var _vehicleTypes = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "VehicleTypes");

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_vehicleTypes);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                vehicleTypes = response.Content.ReadAsAsync<IList<VehicleType>>().Result;
            }
            var vehicleFormViewModel = new VehicleFormViewModel
            {
                Id = vehicle.Id,
                Make = vehicle.Make,
                IsDeleted = vehicle.IsDeleted,
                Name = vehicle.Name,
                Model = vehicle.Model,
                Branch = vehicle.BranchesId,
                RegistrationNumber = vehicle.RegistrationNumber,
                VehicleType = vehicle.VehicleTypeId,
                Branches = branches,
                VehicleTypes = vehicleTypes

            };
            

            return View(vehicleFormViewModel);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Model,Make,RegistrationNumber,IsDeleted,BranchesId,VehicleType")] VehicleFormViewModel vehicleFormViewModel)
        {
            if (id != vehicleFormViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Vehicles/", id);
                var _vechicle = new Vehicle
                {
                    Make = vehicleFormViewModel.Make,
                    Name = vehicleFormViewModel.Name,
                    Model = vehicleFormViewModel.Model,
                    BranchesId = vehicleFormViewModel.Branch,
                    RegistrationNumber = vehicleFormViewModel.RegistrationNumber,
                    VehicleTypeId = vehicleFormViewModel.VehicleType
                };
                string jsonString = JsonSerializer.Serialize(_vechicle);
                using (var client = new HttpClient())
                {
                    var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync(uriString, httpContent);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        ViewBag.Error = "Error : " + response.StatusCode;
                        return View(vehicleFormViewModel);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleFormViewModel);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Vehicles/", id);
            var vehicle = new Vehicle();
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.DeleteAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                vehicle = response.Content.ReadAsAsync<Vehicle>().Result;
            }
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Vehicles/", id);

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
