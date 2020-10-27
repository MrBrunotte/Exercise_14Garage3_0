using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Data;
using Garage3.Models;
using Garage3.Models.ViewModels;

namespace Garage3.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly Garage3Context _context;

        public ParkedVehiclesController(Garage3Context context)
        {
            _context = context;
        }

        // GET: ParkedVehicles
        // Added by Stefan search functionality
        public async Task<IActionResult> Index()
        {
            var vehicles = await _context.ParkedVehicle.ToListAsync();

            var model = new VehicleTypeViewModel
            {
                VehicleList = vehicles,
                VehicleTypes = await TypeAsync()
            };
            return View(model);
        }

        private async Task<IEnumerable<SelectListItem>> TypeAsync()
        {
            return await _context.ParkedVehicle
                         .Select(m => m.VehicleType)
                         // Only distinct type, no multiples
                         .Distinct()
                         .Select(m => new SelectListItem
                         {
                             Text = m.ToString(),
                             Value = m.ToString()
                         })
                         .ToListAsync();
        }

        //public async Task<IActionResult> Filter(VehicleTypeViewModel viewModel)
        //{
        //    var vehicles = string.IsNullOrWhiteSpace(viewModel.SearchString) ?
        //        _context.ParkedVehicle :
        //        _context.ParkedVehicle.Where(m => m.RegNum.Contains(viewModel.SearchString));

        //    vehicles = viewModel.VehicleType == null ?
        //        vehicles :
        //        vehicles.Where(m => m.VehicleType == viewModel.VehicleType);

        //    var model = new VehicleTypeViewModel
        //    {
        //        VehicleList = await vehicles.ToListAsync(),
        //        VehicleTypes = await TypeAsync()
        //    };

        //    return View(nameof(Index), model);
        //}

        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .Include(p => p.Member)
                .Include(p => p.VehicleType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Create
        public IActionResult Create()
        {
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "Id", "ConfirmPassword");
            ViewData["VehicleTypesID"] = new SelectList(_context.Set<VehicleTypes>(), "ID", "VehicleTYpe");
            return View();
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,VehicleTypesID,MemberID,RegNum,Color,Make,Model,ArrivalTime")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "Id", "ConfirmPassword", parkedVehicle.MemberID);
            ViewData["VehicleTypeID"] = new SelectList(_context.Set<VehicleTypes>(), "ID", "VehicleTYpe", parkedVehicle.VehicleTypeID);
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "Id", "ConfirmPassword", parkedVehicle.MemberID);
            ViewData["VehicleTypeID"] = new SelectList(_context.Set<VehicleTypes>(), "ID", "VehicleTYpe", parkedVehicle.VehicleTypeID);
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,VehicleTypesID,MemberID,RegNum,Color,Make,Model,ArrivalTime")] ParkedVehicle parkedVehicle)
        {
            if (id != parkedVehicle.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkedVehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleExists(parkedVehicle.ID))
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
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "Id", "ConfirmPassword", parkedVehicle.MemberID);
            ViewData["VehicleTypeID"] = new SelectList(_context.Set<VehicleTypes>(), "ID", "VehicleTYpe", parkedVehicle.VehicleTypeID);
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .Include(p => p.Member)
                .Include(p => p.VehicleType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            _context.ParkedVehicle.Remove(parkedVehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkedVehicleExists(int id)
        {
            return _context.ParkedVehicle.Any(e => e.ID == id);
        }
    }
}
