using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restful.Core.Migrations
{
    /// <inheritdoc />
    public partial class CollectionFkNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Collections_CollectionId",
                table: "Requests");

            migrationBuilder.AlterColumn<Guid>(
                name: "CollectionId",
                table: "Requests",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Collections_CollectionId",
                table: "Requests",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Collections_CollectionId",
                table: "Requests");

            migrationBuilder.AlterColumn<Guid>(
                name: "CollectionId",
                table: "Requests",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Collections_CollectionId",
                table: "Requests",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
