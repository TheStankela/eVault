using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVault.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MenuItemRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuItemUserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemUserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItemUserRoles_AspNetRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuItemUserRoles_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemUserRoles_MenuItemId",
                table: "MenuItemUserRoles",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemUserRoles_UserRoleId",
                table: "MenuItemUserRoles",
                column: "UserRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItemUserRoles");
        }
    }
}
