using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Obligatorio1.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cart_CartID",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "CartID",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cart_CartID",
                table: "Users",
                column: "CartID",
                principalTable: "Cart",
                principalColumn: "CartID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cart_CartID",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "CartID",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cart_CartID",
                table: "Users",
                column: "CartID",
                principalTable: "Cart",
                principalColumn: "CartID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
