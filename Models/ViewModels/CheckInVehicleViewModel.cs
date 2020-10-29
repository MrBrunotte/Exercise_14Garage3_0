using Garage3.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class CheckInVehicleViewModel
    {
        
        public int VehicleTypeID { get; set; }

        public int MemberID { get; set; }

        [Required, StringLength(8, ErrorMessage = "Registration number can only be 8 characters long"), Display(Name = "Registration number")]
        //[Remote("ValidateRegNum", "Validator", ErrorMessage = "Please enter a valid REGISTRATION NUMBER.")]
        public string RegNum { get; set; }

        [Required, StringLength(30, ErrorMessage = "Do not enter more than 30 characters"), Display(Name = "Vehicle Color")]
        public string Color { get; set; }

        [Required, StringLength(30, ErrorMessage = "Do not enter more than 30 characters"), Display(Name = "Make of Vehicle")]
        public string Make { get; set; }

        [Required, StringLength(30, ErrorMessage = "Do not enter more than 30 characters"), Display(Name = "Model of Vehicle")]
        public string Model { get; set; }

        public DateTime ArrivalTime { get; set; }

    }
}
