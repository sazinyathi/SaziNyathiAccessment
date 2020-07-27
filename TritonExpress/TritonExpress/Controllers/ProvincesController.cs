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

namespace TritonExpress.Controllers
{
    public class ProvincesController : Controller
    {
       
        private readonly IConfiguration configuration;
        private IEnumerable<Province> provinces = null;
        public ProvincesController( IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // GET: Provinces
        public async Task<IActionResult> Index(string searchString)
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
                var uriString = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Provinces");
                string jsonString = JsonSerializer.Serialize(province);
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
              
                    var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Provinces/", id);
                    string jsonString = JsonSerializer.Serialize(province);
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
            return View(province);
        }

        // GET: Provinces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Provinces/", id);
            var province = new Province();
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.DeleteAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                province = response.Content.ReadAsAsync<Province>().Result;
            }
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
            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Provinces/", id);
          
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
