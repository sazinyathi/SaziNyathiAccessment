using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TritonExpress.A;
using TritonExpress.Models;

namespace TritonExpress.Controllers
{
    public class ProvincesController : Controller
    {
        private readonly TritonExpressDbContex1t _context;
        private readonly IConfiguration configuration;
        private IEnumerable<Province> provinces = null;
        public ProvincesController(TritonExpressDbContex1t context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        // GET: Provinces
        public async Task<IActionResult> Index(string searchString)
        {
            
            var uriString = string.Format("{0}{1}", configuration["TritonExpressEndopint"],"Provinces");

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                    var readJob = response.Content.ReadAsAsync<IList<Province>>();
                    provinces = readJob.Result;
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        provinces = readJob.Result.Where(
                            s => s.Name.ToLower().Contains(searchString.ToLower())
                           || s.Description.ToLower().Contains(searchString.ToLower()));
                    }
             
            }
            return View(provinces);
        }

        // GET: Provinces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }
            var province = new Province();
            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Provinces/",id);
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                province = await response.Content.ReadAsAsync<Province>();
                
            }
            

            return View(province);
        }

        // GET: Provinces/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Provinces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,IsDeleted")] Province province)
        {
            if (ModelState.IsValid)
            {
                var uriString = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Provinces/");
                using (var client = new HttpClient())
                {
                    var httpContent = new StringContent(province.ToString(), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(uriString, httpContent);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        ViewBag.Error = "Error : "+ response.StatusCode;
                        return View();
                    }
                }
                
            }
            return View(province);
        }

        // GET: Provinces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var province = new Province();
            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Provinces/", id);
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                  ViewBag.Error = "Error : " + response.StatusCode;
                  return View();
                }
                province = await response.Content.ReadAsAsync<Province>();
            }
         
            return View(province);
        }

        // POST: Provinces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,IsDeleted")] Province province)
        {
            if (id != province.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(province);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvinceExists(province.Id))
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
            return View(province);
        }

        // GET: Provinces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await _context.Provinces
                .FirstOrDefaultAsync(m => m.Id == id);
            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }

        // POST: Provinces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var province = await _context.Provinces.FindAsync(id);
            _context.Provinces.Remove(province);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvinceExists(int id)
        {
            return _context.Provinces.Any(e => e.Id == id);
        }
    }
}
