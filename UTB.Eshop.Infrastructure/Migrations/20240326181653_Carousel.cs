using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BistroWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Carousel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Carousel",
                table: "Carousel");

            migrationBuilder.RenameTable(
                name: "Carousel",
                newName: "Carousels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carousels",
                table: "Carousels",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Carousels",
                table: "Carousels");

            migrationBuilder.RenameTable(
                name: "Carousels",
                newName: "Carousel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carousel",
                table: "Carousel",
                column: "Id");
        }
    }
}
