using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransportWorkshopUserAuth.Data;
using TransportWorkshopUserAuth.Models;

namespace TransportWorkshopUserAuth.Controllers
{
    public class WinterTimesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WinterTimesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WinterTimes
        public async Task<IActionResult> Index()
        {
            return View(await _context.WinterTimes.ToListAsync());
        }

        // GET: WinterTimes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var winterTime = await _context.WinterTimes
                .FirstOrDefaultAsync(m => m.WinterTimeId == id);
            if (winterTime == null)
            {
                return NotFound();
            }

            return View(winterTime);
        }

        // GET: WinterTimes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WinterTimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WinterTimeId,WinterNorma,DateStart,DateEnd,Temperature")] WinterTime winterTime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(winterTime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(winterTime);
        }

        // GET: WinterTimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var winterTime = await _context.WinterTimes.FindAsync(id);
            if (winterTime == null)
            {
                return NotFound();
            }
            return View(winterTime);
        }

        // POST: WinterTimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WinterTimeId,WinterNorma,DateStart,DateEnd,Temperature")] WinterTime winterTime)
        {
            if (id != winterTime.WinterTimeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(winterTime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WinterTimeExists(winterTime.WinterTimeId))
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
            return View(winterTime);
        }

        // GET: WinterTimes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var winterTime = await _context.WinterTimes
                .FirstOrDefaultAsync(m => m.WinterTimeId == id);
            if (winterTime == null)
            {
                return NotFound();
            }

            return View(winterTime);
        }

        // POST: WinterTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var winterTime = await _context.WinterTimes.FindAsync(id);
            _context.WinterTimes.Remove(winterTime);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WinterTimeExists(int id)
        {
            return _context.WinterTimes.Any(e => e.WinterTimeId == id);
        }
    }
}
