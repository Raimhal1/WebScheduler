using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebScheluder.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Events",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        EventName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        StartEventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        EndEventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Events", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Roles",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Roles", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        RoleId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Users", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Users_Roles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "Roles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EventUser",
            //    columns: table => new
            //    {
            //        DayEventsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EventUser", x => new { x.DayEventsId, x.UsersId });
            //        table.ForeignKey(
            //            name: "FK_EventUser_Events_DayEventsId",
            //            column: x => x.DayEventsId,
            //            principalTable: "Events",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_EventUser_Users_UsersId",
            //            column: x => x.UsersId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.InsertData(
            //    table: "Roles",
            //    columns: new[] { "Id", "Name" },
            //    values: new object[] { 1, "admin" });

            //migrationBuilder.InsertData(
            //    table: "Roles",
            //    columns: new[] { "Id", "Name" },
            //    values: new object[] { 2, "user" });

            //migrationBuilder.InsertData(
            //    table: "Users",
            //    columns: new[] { "Id", "Email", "Password", "RoleId", "UserName" },
            //    values: new object[] { new Guid("35b9f462-9f75-4663-b42f-466316d2c990"), "admin@gmail.com", "admin", 1, "Admin" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_EventUser_UsersId",
            //    table: "EventUser",
            //    column: "UsersId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Users_RoleId",
            //    table: "Users",
            //    column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventUser");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
