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
    public class StatusController : Controller
    {
        private readonly IConfiguration configuration;
        private IEnumerable<Status> statuses = null;

        public StatusController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // GET: Status
        public async Task<IActionResult> Index(string searchString)
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
                var readJob = response.Content.ReadAsAsync<IList<Status>>();
                statuses = readJob.Result;
                if (!String.IsNullOrEmpty(searchString))
                {
                    statuses = readJob.Result.Where(
                        s => s.Name.ToLower().Contains(searchString.ToLower())
                       || s.Description.ToLower().Contains(searchString.ToLower()));
                }

            }
            return View(statuses);
        }

        // GET: Status/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = new Status();
            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Status/", id);
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                status = await response.Content.ReadAsAsync<Status>();

            }

            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // GET: Status/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,IsDeleted")] Status status)
        {
            if (ModelState.IsValid)
            {
                var uriString = string.Format("{0}{1}", configuration["TritonExpressEndopint"], "Status");
                string jsonString = JsonSerializer.Serialize(status);
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
            return View(status);
        }

        // GET: Status/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = new Status();
            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Status/", id);
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                status = await response.Content.ReadAsAsync<Status>();
            }
            if (status == null)
            {
                return NotFound();
            }
            return View(status);
        }

        // POST: Status/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,IsDeleted")] Status status)
        {
            if (id != status.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Status/", id);
                string jsonString = JsonSerializer.Serialize(status);
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
            return View(status);
        }

        // GET: Status/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Status/", id);
            var status = new Status();
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.DeleteAsync(uriString);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.Error = "Error : " + response.StatusCode;
                    return View();
                }
                status = response.Content.ReadAsAsync<Status>().Result;
            }
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uriString = string.Format("{0}{1}{2}", configuration["TritonExpressEndopint"], "Status/", id);

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
