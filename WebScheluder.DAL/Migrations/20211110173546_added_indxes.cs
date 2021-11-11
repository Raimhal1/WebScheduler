using Microsoft.EntityFrameworkCore.Migrations;

namespace WebScheluder.DAL.Migrations
{
    public partial class added_indxes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id_Email",
                table: "Users",
                columns: new[] { "Id", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Id",
                table: "Reports",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Id",
                table: "RefreshTokens",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventFiles_Id",
                table: "EventFiles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AllowedFileTypes_Id",
                table: "AllowedFileTypes",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Id_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Reports_Id",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_Id",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_EventFiles_Id",
                table: "EventFiles");

            migrationBuilder.DropIndex(
                name: "IX_AllowedFileTypes_Id",
                table: "AllowedFileTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
