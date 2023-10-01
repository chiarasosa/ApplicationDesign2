using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Obligatorio1.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Correccion_Cart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Cart",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "porcentaje",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "porcentaje",
                table: "Cart");
        }
    }
}
