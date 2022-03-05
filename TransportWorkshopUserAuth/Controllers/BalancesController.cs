using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TransportWorkshopUserAuth.Data;
using TransportWorkshopUserAuth.Models;

namespace TransportWorkshopUserAuth.Controllers
{
    public class BalancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BalancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Balances
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewData["LeftoverSortParm"] = sortOrder == "Leftover" ? "leftover_desc" : "Leftover";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var balances = from s in _context.Balances.Include(b => b.AutoCar)
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                balances = balances.Where(s => s.AutoCar.NameAuto.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "date_desc":
                    balances = balances.OrderByDescending(s => s.Date);
                    break;
                case "Leftover":
                    balances = balances.OrderBy(s => s.Leftovers);
                    break;
                case "leftover_desc":
                    balances = balances.OrderByDescending(s => s.Leftovers);
                    break;
                default:
                    balances = balances.OrderBy(s => s.Date);
                    break;
            }

            int pageSize = 5;
            return View(await PageViewModel<Balance>.CreateAsync(balances.AsNoTracking(), pageNumber ?? 1, pageSize));
            //var transportWorkshopCoreContext = _context.Balances.Include(b => b.AutoCar);
            //return View(await transportWorkshopCoreContext.ToListAsync());
        }

        // GET: Balances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var balance = await _context.Balances
                .Include(b => b.AutoCar)
                .FirstOrDefaultAsync(m => m.BalanceId == id);
            if (balance == null)
            {
                return NotFound();
            }

            return View(balance);
        }

        // GET: Balances/Create
        public IActionResult Create()
        {
            ViewData["AutoCarId"] = new SelectList(_context.AutoCars, "AutoCarId", "NameAuto");
            return View();
        }

        // POST: Balances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BalanceId,Date,AutoCarId,Leftovers,Sug")] Balance balance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(balance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutoCarId"] = new SelectList(_context.AutoCars, "AutoCarId", "NameAuto", balance.AutoCarId);
            return View(balance);
        }

        // GET: Balances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var balance = await _context.Balances.FindAsync(id);
            if (balance == null)
            {
                return NotFound();
            }
            ViewData["AutoCarId"] = new SelectList(_context.AutoCars, "AutoCarId", "NameAuto", balance.AutoCarId);
            return View(balance);
        }

        // POST: Balances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BalanceId,Date,AutoCarId,Leftovers,Sug")] Balance balance)
        {
            if (id != balance.BalanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(balance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BalanceExists(balance.BalanceId))
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
            ViewData["AutoCarId"] = new SelectList(_context.AutoCars, "AutoCarId", "NameAuto", balance.AutoCarId);
            return View(balance);
        }

        // GET: Balances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var balance = await _context.Balances
                .Include(b => b.AutoCar)
                .FirstOrDefaultAsync(m => m.BalanceId == id);
            if (balance == null)
            {
                return NotFound();
            }

            return View(balance);
        }

        // POST: Balances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var balance = await _context.Balances.FindAsync(id);
            _context.Balances.Remove(balance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BalanceExists(int id)
        {
            return _context.Balances.Any(e => e.BalanceId == id);
        }
    }
}
