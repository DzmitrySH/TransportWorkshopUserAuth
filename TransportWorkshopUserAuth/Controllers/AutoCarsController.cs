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
    public class AutoCarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AutoCarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AutoCars
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string currentFilter2, string searchString2, string currentFilter3, string searchString3, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["MileageSortParm"] = sortOrder == "Mileage" ? "mileage_desc" : "Mileage";

            if (searchString != null || searchString2 != null || searchString3 != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
                searchString2 = currentFilter2;
                searchString3 = currentFilter3;
            }
            ViewData["CurrentFilter"] = searchString;
            var autocars = from s in _context.AutoCars.Include(a => a.Driver).Include(a => a.NormaFuel).Include(a => a.Tire).Include(a => a.Trailer).Include(a => a.TypeAuto).Include(a => a.TypeFuel)
                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                autocars = autocars.Where(s => s.Driver.FirsLastMidName.Contains(searchString));
            }
            ViewData["CurrentFilter2"] = searchString2;
            if (!String.IsNullOrEmpty(searchString2))
            {
                autocars = autocars.Where(s => s.TypeFuel.Fuel.Contains(searchString2));
            }
            ViewData["CurrentFilter3"] = searchString3;
            if (!String.IsNullOrEmpty(searchString3))
            {
                autocars = autocars.Where(s => s.NameAuto.Contains(searchString3));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    autocars = autocars.OrderByDescending(s => s.NameAuto);
                    break;
                case "Mileage":
                    autocars = autocars.OrderBy(s => s.Mileage);
                    break;
                case "mileage_desc":
                    autocars = autocars.OrderByDescending(s => s.Mileage);
                    break;
                default:
                    autocars = autocars.OrderBy(s => s.NameAuto);
                    break;
            }

            int pageSize = 5;
            return View(await PageViewModel<AutoCar>.CreateAsync(autocars.AsNoTracking(), pageNumber ?? 1, pageSize));
            //var transportWorkshopCoreContext = _context.AutoCars.Include(a => a.Driver).Include(a => a.NormaFuel).Include(a => a.Tire).Include(a => a.Trailer).Include(a => a.TypeAuto).Include(a => a.TypeFuel);
            //return View(await transportWorkshopCoreContext.ToListAsync());
        }

        // GET: AutoCars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autoCar = await _context.AutoCars
                .Include(a => a.Driver)
                .Include(a => a.NormaFuel)
                .Include(a => a.Tire)
                .Include(a => a.Trailer)
                .Include(a => a.TypeAuto)
                .Include(a => a.TypeFuel)
                .FirstOrDefaultAsync(m => m.AutoCarId == id);
            if (autoCar == null)
            {
                return NotFound();
            }

            return View(autoCar);
        }

        // GET: AutoCars/Create
        public IActionResult Create()
        {
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "FirsLastMidName");
            ViewData["NormaFuelId"] = new SelectList(_context.NormaFuels, "NormaFuelId", "Linear");//"NormaFuelId"
            ViewData["TireId"] = new SelectList(_context.Tires, "TireId", "Name");//Brand
            ViewData["TrailerId"] = new SelectList(_context.Trailers, "TrailerId", "Number");
            ViewData["TypeAutoId"] = new SelectList(_context.TypeAutos, "TypeAutoId", "NameType");
            ViewData["TypeFuelId"] = new SelectList(_context.TypeFuels, "TypeFuelId", "Fuel");
            return View();
        }

        // POST: AutoCars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AutoCarId,NameAuto,Number,Mileage,TypeFuelId,NormaFuelId,TrailerId,DriverId,TypeAutoId,TireId,Harmfulness,Navigation,Injector")] AutoCar autoCar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autoCar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "FirsLastMidName", autoCar.DriverId);
            ViewData["NormaFuelId"] = new SelectList(_context.NormaFuels, "NormaFuelId", "Linear", autoCar.NormaFuelId);
            ViewData["TireId"] = new SelectList(_context.Tires, "TireId", "Name", autoCar.TireId);
            ViewData["TrailerId"] = new SelectList(_context.Trailers, "TrailerId", "Number", autoCar.TrailerId);
            ViewData["TypeAutoId"] = new SelectList(_context.TypeAutos, "TypeAutoId", "NameType", autoCar.TypeAutoId);
            ViewData["TypeFuelId"] = new SelectList(_context.TypeFuels, "TypeFuelId", "Fuel", autoCar.TypeFuelId);
            return View(autoCar);
        }

        // GET: AutoCars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autoCar = await _context.AutoCars.FindAsync(id);
            if (autoCar == null)
            {
                return NotFound();
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "FirsLastMidName", autoCar.DriverId);
            ViewData["NormaFuelId"] = new SelectList(_context.NormaFuels, "NormaFuelId", "Linear", autoCar.NormaFuelId);
            ViewData["TireId"] = new SelectList(_context.Tires, "TireId", "Name", autoCar.TireId);
            ViewData["TrailerId"] = new SelectList(_context.Trailers, "TrailerId", "Number", autoCar.TrailerId);
            ViewData["TypeAutoId"] = new SelectList(_context.TypeAutos, "TypeAutoId", "NameType", autoCar.TypeAutoId);
            ViewData["TypeFuelId"] = new SelectList(_context.TypeFuels, "TypeFuelId", "Fuel", autoCar.TypeFuelId);
            return View(autoCar);
        }

        // POST: AutoCars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AutoCarId,NameAuto,Number,Mileage,TypeFuelId,NormaFuelId,TrailerId,DriverId,TypeAutoId,TireId,Harmfulness,Navigation,Injector")] AutoCar autoCar)
        {
            if (id != autoCar.AutoCarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autoCar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoCarExists(autoCar.AutoCarId))
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
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "FirsLastMidName", autoCar.DriverId);
            ViewData["NormaFuelId"] = new SelectList(_context.NormaFuels, "NormaFuelId", "Linear", autoCar.NormaFuelId);
            ViewData["TireId"] = new SelectList(_context.Tires, "TireId", "Name", autoCar.TireId);
            ViewData["TrailerId"] = new SelectList(_context.Trailers, "TrailerId", "Number", autoCar.TrailerId);
            ViewData["TypeAutoId"] = new SelectList(_context.TypeAutos, "TypeAutoId", "NameType", autoCar.TypeAutoId);
            ViewData["TypeFuelId"] = new SelectList(_context.TypeFuels, "TypeFuelId", "Fuel", autoCar.TypeFuelId);
            return View(autoCar);
        }

        // GET: AutoCars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autoCar = await _context.AutoCars
                .Include(a => a.Driver)
                .Include(a => a.NormaFuel)
                .Include(a => a.Tire)
                .Include(a => a.Trailer)
                .Include(a => a.TypeAuto)
                .Include(a => a.TypeFuel)
                .FirstOrDefaultAsync(m => m.AutoCarId == id);
            if (autoCar == null)
            {
                return NotFound();
            }

            return View(autoCar);
        }

        // POST: AutoCars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autoCar = await _context.AutoCars.FindAsync(id);
            _context.AutoCars.Remove(autoCar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutoCarExists(int id)
        {
            return _context.AutoCars.Any(e => e.AutoCarId == id);
        }
    }
}
