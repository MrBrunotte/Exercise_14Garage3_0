using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class OverViewViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Type of Vehicle")]
        public VehicleTypes VehicleType { get; set; }

        [Display(Name = "Registration number")]
        public string RegNum { get; set; }

        [Display(Name = "Arrived")]
        public DateTime ArrivalTime { get; set; }

        [Display(Name = "Parked Time")]
        [DisplayFormat(DataFormatString = "{0:%d}d {0:%h}h {0:%m}m")]
        public TimeSpan Period { get; set; }
    }
}
