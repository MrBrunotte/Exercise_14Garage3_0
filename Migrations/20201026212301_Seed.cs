using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage3.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "Id", "ConfirmPassword", "Email", "FirstName", "LastName", "Password", "PhoneNum" },
                values: new object[] { 1, "plugga", "kalle.kula@hotail.com", "Kalle", "Kula", "plugga", "070234567" });

            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "Id", "ConfirmPassword", "Email", "FirstName", "LastName", "Password", "PhoneNum" },
                values: new object[] { 2, "plugga2", "and.and@hotail.com", "Andreas", "Andersson", "plugga2", "070234568" });

            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "Id", "ConfirmPassword", "Email", "FirstName", "LastName", "Password", "PhoneNum" },
                values: new object[] { 3, "plugga3", "zlatan@hotail.com", "Zlatan", "Ibrahimovic", "plugga3", "070234569" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
