using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVault.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MenuItemRoleUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "AspNetRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                table: "AspNetRoles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_CreatedById",
                table: "AspNetRoles",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_UpdatedById",
                table: "AspNetRoles",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_CreatedById",
                table: "AspNetRoles",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_UpdatedById",
                table: "AspNetRoles",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_CreatedById",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_UpdatedById",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_CreatedById",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_UpdatedById",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "AspNetRoles");
        }
    }
}
