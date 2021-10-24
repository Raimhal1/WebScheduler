using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebScheluder.DAL.Migrations
{
    public partial class added_column_FirstName_and_LastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3cbfb4e2-dc88-4915-9981-21aacf313a25"));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "RoleId", "UserName" },
                values: new object[] { new Guid("a78bb934-e57e-4446-aa44-fdd7b77dd494"), "admin@gmail.com", null, null, "admin", 1, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a78bb934-e57e-4446-aa44-fdd7b77dd494"));

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "RoleId", "UserName" },
                values: new object[] { new Guid("3cbfb4e2-dc88-4915-9981-21aacf313a25"), "admin@gmail.com", "admin", 1, "Admin" });
        }
    }
}
