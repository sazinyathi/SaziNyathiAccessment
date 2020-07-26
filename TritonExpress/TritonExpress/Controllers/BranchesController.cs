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
using PagedList;
using TritonExpress.A;
using TritonExpress.Models;
using TritonExpress.ViewModels;

namespace TritonExpress.Controllers
{
    public class BranchesController : Controller
    {
        private readonly TritonExpressDbContex1t _context;
        private IEnumerable<Branches> branches = null;
        private IEnumerable<Province> provinces = null;
        private readonly IConfiguration configuration;

        public BranchesController(TritonExpressDbContex1t context, IConfiguration configuration)
        {
            _context = context;
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
                    //Log.Error(response.ReasonPhrase);
                    //return default(TResult);
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
                    //Log.Error(response.ReasonPhrase);
                    //return default(TResult);
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
                
                var uriString = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Branches/");
                string jsonString = JsonSerializer.Serialize(branches);
                using (var client = new HttpClient())
                {
                    var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(uriString, httpContent);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        //Log.Error(response.ReasonPhrase);
                        //return default(TResult);
                    }
                    
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(branchesFormViewModel);
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
                    //Log.Error(response.ReasonPhrase);
                    //return default(TResult);
                }
                branches = await response.Content.ReadAsAsync<Branches>();

            }
            var viewModel = new BranchesFormViewModel
            {
                Address = branches.Address,
                BranchDescription = branches.BranchDescription,
                BranchName = branches.BranchName,
                Id = branches.Id,
                Province = branches.ProvincesId,
                Provinces = _context.Provinces.ToList()
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
                try
                {
                    var branch = new Branches
                    {
                      Address = branchesFormViewModel.Address,
                      BranchName  = branchesFormViewModel.BranchName,
                      BranchDescription = branchesFormViewModel.BranchDescription,
                      ProvincesId = branchesFormViewModel.Province,
                      IsActive = branchesFormViewModel.IsActive,
                      IsDeleted = branchesFormViewModel.IsDeleted
                    };

                    _context.Update(branch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchesExists(branchesFormViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
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

            var branches = await _context.Branches
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var branches = await _context.Branches.FindAsync(id);
            _context.Branches.Remove(branches);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BranchesExists(int id)
        {
            return _context.Branches.Any(e => e.Id == id);
        }
    }
}
