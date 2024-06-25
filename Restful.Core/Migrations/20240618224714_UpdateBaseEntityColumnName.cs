using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restful.Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBaseEntityColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Users",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Results",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Requests",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Passwords",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Parameters",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Headers",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Collections",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Users",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Results",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Requests",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Passwords",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Parameters",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Headers",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Collections",
                newName: "Notes");
        }
    }
}
