using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TransportWorkshopUserAuth.Data;
using TransportWorkshopUserAuth.Models;

namespace TransportWorkshopUserAuth.Controllers
{
    public class NormaFuelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NormaFuelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NormaFuels
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["LinearSortParm"] = String.IsNullOrEmpty(sortOrder) ? "linear_desc" : "";
            ViewData["SummerSortParm"] = sortOrder == "Summer" ? "summer_desc" : "Summer";
            ViewData["WinterSortParm"] = sortOrder == "Winter" ? "winter_desc" : "Winter";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var normafuels = from s in _context.NormaFuels
                             select s;
            switch (sortOrder)
            {
                case "linear_desc":
                    normafuels = normafuels.OrderByDescending(s => s.Linear);
                    break;
                case "Summer":
                    normafuels = normafuels.OrderBy(s => s.Summer);
                    break;
                case "summer_desc":
                    normafuels = normafuels.OrderByDescending(s => s.Summer);
                    break;
                case "Winter":
                    normafuels = normafuels.OrderBy(s => s.Winter);
                    break;
                case "winter_desc":
                    normafuels = normafuels.OrderByDescending(s => s.Winter);
                    break;
                default:
                    normafuels = normafuels.OrderBy(s => s.Linear);
                    break;
            }

            int pageSize = 8;
            return View(await PageViewModel<NormaFuel>.CreateAsync(normafuels.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await _context.NormaFuels.ToListAsync());
        }

        // GET: NormaFuels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var normaFuel = await _context.NormaFuels
                .FirstOrDefaultAsync(m => m.NormaFuelId == id);
            if (normaFuel == null)
            {
                return NotFound();
            }

            return View(normaFuel);
        }

        // GET: NormaFuels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NormaFuels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NormaFuelId,Linear,Summer,Winter")] NormaFuel normaFuel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(normaFuel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(normaFuel);
        }

        // GET: NormaFuels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var normaFuel = await _context.NormaFuels.FindAsync(id);
            if (normaFuel == null)
            {
                return NotFound();
            }
            return View(normaFuel);
        }

        // POST: NormaFuels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NormaFuelId,Linear,Summer,Winter")] NormaFuel normaFuel)
        {
            if (id != normaFuel.NormaFuelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(normaFuel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NormaFuelExists(normaFuel.NormaFuelId))
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
            return View(normaFuel);
        }

        // GET: NormaFuels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var normaFuel = await _context.NormaFuels
                .FirstOrDefaultAsync(m => m.NormaFuelId == id);
            if (normaFuel == null)
            {
                return NotFound();
            }

            return View(normaFuel);
        }

        // POST: NormaFuels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var normaFuel = await _context.NormaFuels.FindAsync(id);
            _context.NormaFuels.Remove(normaFuel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NormaFuelExists(int id)
        {
            return _context.NormaFuels.Any(e => e.NormaFuelId == id);
        }
    }
}
