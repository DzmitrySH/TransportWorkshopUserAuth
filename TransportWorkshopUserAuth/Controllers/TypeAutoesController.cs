using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransportWorkshopUserAuth.Data;
using TransportWorkshopUserAuth.Models;

namespace TransportWorkshopUserAuth.Controllers
{
    public class TypeAutoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeAutoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeAutoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeAutos.ToListAsync());
        }

        // GET: TypeAutoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeAuto = await _context.TypeAutos
                .FirstOrDefaultAsync(m => m.TypeAutoId == id);
            if (typeAuto == null)
            {
                return NotFound();
            }

            return View(typeAuto);
        }

        // GET: TypeAutoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeAutoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeAutoId,NameType")] TypeAuto typeAuto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeAuto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeAuto);
        }

        // GET: TypeAutoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeAuto = await _context.TypeAutos.FindAsync(id);
            if (typeAuto == null)
            {
                return NotFound();
            }
            return View(typeAuto);
        }

        // POST: TypeAutoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeAutoId,NameType")] TypeAuto typeAuto)
        {
            if (id != typeAuto.TypeAutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeAuto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeAutoExists(typeAuto.TypeAutoId))
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
            return View(typeAuto);
        }

        // GET: TypeAutoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeAuto = await _context.TypeAutos
                .FirstOrDefaultAsync(m => m.TypeAutoId == id);
            if (typeAuto == null)
            {
                return NotFound();
            }

            return View(typeAuto);
        }

        // POST: TypeAutoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeAuto = await _context.TypeAutos.FindAsync(id);
            _context.TypeAutos.Remove(typeAuto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeAutoExists(int id)
        {
            return _context.TypeAutos.Any(e => e.TypeAutoId == id);
        }
    }
}
