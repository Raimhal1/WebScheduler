using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebScheluder.DAL.Migrations
{
    public partial class added_dtos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventUser_Events_DayEventsId",
                table: "EventUser");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("757342dc-ef82-42c2-aafc-cf50c4b1095c"));

            migrationBuilder.RenameColumn(
                name: "DayEventsId",
                table: "EventUser",
                newName: "EventsId");

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Events",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventName",
                table: "Events",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "RoleId", "UserName" },
                values: new object[] { new Guid("3cbfb4e2-dc88-4915-9981-21aacf313a25"), "admin@gmail.com", "admin", 1, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Events_Id",
                table: "Events",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EventUser_Events_EventsId",
                table: "EventUser",
                column: "EventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventUser_Events_EventsId",
                table: "EventUser");

            migrationBuilder.DropIndex(
                name: "IX_Events_Id",
                table: "Events");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3cbfb4e2-dc88-4915-9981-21aacf313a25"));

            migrationBuilder.RenameColumn(
                name: "EventsId",
                table: "EventUser",
                newName: "DayEventsId");

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventName",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "RoleId", "UserName" },
                values: new object[] { new Guid("757342dc-ef82-42c2-aafc-cf50c4b1095c"), "admin@gmail.com", "admin", 1, "Admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_EventUser_Events_DayEventsId",
                table: "EventUser",
                column: "DayEventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
