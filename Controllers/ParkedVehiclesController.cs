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

        public async Task<IActionResult> Filter(VehicleTypeViewModel viewModel)
        {
            var vehicles = string.IsNullOrWhiteSpace(viewModel.SearchString) ?
                _context.ParkedVehicle :
                _context.ParkedVehicle.Where(m => m.RegNum.Contains(viewModel.SearchString));

            vehicles = viewModel.VehicleTypes == null ?
                vehicles :
                vehicles.Where(m => m.VehicleType == viewModel.VehicleTypes);

            var model = new VehicleTypeViewModel
            {
                VehicleList = await vehicles.ToListAsync(),
                VehicleTypes = await TypeAsync()
            };

            return View(nameof(Index), model);
        }

        // Torbjörn

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
                .Include(p => p.Parking)
                  .ThenInclude(p => p.ParkingSpace)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (parkedVehicle == null)
            {
                return NotFound();
            }

            var detailsView = new ParkedVehicleDetailsViewModel
            {
                VehicleType = parkedVehicle.VehicleType,
                Member = parkedVehicle.Member,
                RegNum = parkedVehicle.RegNum,
                Color = parkedVehicle.Color,
                Make = parkedVehicle.Make,
                Model = parkedVehicle.Model,
                ArrivalTime = parkedVehicle.ArrivalTime,
                Period = DateTime.Now - parkedVehicle.ArrivalTime,
                ParkingSpaces = parkedVehicle.Parking.Select(s => s.ParkingSpace).ToList()

            };

            return View(detailsView);
        }


        //Soile
        public async Task<IActionResult> OverView()
        {
            
            var vehicles = await _context.ParkedVehicle.ToListAsync();

            var model = new List<OverViewViewModel>();
            //var pV = await _context.ParkedVehicle.Include(p => p.VehicleType.VehicleType).ToListAsync();

            foreach (var vehicle in vehicles)
            {

                var arrival = vehicle.ArrivalTime;
                var nowTime = DateTime.Now;

                model.Add(new OverViewViewModel
                {
                    VehicleType = vehicle.VehicleType,
                    RegNum = vehicle.RegNum,
                    ArrivalTime = arrival,
                    Period = nowTime - arrival
                });
            }

            return View(model);
        }


        //Soile
        // GET: ParkedVehicles/CheckInVehicle
        public IActionResult CheckInVehicle()
        {
            //var model = new List<CheckInVehicleViewModel>();
            //ToDo -  set member - as already logged in -  no selectlist
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
       


        //********** END SOILE **********



        // GET: ParkedVehicles/Create
        public IActionResult Create()
        {
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "Id", "FullName");
            ViewData["VehicleTypeID"] = new SelectList(_context.Set<VehicleTypes>(), "ID", "VehicleType");
            return View();
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
