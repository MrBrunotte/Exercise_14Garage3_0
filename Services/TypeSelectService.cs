using Garage3.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Services
{
    public class TypeSelectService : ISelectService
    {
        private readonly Garage3Context _context;

        public TypeSelectService(Garage3Context _context)
        {
            this._context = _context;
        }

        public async Task<IEnumerable<SelectListItem>> TypeAsync()
        {
            return await _context.ParkedVehicle
                         .Select(m => m.VehicleType)
                         .Distinct()
                         .Select(m => new SelectListItem
                         {
                             Text = m.ToString(),
                             Value = m.ToString()
                         })
                         .ToListAsync();
        }
    }
}
