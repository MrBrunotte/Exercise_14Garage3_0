using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models
{
    public class ParkedVehicle
    {
        public int ID { get; set; }

        public int VehicleTypeID { get; set; }
        public VehicleTypes VehicleType { get; set; }

        public int MemberID { get; set; }
        public Member Member { get; set; }


        [Required, StringLength(8, ErrorMessage = "Registration number can only be 8 characters long"), Display(Name = "Registration number")]
        public string RegNum { get; set; }

        [Required, StringLength(30, ErrorMessage = "Do not enter more than 30 characters"), Display(Name = "Vehicle Color")]
        public string Color { get; set; }

        [Required, StringLength(30, ErrorMessage = "Do not enter more than 30 characters"), Display(Name = "Make of Vehicle")]
        public string Make { get; set; }

        [Required, StringLength(30, ErrorMessage = "Do not enter more than 30 characters"), Display(Name = "Model of Vehicle")]
        public string Model { get; set; }

        [Display(Name = "Time of Arrival")]
        public DateTime ArrivalTime { get; set; }
    }
}
