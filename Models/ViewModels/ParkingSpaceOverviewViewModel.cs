using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class ParkingSpaceOverviewViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Parking space number")]
        public int ParkingSpaceNum { get; set; }
        public bool Available { get; set; }
        [Display(Name = "Registration number")]
        public string RegNum { get; set; }
        public int? ParkedVehicleId { get; set; }
    }
}

