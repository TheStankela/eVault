using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVault.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MenuItemTabIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TabIndex",
                table: "MenuItems",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TabIndex",
                table: "MenuItems");
        }
    }
}
