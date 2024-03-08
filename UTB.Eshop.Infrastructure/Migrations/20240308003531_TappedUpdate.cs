using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BistroWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TappedUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Tappeds",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Tappeds",
                keyColumn: "Id",
                keyValue: 1,
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "Tappeds",
                keyColumn: "Id",
                keyValue: 2,
                column: "Active",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Tappeds");
        }
    }
}
