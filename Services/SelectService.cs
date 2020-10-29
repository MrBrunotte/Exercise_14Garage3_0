using Garage3.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Services
{
    public class SelectService : ISelectService
    {
        private readonly Garage3Context _context;

        public SelectService(Garage3Context _context)
        {
            this._context = _context;
        }

        public async Task<IEnumerable<SelectListItem>> GetTypeAsync()
        {
            return await _context.VehicleTypes
                         .Select(m => new SelectListItem
                         {
                             Text = m.VehicleType,
                             Value = m.ID.ToString()
                         })
                         .ToListAsync();
        }
        public async Task<IEnumerable<SelectListItem>> GetMemberAsync()
        {
            return await _context.Members
                         .Select(m => new SelectListItem
                         {
                             Text = m.FullName,
                             Value = m.Id.ToString()
                         })
                         .ToListAsync();
        }
    }
}