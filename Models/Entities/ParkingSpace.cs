using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.Entities
{
    public class ParkingSpace
    {
        public int ID { get; set; }

        public bool Available { get; set; }

        [Required, Display(Name = "Parkingspace number")]
        public int ParkingSpaceNum { get; set; }

        // Navigation property
        public ICollection<Parking> Parking { get; set; }
    }
}
