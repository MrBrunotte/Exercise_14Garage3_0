using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage3.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 30, nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: false),
                    Email = table.Column<string>(maxLength: 60, nullable: false),
                    PhoneNum = table.Column<string>(maxLength: 30, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    ConfirmPassword = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleType = table.Column<string>(maxLength: 30, nullable: false),
                    FillsNumberOfSpaces = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ParkedVehicle",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleTypeID = table.Column<int>(nullable: false),
                    MemberID = table.Column<int>(nullable: false),
                    RegNum = table.Column<string>(maxLength: 8, nullable: false),
                    Color = table.Column<string>(maxLength: 30, nullable: false),
                    Make = table.Column<string>(maxLength: 30, nullable: false),
                    Model = table.Column<string>(maxLength: 30, nullable: false),
                    ArrivalTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkedVehicle", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ParkedVehicle_Member_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParkedVehicle_VehicleTypes_VehicleTypeID",
                        column: x => x.VehicleTypeID,
                        principalTable: "VehicleTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "VehicleTypes",
                columns: new[] { "ID", "FillsNumberOfSpaces", "VehicleType" },
                values: new object[,]
                {
                    { 1, 3, "Bus" },
                    { 2, 1, "Car" },
                    { 3, 1, "Sportscar" },
                    { 4, 1, "Motorcycle" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkedVehicle_MemberID",
                table: "ParkedVehicle",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_ParkedVehicle_VehicleTypeID",
                table: "ParkedVehicle",
                column: "VehicleTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkedVehicle");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "VehicleTypes");
        }
    }
}
