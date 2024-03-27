using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BistroWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Carousel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Carousels",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageSrc",
                value: "/img/carousel/Bistro_1.jpg");

            migrationBuilder.UpdateData(
                table: "Carousels",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageSrc",
                value: "/img/carousel/Bistro_2.jpg");

            migrationBuilder.UpdateData(
                table: "Carousels",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageSrc",
                value: "/img/carousel/Bistro_3.jpg");

            migrationBuilder.InsertData(
                table: "Carousels",
                columns: new[] { "Id", "ImageAlt", "ImageSrc" },
                values: new object[] { 4, "Third slide", "/img/carousel/Bistro_4.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carousels",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Carousels",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageSrc",
                value: "/img/carousel/information-technology-specialist.jpg");

            migrationBuilder.UpdateData(
                table: "Carousels",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageSrc",
                value: "/img/carousel/Information-Technology-1-1.jpg");

            migrationBuilder.UpdateData(
                table: "Carousels",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageSrc",
                value: "/img/carousel/itec-index-banner.jpg");
        }
    }
}
