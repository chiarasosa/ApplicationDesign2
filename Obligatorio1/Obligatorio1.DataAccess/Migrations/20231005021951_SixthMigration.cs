using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Obligatorio1.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Sixth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Purchase_PurchaseID",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_Product_PurchaseID",
                table: "Products",
                newName: "IX_Products_PurchaseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Purchase_PurchaseID",
                table: "Products",
                column: "PurchaseID",
                principalTable: "Purchase",
                principalColumn: "PurchaseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Purchase_PurchaseID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameIndex(
                name: "IX_Products_PurchaseID",
                table: "Product",
                newName: "IX_Product_PurchaseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Purchase_PurchaseID",
                table: "Product",
                column: "PurchaseID",
                principalTable: "Purchase",
                principalColumn: "PurchaseID");
        }
    }
}
