using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BistroWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carousels",
                keyColumn: "Id",
                keyValue: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Carousels",
                columns: new[] { "Id", "ImageAlt", "ImageSrc" },
                values: new object[] { 4, "Third slide", "/img/carousel/Bistro_4.jpg" });
        }
    }
}
