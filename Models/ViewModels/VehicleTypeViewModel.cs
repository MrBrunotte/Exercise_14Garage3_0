using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Garage3.Models.ViewModels
{
    // Added by Stefan for the search function
    public class VehicleTypeViewModel
    {
        public IEnumerable<ParkedVehicle> VehicleList { get; set; }
        public IEnumerable<SelectListItem> VehicleTypes { get; set; }
        public int VehicleTypeID { get; set; }
        public VehicleTypes VehicleType { get; set; }
        public string SearchString { get; set; }
    }
}
