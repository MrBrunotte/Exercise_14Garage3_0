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
using System.Xml.Schema;
using Garage3.Models.Entities;

namespace Garage3.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private const double costPerMinute = 0.2;

        private readonly Garage3Context _context;

        public ParkedVehiclesController(Garage3Context context)
        {
            _context = context;
        }

        // GET: ParkedVehicles
        // STEFAN start
        public async Task<IActionResult> Index()
        {
            var vehicles = await _context.ParkedVehicle.Include(p => p.VehicleType).ToListAsync();

            var model = new VehicleTypeViewModel
            {
                VehicleList = vehicles,
                VehicleTypes = await GetTypeAsync()
            };
            return View(model);
        }
        private async Task<IEnumerable<SelectListItem>> GetTypeAsync()
        {
            return await _context.VehicleTypes
                         .Select(m => new SelectListItem
                         {
                             Text = m.VehicleType,
                             Value = m.ID.ToString()
                         })
                         .ToListAsync();
        }
        // Filter for vehicle search and type
        public async Task<IActionResult> Filter(VehicleTypeViewModel viewModel)
        {
            // searchfield
            var vehicles = string.IsNullOrWhiteSpace(viewModel.SearchString) ?
                //ViewData["message"] = "Sorry, no data!";
                _context.ParkedVehicle.Include(p => p.VehicleType) :
                _context.ParkedVehicle.Include(p => p.VehicleType).Where(m => m.RegNum.Contains(viewModel.SearchString));
            //Dropdown 
            vehicles = viewModel.VehicleTypeID == null ?
                vehicles :
                vehicles.Where(m => m.VehicleTypeID == viewModel.VehicleTypeID);

            var model = new VehicleTypeViewModel
            {
                VehicleList = await vehicles.ToListAsync(),
                VehicleTypes = await GetTypeAsync()
            };

            return View(nameof(Index), model);
        }

        // Member search
        public async Task<IActionResult> Index2()
        {
            var members = await _context.Members.ToListAsync();

            var memberModel = new MemberViewModel
            {
                MemberList = members
            };

            return View(memberModel);
        }
        public async Task<IActionResult> FilterMembers(string searchString)
        {
            var member = string.IsNullOrWhiteSpace(searchString) ?
                _context.Members :
                _context.Members.Where(m => m.FirstName.Contains(searchString) || m.LastName.Contains(searchString));

            var model = new MemberViewModel
            {
                MemberList = await member.ToListAsync(),
            };

            return View(nameof(Index2), model);
        }


        // STEFAN END
        //----------------------------------------//

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
                ParkingSpaces = parkedVehicle.Parking.Select(s => s.ParkingSpace).ToList(),
                ID = parkedVehicle.ID
            };

            return View(detailsView);
        }


        //Soile
        public async Task<IActionResult> OverView()
        {

            var vehicles = await _context.ParkedVehicle
                .Include(p => p.VehicleType)
                .Include(p => p.Member)
                .ToListAsync();

            if (vehicles == null)
            {
                return NotFound();
            }

            var model = new List<OverViewViewModel>();


            foreach (var vehicle in vehicles)
            {

                var arrival = vehicle.ArrivalTime;
                var nowTime = DateTime.Now;

                model.Add(new OverViewViewModel
                {
                    Member = vehicle.Member,
                    PhoneNum = vehicle.Member.PhoneNum,
                    VehicleType = vehicle.VehicleType,
                    RegNum = vehicle.RegNum,
                    ArrivalTime = arrival,
                    Period = nowTime - arrival,
                    ID = vehicle.ID
                });
            }

            return View(model);
        }

        //Soile
        public async Task<IActionResult> MemberOverView()
        {
            var members = await _context.Members
                .Include(m => m.ParkedVehicles)
               .ToListAsync();

            var model = new List<MemberOverViewViewModel>();
            foreach (var member in members)
            {
                model.Add(new MemberOverViewViewModel
                {
                    Member = member.FullName,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Email = member.Email,
                    PhoneNum = member.PhoneNum,
                    NumOfVehicles = member.ParkedVehicles.Count,
                    ID = member.Id
                });
            }


            return View(model);
        }

        // GET: ParkedVehicles/MemberDetails/
        public async Task<IActionResult> MemberDetails(int? id)
        {

            var parkedVehicle = await _context.ParkedVehicle
                .Include(p => p.Member)
                .Include(p => p.VehicleType)
                .Where(m => m.MemberID == id)
                .ToListAsync();

            List<MemberDetailsViewModel> newList = new List<MemberDetailsViewModel>();
            foreach (var item in parkedVehicle)
            {
                MemberDetailsViewModel listItem = new MemberDetailsViewModel();
                listItem.ID = item.ID;
                listItem.Member = item.Member;
                listItem.VehicleType = item.VehicleType;
                listItem.RegNum = item.RegNum;
                listItem.Make = item.Make;
                listItem.Model = item.Model;
                newList.Add(listItem);
            }

            return View(newList);
        }

        //Soile
        public IActionResult ValidateRegNum(string regNum)
        {
            if (_context.ParkedVehicle.Any(m => m.RegNum == regNum))
            {
                return Json($"{regNum} is in use");
            }
            return Json(true);
        }
        public IActionResult CheckEmail(string email)
        {
            if (_context.Members.Any(m => m.Email == email))
            {
                return Json($"{email} is in use");
            }
            return Json(true);
        }
        //Soile
        public IActionResult RegisterMember()
        {
            return View();
        }

        //Soile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterMember(RegisterMemberViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var member = new Member
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Email = viewModel.Email,
                    PhoneNum = viewModel.PhoneNum
                };
                if (!EmailExists(viewModel.Email))
                {
                    _context.Add(member);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return Json($"{viewModel.Email} is in use");
                }

                return RedirectToAction(nameof(MemberOverView));
            }

            return View(viewModel);
        }

        //Soile
        public IActionResult CheckInVehicle()
        {
            return View();
        }

        //Soile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckInVehicle(CheckInVehicleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                // Torbjörn
                // Check if enough parking spaces for vehicle

                var size = _context.VehicleTypes
                    .Where(v => v.ID == viewModel.VehicleTypeID)
                    .Select(v => v.FillsNumberOfSpaces)
                    .FirstOrDefault();

                if (!CheckParkingSpaces(size))
                {
                    ModelState.AddModelError("RegNum", $"Not enough free parking spaces. {viewModel.RegNum} requires {size} parking spaces.");
                    return View();
                }

                // Park vehicle
                var vehicles = new ParkedVehicle
                {
                    MemberID = viewModel.MemberID,
                    VehicleTypeID = viewModel.VehicleTypeID,
                    RegNum = viewModel.RegNum,
                    Color = viewModel.Color,
                    Make = viewModel.Make,
                    Model = viewModel.Model
                };

                // Get available parking spaces
                var availableSpaces = _context.ParkingSpace
                    .Include(s => s.Parking)
                    .Where(s => s.Available == true)
                    .OrderBy(s => s.ParkingSpaceNum)
                    .ToList();

                var allocateSpaces = new List<Parking>();
                // Loop through available parkingspaces to find where vehicle can park (checking for adjacent places
                // for large vehicles)

                // TODO: Same test as above, could perhaps do it one time only
                for (var i = 0; i < availableSpaces.Count - size + 1; i++)
                {
                    if (availableSpaces[i + size - 1].ParkingSpaceNum == availableSpaces[i].ParkingSpaceNum + size - 1)
                    {
                        // availableSpaces[i] and the following "numberOfSpaces - 1" spaces are available
                        for (var a = i; a < size; a++)
                        {
                            availableSpaces[a].Available = false;
                            allocateSpaces.Add(new Parking { ParkedVehicle = vehicles, ParkingSpace = availableSpaces[a] });
                        }
                        break;
                    }
                }

                try
                {
                    if (!RegNumExists(viewModel.RegNum))
                    {
                        vehicles.ArrivalTime = DateTime.Now;

                        _context.Add(vehicles);
                        _context.Parking.AddRange(allocateSpaces);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError("RegNum", $"{viewModel.RegNum} Already parked.");
                        return View();
                    }
                }
                catch (DBConcurrencyException)
                {
                    //ToDo
                    if (RegNumExists(viewModel.RegNum))
                    {
                        return RedirectToAction(nameof(Index));
                        //return RedirectToAction(nameof(Feedback), new { RegNum = parkedVehicle.RegNum, Message = "The Registraion number exist, Some error occured" });
                    }
                    else
                    {
                        throw;
                    }
                }

                //return RedirectToAction(nameof(Feedback), new { RegNum = parkedVehicle.RegNum, Message = "Has been checked in" });
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }


        //Soile
        public IActionResult AddNewVehicleType()
        {
            return View();
        }

        //Soile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewVehicleType(AddNewVehicleTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var vTYpe = new VehicleTypes
                {
                    VehicleType = viewModel.VehicleType,
                    FillsNumberOfSpaces = viewModel.FillsNumberOfSpaces
                };
                if (!VehicleExists(viewModel.VehicleType))
                {
                    if (viewModel.FillsNumberOfSpaces.ToString().Equals(""))
                    {
                        viewModel.FillsNumberOfSpaces = 1;
                    }
                    _context.Add(vTYpe);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return Json($"{viewModel.VehicleType} is already in use");
                }

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        private bool VehicleExists(string vType)
        {
            return _context.VehicleTypes.Any(e => e.VehicleType == vType);
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

        // Torbjörn

        // GET: ParkedVehicles/Checkout/5
        public async Task<IActionResult> Checkout(int? id)
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

            var arrival = parkedVehicle.ArrivalTime;
            var checkout = DateTime.Now;

            var checkoutView = new ParkedVehicleCheckoutViewModel
            {
                Member = parkedVehicle.Member,
                RegNum = parkedVehicle.RegNum,
                ArrivalTime = arrival,
                CheckOutTime = checkout,
                Period = checkout - arrival,
                CostPerMinute = costPerMinute,
                Cost = Math.Round((checkout - arrival).TotalMinutes * costPerMinute, 2)
            };

            return View(checkoutView);
        }

        // Torbjörn
        // ParkedVehicle is deleted and Available is set to True in the ParkingSpace(s) that was used

        // POST: ParkedVehicles/Checkout/5
        [HttpPost, ActionName("Checkout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckoutConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle
                .Include(p => p.Member)
                .Include(p => p.Parking)
                .ThenInclude(p => p.ParkingSpace)
                .FirstOrDefaultAsync(m => m.ID == id);

            // To be used in Receipt
            TempData["regnum"] = parkedVehicle.RegNum;
            TempData["arrival"] = parkedVehicle.ArrivalTime;
            TempData["checkout"] = DateTime.Now;
            TempData["membername"] = parkedVehicle.Member.FullName;
            TempData["parkingspace"] = parkedVehicle.Parking.Select(s => s.ParkingSpace.ParkingSpaceNum).ToList();

            // Update ParkingSpace (set Available = True)
            parkedVehicle.Parking.Select(s => s.ParkingSpace)
                .ToList()
                .ForEach(p => p.Available = true);

            _context.ParkedVehicle.Remove(parkedVehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Receipt));
        }

        public IActionResult Receipt()
        {
            var arrival = (DateTime)TempData["arrival"];
            var checkout = (DateTime)TempData["checkout"];
            var parkingSpaces = (ICollection<int>)TempData["parkingspace"];

            // TODO: This is to handle if vehicles exist without being parked. During development
            if (parkingSpaces == null)
            {
                parkingSpaces = new List<int>() { 0 };
            }

            var receipt = new ParkedVehicleReceiptViewModel
            {
                RegNum = (string)TempData["regnum"],
                MemberName = (string)TempData["membername"],
                ArrivalTime = arrival,
                CheckOutTime = checkout,
                Period = checkout - arrival,
                Cost = Math.Round((checkout - arrival).TotalMinutes * costPerMinute, 2),
                ParkingSpaces = parkingSpaces
            };

            return View(receipt);
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
        private bool VehicleTypeExists(string vType)
        {
            return _context.ParkedVehicle.Any(e => e.VehicleType.VehicleType == vType);
        }

        private bool EmailExists(string email)
        {
            return _context.Members.Any(e => e.Email == email);
        }


        public async Task<IActionResult> ParkingSpaceOverview()
        {
            var model = await _context.ParkingSpace
                .Include(s => s.Parking)
                .ThenInclude(s => s.ParkedVehicle)
                .Select(p => new ParkingSpaceOverviewViewModel
                {
                    ID = p.ID,
                    ParkingSpaceNum = p.ParkingSpaceNum,
                    Available = p.Available,
                    RegNum = p.Parking.Select(p => p.ParkedVehicle.RegNum).FirstOrDefault(),
                    ParkedVehicleId = p.Parking.Select(p => p.ParkedVehicle.ID).FirstOrDefault()
                })
                .ToListAsync();

            return View(model);
        }


        // Check if there are parking spaces available
        // If more than one, they have to be adjacent to each other
        private bool CheckParkingSpaces(int numberOfSpaces)
        {
            var availableSpaces = _context.ParkingSpace
                .Where(s => s.Available == true)
                .OrderBy(s => s.ParkingSpaceNum)
                .ToList();

            var adjacentFound = false;
            // List contains free parking spaces sorted on ParkingSpace number.
            for (var i = 0; i < availableSpaces.Count - numberOfSpaces + 1; i++)
            {
                if (availableSpaces[i + numberOfSpaces - 1].ParkingSpaceNum == availableSpaces[i].ParkingSpaceNum + numberOfSpaces - 1)
                {
                    // availableSpaces[i] and the following "numberOfSpaces - 1" spaces are available
                    adjacentFound = true;
                    break;
                }
            }
            return adjacentFound;
        }
    }
}
