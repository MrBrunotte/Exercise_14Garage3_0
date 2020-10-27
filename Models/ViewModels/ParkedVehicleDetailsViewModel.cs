using Garage3.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class ParkedVehicleDetailsViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Vehicle Type")]
        public VehicleTypes VehicleType { get; set; }
        public Member Member { get; set; }
       
        [Display(Name = "Registration number")]
        public string RegNum { get; set; }

        [ Display(Name = "Vehicle Color")]
        public string Color { get; set; }

        [ Display(Name = "Make of Vehicle")]
        public string Make { get; set; }

        [ Display(Name = "Model of Vehicle")]
        public string Model { get; set; }

        [Display(Name = "Time of Arrival")]
        public DateTime ArrivalTime { get; set; }

        [Display(Name = "Parking Time")]
        [DisplayFormat(DataFormatString = "{0:%d}d {0:%h}h {0:%m}m")]
        public TimeSpan Period { get; set; }
        
        [Display(Name = "Parking Space(s)")]
        public ICollection<ParkingSpace> ParkingSpaces { get; set; }
    }
}
