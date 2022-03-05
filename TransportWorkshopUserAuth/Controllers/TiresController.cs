using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TransportWorkshopUserAuth.Data;
using TransportWorkshopUserAuth.Models;

namespace TransportWorkshopUserAuth.Controllers
{
    public class TiresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TiresController(ApplicationDbContext context)
        {

            _context = context;
        }

        // GET: Tires
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewData["RunSortParm"] = sortOrder == "Run" ? "run_desc" : "Run";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var tires = from s in _context.Tires
                          select s;
            switch (sortOrder)
            {
                case "date_desc":
                    tires = tires.OrderByDescending(s => s.Date);
                    break;
                case "Run":
                    tires = tires.OrderBy(s => s.RunStart);
                    break;
                case "run_desc":
                    tires = tires.OrderByDescending(s => s.RunStart);
                    break;
                default:
                    tires = tires.OrderBy(s => s.Date);
                    break;
            }

            int pageSize = 8;
            return View(await PageViewModel<Tire>.CreateAsync(tires.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await _context.Tires.ToListAsync());
        }

        // GET: Tires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tire = await _context.Tires
                .FirstOrDefaultAsync(m => m.TireId == id);
            if (tire == null)
            {
                return NotFound();
            }

            return View(tire);
        }

        // GET: Tires/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TireId,Name,Brand,Date,RunStart")] Tire tire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tire);
        }

        // GET: Tires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tire = await _context.Tires.FindAsync(id);
            if (tire == null)
            {
                return NotFound();
            }
            return View(tire);
        }

        // POST: Tires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TireId,Name,Brand,Date,RunStart")] Tire tire)
        {
            if (id != tire.TireId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TireExists(tire.TireId))
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
            return View(tire);
        }

        // GET: Tires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tire = await _context.Tires
                .FirstOrDefaultAsync(m => m.TireId == id);
            if (tire == null)
            {
                return NotFound();
            }

            return View(tire);
        }

        // POST: Tires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tire = await _context.Tires.FindAsync(id);
            _context.Tires.Remove(tire);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TireExists(int id)
        {
            return _context.Tires.Any(e => e.TireId == id);
        }
    }
}
