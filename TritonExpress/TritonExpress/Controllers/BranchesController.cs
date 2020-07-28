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
    public class BranchesController : Controller
    {

        private IEnumerable<Branches> branches = null;
        private IEnumerable<Province> provinces = null;
        private readonly IConfiguration configuration;

        public BranchesController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // GET: Branches
        public async Task<IActionResult> Index(string searchString)
        {
            var uriString = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Branches");

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return View();
                }
                branches = response.Content.ReadAsAsync<IList<Branches>>().Result;

                if (!String.IsNullOrEmpty(searchString))
                {
                    branches = branches.Where(
                       s => s.BranchName.ToLower().Contains(searchString.ToLower())
                    || s.Address.ToLower().Contains(searchString.ToLower())
                    || s.BranchDescription.ToLower().Contains(searchString.ToLower())
                    ).ToList();
                }

            }

            return View(branches);
        }

        // GET: Branches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branches = new Branches();
            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Branches/", id);
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                branches = await response.Content.ReadAsAsync<Branches>();

            }


            return View(branches);
        }

        // GET: Branches/Create
        public async Task<IActionResult> Create()
        {
            var uriString = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Provinces");

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                provinces = response.Content.ReadAsAsync<IList<Province>>().Result;
            }
            var viewModel = new BranchesFormViewModel
            {
                Provinces = provinces
            };

            return View(viewModel);
        }

        // POST: Branches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BranchName,BranchDescription,Address,IsDeleted,IsActive,Province")] BranchesFormViewModel branchesFormViewModel)
        {

            if (ModelState.IsValid)
            {
                var branches = new Branches
                {
                    Address = branchesFormViewModel.Address,
                    BranchDescription = branchesFormViewModel.BranchDescription,
                    BranchName = branchesFormViewModel.BranchName,
                    ProvincesId = branchesFormViewModel.Province,

                };

                var uriString = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Branches");
                string jsonString = JsonSerializer.Serialize(branches);
                using (var client = new HttpClient())
                {
                    var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(uriString, httpContent);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        ViewBag.Error = "Error : " + response.StatusCode;
                    }
                    var uriProvinces = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Provinces");
                    HttpResponseMessage responseProvinces = await client.GetAsync(uriProvinces);
                    provinces = responseProvinces.Content.ReadAsAsync<IList<Province>>().Result;

                    var branchesFormViewModels = new BranchesFormViewModel
                    {
                        Provinces = provinces
                    };
                    return View(branchesFormViewModels);
                }

            }
            using (var client = new HttpClient())
            {
                var uriProvinces = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Provinces");
                HttpResponseMessage response = await client.GetAsync(uriProvinces);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                }

                HttpResponseMessage responseProvinces = await client.GetAsync(uriProvinces);
                provinces = responseProvinces.Content.ReadAsAsync<IList<Province>>().Result;

                var branchesFormViewModels = new BranchesFormViewModel
                {
                    Provinces = provinces
                };
                return View(branchesFormViewModels);
            }
           
        }

        // GET: Branches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branches = new Branches();
            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Branches/", id);
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                branches = await response.Content.ReadAsAsync<Branches>();

            }
            var uriString2 = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Provinces");

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uriString2);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                provinces = response.Content.ReadAsAsync<IList<Province>>().Result;
            }
            var viewModel = new BranchesFormViewModel
            {
                Address = branches.Address,
                BranchDescription = branches.BranchDescription,
                BranchName = branches.BranchName,
                Id = branches.Id,
                Province = branches.ProvincesId,
                Provinces = provinces
            };
            return View(viewModel);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BranchName,BranchDescription,Address,IsDeleted,IsActive,Province")] BranchesFormViewModel branchesFormViewModel)
        {
            if (id != branchesFormViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var branch = new Branches
                {
                    Address = branchesFormViewModel.Address,
                    BranchName = branchesFormViewModel.BranchName,
                    BranchDescription = branchesFormViewModel.BranchDescription,
                    ProvincesId = branchesFormViewModel.Province,
                    IsActive = branchesFormViewModel.IsActive,
                    IsDeleted = branchesFormViewModel.IsDeleted
                };

                var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Branches/", id);
                string jsonString = JsonSerializer.Serialize(branch);
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
            return View(branchesFormViewModel);
        }

        // GET: Branches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Branches/", id);
            var branches = new Branches();
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.DeleteAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                branches = response.Content.ReadAsAsync<Branches>().Result;
            }
            if (branches == null)
            {
                return NotFound();
            }

            return View(branches);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Branches/", id);

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
