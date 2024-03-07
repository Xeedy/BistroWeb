using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BistroWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeeId",
                table: "Tappeds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Tappeds",
                keyColumn: "Id",
                keyValue: 1,
                column: "TypeeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Tappeds",
                keyColumn: "Id",
                keyValue: 2,
                column: "TypeeId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Tappeds_TypeeId",
                table: "Tappeds",
                column: "TypeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tappeds_Typees_TypeeId",
                table: "Tappeds",
                column: "TypeeId",
                principalTable: "Typees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tappeds_Typees_TypeeId",
                table: "Tappeds");

            migrationBuilder.DropIndex(
                name: "IX_Tappeds_TypeeId",
                table: "Tappeds");

            migrationBuilder.DropColumn(
                name: "TypeeId",
                table: "Tappeds");
        }
    }
}
