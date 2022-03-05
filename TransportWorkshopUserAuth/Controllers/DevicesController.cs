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
    public class DevicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DevicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Devices
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var devices = from s in _context.Devices.Include(d => d.Tire).Include(d => d.TypeFuel).Include(d => d.WinterTime)
                               select s;
            switch (sortOrder)
            {
                case "name_desc":
                    devices = devices.OrderByDescending(s => s.Namedevice);
                    break;
                default:
                    devices = devices.OrderBy(s => s.Namedevice);
                    break;
            }

            int pageSize = 8;
            return View(await PageViewModel<Device>.CreateAsync(devices.AsNoTracking(), pageNumber ?? 1, pageSize));
            //var transportWorkshopCoreContext = _context.Devices.Include(d => d.Tire).Include(d => d.TypeFuel).Include(d => d.WinterTime);
            //return View(await transportWorkshopCoreContext.ToListAsync());
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.Tire)
                .Include(d => d.TypeFuel)
                .Include(d => d.WinterTime)
                .FirstOrDefaultAsync(m => m.DeviceId == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            ViewData["TireId"] = new SelectList(_context.Tires, "TireId", "Name");//Brand
            ViewData["TypeFuelId"] = new SelectList(_context.TypeFuels, "TypeFuelId", "Fuel");
            ViewData["WinterTimeId"] = new SelectList(_context.WinterTimes, "WinterTimeId", "DateStart");//WinterTimeId
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceId,Namedevice,TypeFuelId,SumerTime,WinterTimeId,Harmfulness,TireId")] Device device)
        {
            if (ModelState.IsValid)
            {
                _context.Add(device);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TireId"] = new SelectList(_context.Tires, "TireId", "Name", device.TireId);
            ViewData["TypeFuelId"] = new SelectList(_context.TypeFuels, "TypeFuelId", "Fuel", device.TypeFuelId);
            ViewData["WinterTimeId"] = new SelectList(_context.WinterTimes, "WinterTimeId", "DateStart", device.WinterTimeId);
            return View(device);
        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }
            ViewData["TireId"] = new SelectList(_context.Tires, "TireId", "Name", device.TireId);
            ViewData["TypeFuelId"] = new SelectList(_context.TypeFuels, "TypeFuelId", "Fuel", device.TypeFuelId);
            ViewData["WinterTimeId"] = new SelectList(_context.WinterTimes, "WinterTimeId", "DateStart", device.WinterTimeId);
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeviceId,Namedevice,TypeFuelId,SumerTime,WinterTimeId,Harmfulness,TireId")] Device device)
        {
            if (id != device.DeviceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(device);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.DeviceId))
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
            ViewData["TireId"] = new SelectList(_context.Tires, "TireId", "Name", device.TireId);
            ViewData["TypeFuelId"] = new SelectList(_context.TypeFuels, "TypeFuelId", "Fuel", device.TypeFuelId);
            ViewData["WinterTimeId"] = new SelectList(_context.WinterTimes, "WinterTimeId", "DateStart", device.WinterTimeId);
            return View(device);
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.Tire)
                .Include(d => d.TypeFuel)
                .Include(d => d.WinterTime)
                .FirstOrDefaultAsync(m => m.DeviceId == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.DeviceId == id);
        }
    }
}
