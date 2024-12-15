using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVault.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedNotificationText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NotificationText",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationText",
                table: "Notifications");
        }
    }
}
