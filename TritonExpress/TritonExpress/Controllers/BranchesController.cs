using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TritonExpress.A;
using TritonExpress.Models;
using TritonExpress.ViewModels;

namespace TritonExpress.Controllers
{
    public class BranchesController : Controller
    {
        private readonly TritonExpressDbContex1t _context;


        public BranchesController(TritonExpressDbContex1t context)
        {
            _context = context;
        }

        // GET: Branches
        public async Task<IActionResult> Index()
        {
            return View(await _context.Branches.ToListAsync());
        }

        // GET: Branches/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Branches/Create
        public IActionResult Create()
        {
            var viewModel = new BranchesFormViewModel
            {
                Provinces = _context.Provinces.ToList()
            };
           
            return View(viewModel);
        }

        // POST: Branches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BranchName,BranchDescription,Address,IsDeleted,IsActive,ProvincesId")] Branches branches)
        {
            if (ModelState.IsValid)
            {
                _context.Add(branches);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(branches);
        }

        // GET: Branches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branches = await _context.Branches.FindAsync(id);
            if (branches == null)
            {
                return NotFound();
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,BranchName,BranchDescription,Address,IsDeleted,IsActive,ProvincesId")] Branches branches)
        {
            if (id != branches.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(branches);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchesExists(branches.Id))
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
            return View(branches);
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
