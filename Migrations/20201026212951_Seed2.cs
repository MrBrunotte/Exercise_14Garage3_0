using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage3.Migrations
{
    public partial class Seed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkingSpace",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Available = table.Column<bool>(nullable: false),
                    ParkingSpaceNum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpace", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Parking",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleTypeID = table.Column<int>(nullable: false),
                    ParkingSpaceID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parking", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Parking_ParkingSpace_ParkingSpaceID",
                        column: x => x.ParkingSpaceID,
                        principalTable: "ParkingSpace",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parking_VehicleTypes_VehicleTypeID",
                        column: x => x.VehicleTypeID,
                        principalTable: "VehicleTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ParkingSpace",
                columns: new[] { "ID", "Available", "ParkingSpaceNum" },
                values: new object[] { 1, false, 1 });

            migrationBuilder.InsertData(
                table: "ParkingSpace",
                columns: new[] { "ID", "Available", "ParkingSpaceNum" },
                values: new object[] { 2, true, 2 });

            migrationBuilder.InsertData(
                table: "ParkingSpace",
                columns: new[] { "ID", "Available", "ParkingSpaceNum" },
                values: new object[] { 3, true, 3 });

            migrationBuilder.InsertData(
                table: "Parking",
                columns: new[] { "ID", "ParkingSpaceID", "VehicleTypeID" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Parking_ParkingSpaceID",
                table: "Parking",
                column: "ParkingSpaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Parking_VehicleTypeID",
                table: "Parking",
                column: "VehicleTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parking");

            migrationBuilder.DropTable(
                name: "ParkingSpace");
        }
    }
}
