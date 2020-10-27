using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage3.Migrations
{
    public partial class Seed3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ParkedVehicle",
                columns: new[] { "ID", "ArrivalTime", "Color", "Make", "MemberID", "Model", "RegNum", "VehicleTypeID" },
                values: new object[] { 1, new DateTime(2020, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black", "Dodge", 1, "Nitro TR 4/4", "FZK678", 3 });

            migrationBuilder.InsertData(
                table: "ParkedVehicle",
                columns: new[] { "ID", "ArrivalTime", "Color", "Make", "MemberID", "Model", "RegNum", "VehicleTypeID" },
                values: new object[] { 2, new DateTime(2020, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black", "Camaro", 3, "SS", "FZK677", 3 });

            migrationBuilder.InsertData(
                table: "ParkedVehicle",
                columns: new[] { "ID", "ArrivalTime", "Color", "Make", "MemberID", "Model", "RegNum", "VehicleTypeID" },
                values: new object[] { 3, new DateTime(2020, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Orange", "Harley Davidson", 2, "NightRod", "MKT677", 4 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
