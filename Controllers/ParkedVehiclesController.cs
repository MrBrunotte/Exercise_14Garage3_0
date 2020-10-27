using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Data;
using Garage3.Models;
using System.Data;

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
        public async Task<IActionResult> Index()
        {
            var garage3Context = _context.ParkedVehicle.Include(p => p.Member).Include(p => p.VehicleType);
            return View(await garage3Context.ToListAsync());
        }

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
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "Id", "FullName");
            ViewData["VehicleTypeID"] = new SelectList(_context.Set<VehicleTypes>(), "ID", "VehicleType");
            return View();
        }


        //Soile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckInVehicle([Bind("ID,VehicleType,Member,RegNum,Color,Make,Model")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!RegNumExists(parkedVehicle.RegNum))
                    {
                        parkedVehicle.ArrivalTime = DateTime.Now;
                        _context.Add(parkedVehicle);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError("RegNum", $"{parkedVehicle.RegNum} Already parked.");
                        return View();
                    }
                }
                catch (DBConcurrencyException)
                {
                    //ToDo
                    if (RegNumExists(parkedVehicle.RegNum))
                    {
                        return RedirectToAction(nameof(Index));
                        // return RedirectToAction(nameof(Feedback), new { RegNum = parkedVehicle.RegNum, Message = "The Registraion number exist, Some error occured" });
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Feedback), new { RegNum = parkedVehicle.RegNum, Message = "Has been checked in" });
                 return RedirectToAction(nameof(Index));
            }
            return View(parkedVehicle);
        }


        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,VehicleTypeID,MemberID,RegNum,Color,Make,Model")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    if (!RegNumExists(parkedVehicle.RegNum))
                    {
                        parkedVehicle.ArrivalTime = DateTime.Now;
                        _context.Add(parkedVehicle);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError("RegNum", $"{parkedVehicle.RegNum} Already parked.");
                        return View();
                    }
                }
                catch (DBConcurrencyException)
                {

                    //ToDo
                    if (RegNumExists(parkedVehicle.RegNum))
                    {
                        return RedirectToAction(nameof(Index));
                        // return RedirectToAction(nameof(Feedback), new { RegNum = parkedVehicle.RegNum, Message = "The Registraion number exist, Some error occured" });
                    }
                    else
                    {
                        throw;
                    };
                }
                //return RedirectToAction(nameof(Feedback), new { RegNum = parkedVehicle.RegNum, Message = "Has been checked in" });
                return RedirectToAction(nameof(Index));

                //_context.Add(parkedVehicle);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "Id", "FullName", parkedVehicle.MemberID);
            ViewData["VehicleTypeID"] = new SelectList(_context.Set<VehicleTypes>(), "ID", "VehicleType", parkedVehicle.VehicleTypeID);
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
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "Id", "FullName", parkedVehicle.MemberID);
            ViewData["VehicleTypeID"] = new SelectList(_context.Set<VehicleTypes>(), "ID", "VehicleType", parkedVehicle.VehicleTypeID);
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,VehicleTypesID,MemberID,RegNum,Color,Make,Model")] ParkedVehicle parkedVehicle)
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
        //Soile
        private bool RegNumExists(string regNum)
        {
            return _context.ParkedVehicle.Any(e => e.RegNum == regNum);
        }
    }
}
