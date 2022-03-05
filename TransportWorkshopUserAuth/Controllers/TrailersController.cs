using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransportWorkshopUserAuth.Data;
using TransportWorkshopUserAuth.Models;

namespace TransportWorkshopUserAuth.Controllers
{
    public class TrailersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrailersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trailers
        public async Task<IActionResult> Index()
        {
            var transportWorkshopCoreContext = _context.Trailers.Include(t => t.Tire);
            return View(await transportWorkshopCoreContext.ToListAsync());
        }

        // GET: Trailers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trailer = await _context.Trailers
                .Include(t => t.Tire)
                .FirstOrDefaultAsync(m => m.TrailerId == id);
            if (trailer == null)
            {
                return NotFound();
            }

            return View(trailer);
        }

        // GET: Trailers/Create
        public IActionResult Create()
        {
            ViewData["TireId"] = new SelectList(_context.Tires, "TireId", "Name");//Brand
            return View();
        }

        // POST: Trailers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrailerId,Number,Massa,TireId")] Trailer trailer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trailer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TireId"] = new SelectList(_context.Tires, "TireId", "Name", trailer.TireId);
            return View(trailer);
        }

        // GET: Trailers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trailer = await _context.Trailers.FindAsync(id);
            if (trailer == null)
            {
                return NotFound();
            }
            ViewData["TireId"] = new SelectList(_context.Tires, "TireId", "Name", trailer.TireId);
            return View(trailer);
        }

        // POST: Trailers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrailerId,Number,Massa,TireId")] Trailer trailer)
        {
            if (id != trailer.TrailerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trailer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrailerExists(trailer.TrailerId))
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
            ViewData["TireId"] = new SelectList(_context.Tires, "TireId", "Name", trailer.TireId);
            return View(trailer);
        }

        // GET: Trailers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trailer = await _context.Trailers
                .Include(t => t.Tire)
                .FirstOrDefaultAsync(m => m.TrailerId == id);
            if (trailer == null)
            {
                return NotFound();
            }

            return View(trailer);
        }

        // POST: Trailers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trailer = await _context.Trailers.FindAsync(id);
            _context.Trailers.Remove(trailer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrailerExists(int id)
        {
            return _context.Trailers.Any(e => e.TrailerId == id);
        }
    }
}
