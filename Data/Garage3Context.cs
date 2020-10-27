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

        public DbSet<Garage3.Models.ParkedVehicle> ParkedVehicle { get; set; }

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
                    PhoneNum = "070234567",
                    Password = "plugga",
                    ConfirmPassword = "plugga"

                },
                new Member
                {
                    Id = 2,
                    FirstName = "Andreas",
                    LastName = "Andersson",
                    Email = "and.and@hotail.com",
                    PhoneNum = "070234568",
                    Password = "plugga2",
                    ConfirmPassword = "plugga2"
                },
                new Member
                {
                    Id = 3,
                    FirstName = "Zlatan",
                    LastName = "Ibrahimovic",
                    Email = "zlatan@hotail.com",
                    PhoneNum = "070234569",
                    Password = "plugga3",
                    ConfirmPassword = "plugga3"
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

                 }
               // AddParkingSpaces()

               );

            modelbuilder.Entity<Parking>()
             .HasData(

               new Parking
               {
                   ID = 1,
                   ParkedVehickeID = 1,
                   ParkingSpaceID = 1

               }

              );
        }

        //private ParkingSpace[] AddParkingSpaces()
        //{
        //    ParkingSpace p = new ParkingSpace();
        //    for (int i = 0; i < 100; i++)
        //    {
        //        new ParkingSpace
        //        {
        //            ID = i,
        //            Available = true,
        //            ParkingSpaceNum = i

        //        };


        //    }
        //    return ;
        //}
    }
}
