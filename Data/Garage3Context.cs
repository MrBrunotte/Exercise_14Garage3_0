using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage3.Models;
using Garage3.Models.Entities;

namespace Garage3.Data
{
    public class Garage3Context : DbContext
    {
        public Garage3Context(DbContextOptions<Garage3Context> options)
            : base(options)
        {
        }

        public DbSet<ParkedVehicle> ParkedVehicle { get; set; }
        public DbSet<VehicleTypes> VehicleTypes { get; set; }
        public DbSet<Member> Members{ get; set; }
        public DbSet<ParkingSpace> ParkingSpace { get; set; }
        public DbSet<Parking> Parking { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {

            modelbuilder.Entity<Member>()
           .HasData(
                new Member
                {
                    Id = 1,
                    FirstName = "Kalle",
                    LastName = "Kula",
                    Email = "kalle.kula@hotail.com",
                    PhoneNum = "070234567"

                },
                new Member
                {
                    Id = 2,
                    FirstName = "Andreas",
                    LastName = "Andersson",
                    Email = "and.and@hotail.com",
                    PhoneNum = "070234568"
                },
                new Member
                {
                    Id = 3,
                    FirstName = "Zlatan",
                    LastName = "Ibrahimovic",
                    Email = "zlatan@hotail.com",
                    PhoneNum = "070234569"
                }
            );

            modelbuilder.Entity<VehicleTypes>()
               .HasData(
                new VehicleTypes
                {
                    ID = 1,
                    VehicleType = "Bus",
                    FillsNumberOfSpaces = 3
                },
                new VehicleTypes
                {
                    ID = 2,
                    VehicleType = "Car",
                    FillsNumberOfSpaces = 1
                },
                new VehicleTypes
                {
                    ID = 3,
                    VehicleType = "Sportscar",
                    FillsNumberOfSpaces = 1
                },
                new VehicleTypes
                {
                    ID = 4,
                    VehicleType = "Motorcycle",
                    FillsNumberOfSpaces = 1
                }
                );


            modelbuilder.Entity<ParkedVehicle>()
                .HasData(

                new ParkedVehicle
                {
                    ID = 1,
                    VehicleTypeID = 3,
                    MemberID = 1,
                    RegNum = "FZK678",
                    Color = "Black",
                    Make = "Dodge",
                    Model = "Nitro TR 4/4",
                    ArrivalTime = DateTime.Parse("2020-10-26")
                },
                new ParkedVehicle
                {
                    ID = 2,
                    VehicleTypeID = 3,
                    MemberID = 3,
                    RegNum = "FZK677",
                    Color = "Black",
                    Make = "Camaro",
                    Model = "SS",
                    ArrivalTime = DateTime.Parse("2020-10-26")

                },
                new ParkedVehicle
                {
                    ID = 3,
                    VehicleTypeID = 4,
                    MemberID = 2,
                    RegNum = "MKT677",
                    Color = "Orange",
                    Make = "Harley Davidson",
                    Model = "NightRod",
                    ArrivalTime = DateTime.Parse("2020-10-26")
                }

                );



            modelbuilder.Entity<ParkingSpace>()
              .HasData(

                new ParkingSpace
                {
                    ID = 1,
                    Available = false,
                    ParkingSpaceNum = 1

                },
                new ParkingSpace
                {
                    ID = 2,
                    Available = true,
                    ParkingSpaceNum = 2

                },
                 new ParkingSpace
                 {
                     ID = 3,
                     Available = true,
                     ParkingSpaceNum = 3

                 },
                 new ParkingSpace { ID = 4, Available = true, ParkingSpaceNum = 4 },
                 new ParkingSpace { ID = 5, Available = true, ParkingSpaceNum = 5 },
                 new ParkingSpace { ID = 6, Available = true, ParkingSpaceNum = 6 },
                 new ParkingSpace { ID = 7, Available = true, ParkingSpaceNum = 7 },
                 new ParkingSpace { ID = 8, Available = true, ParkingSpaceNum = 8 },
                 new ParkingSpace { ID = 9, Available = true, ParkingSpaceNum = 9 },
                 new ParkingSpace { ID = 10, Available = true, ParkingSpaceNum = 10 },
                 new ParkingSpace { ID = 11, Available = true, ParkingSpaceNum = 11 },
                 new ParkingSpace { ID = 12, Available = true, ParkingSpaceNum = 12 },
                 new ParkingSpace { ID = 13, Available = true, ParkingSpaceNum = 13 },
                 new ParkingSpace { ID = 14, Available = true, ParkingSpaceNum = 14 },
                 new ParkingSpace { ID = 15, Available = true, ParkingSpaceNum = 15 },
                 new ParkingSpace { ID = 16, Available = true, ParkingSpaceNum = 16 },
                 new ParkingSpace { ID = 17, Available = true, ParkingSpaceNum = 17 },
                 new ParkingSpace { ID = 18, Available = true, ParkingSpaceNum = 18 },
                 new ParkingSpace { ID = 19, Available = true, ParkingSpaceNum = 19 },
                 new ParkingSpace { ID = 20, Available = true, ParkingSpaceNum = 20 }

              // AddParkingSpaces()

               );

            modelbuilder.Entity<Parking>()
             .HasData(

               new Parking
               {
                   ID = 1,
                   ParkedVehicleID = 1,
                   ParkingSpaceID = 1

               }

              );
        }

    }
}
