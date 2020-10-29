using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage3.Migrations
{
    public partial class Members : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_Member_MemberID",
                table: "ParkedVehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Member",
                table: "Member");

            migrationBuilder.RenameTable(
                name: "Member",
                newName: "Members");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkedVehicle_Members_MemberID",
                table: "ParkedVehicle",
                column: "MemberID",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_Members_MemberID",
                table: "ParkedVehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "Member");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Member",
                table: "Member",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkedVehicle_Member_MemberID",
                table: "ParkedVehicle",
                column: "MemberID",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
