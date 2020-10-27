using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace Garage3.Models.Entities
{
    public class Parking 
    {
        public int ID { get; set; }

        public int ParkedVehickeID { get; set; }
        public ParkedVehicle ParkedVehicle { get; set; }

        public int ParkingSpaceID { get; set; }
        public ParkingSpace ParkingSpace { get; set; }

    }
    
   
}
