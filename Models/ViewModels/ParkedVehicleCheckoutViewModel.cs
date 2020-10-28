using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class ParkedVehicleCheckoutViewModel
    {
        public int ID { get; set; }

        public Member Member { get; set; }

        [Display(Name = "Registration number")]
        public string RegNum { get; set; }

        [Display(Name = "Time of Arrival")]
        public DateTime ArrivalTime { get; set; }

        [Display(Name = "Prel. Checkout")]
        public DateTime CheckOutTime { get; set; }

        [Display(Name = "Parking Time")]
        [DisplayFormat(DataFormatString = "{0:%d}d {0:%h}h {0:%m}m")]
        public TimeSpan Period { get; set; }

        [Display(Name = "Cost per minute")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double CostPerMinute { get; set; }

        [Display(Name = "Preliminary cost")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Cost { get; set; }
    }
}
