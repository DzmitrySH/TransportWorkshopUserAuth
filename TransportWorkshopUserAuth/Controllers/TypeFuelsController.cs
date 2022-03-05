using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransportWorkshopUserAuth.Data;
using TransportWorkshopUserAuth.Models;

namespace TransportWorkshopUserAuth.Controllers
{
    public class TypeFuelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeFuelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeFuels
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeFuels.ToListAsync());
        }

        // GET: TypeFuels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeFuel = await _context.TypeFuels
                .FirstOrDefaultAsync(m => m.TypeFuelId == id);
            if (typeFuel == null)
            {
                return NotFound();
            }

            return View(typeFuel);
        }

        // GET: TypeFuels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeFuels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeFuelId,Fuel,Cost,ToDate")] TypeFuel typeFuel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeFuel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeFuel);
        }

        // GET: TypeFuels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeFuel = await _context.TypeFuels.FindAsync(id);
            if (typeFuel == null)
            {
                return NotFound();
            }
            return View(typeFuel);
        }

        // POST: TypeFuels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeFuelId,Fuel,Cost,ToDate")] TypeFuel typeFuel)
        {
            if (id != typeFuel.TypeFuelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeFuel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeFuelExists(typeFuel.TypeFuelId))
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
            return View(typeFuel);
        }

        // GET: TypeFuels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeFuel = await _context.TypeFuels
                .FirstOrDefaultAsync(m => m.TypeFuelId == id);
            if (typeFuel == null)
            {
                return NotFound();
            }

            return View(typeFuel);
        }

        // POST: TypeFuels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeFuel = await _context.TypeFuels.FindAsync(id);
            _context.TypeFuels.Remove(typeFuel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeFuelExists(int id)
        {
            return _context.TypeFuels.Any(e => e.TypeFuelId == id);
        }
    }
}
