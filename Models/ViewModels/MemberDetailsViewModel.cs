using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Garage3.Models.Entities;

namespace Garage3.Models.ViewModels
{
    public class MemberDetailsViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Type of Vehicle")]
        public VehicleTypes VehicleType { get; set; }
        public Member Member { get; set; }

        [Display(Name = "Make of Vehicle")]
        public string Make { get; set; }

        [Display(Name = "Model of Vehicle")]
        public string Model { get; set; }

        [Display(Name = "Registration number")]
        public string RegNum { get; set; }

        public ICollection<ParkedVehicle> MemberVehicles { get; set; }
    }
}
