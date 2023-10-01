using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Obligatorio1.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangePromoAppliedNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Purchase_PurchaseID",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_Users_UserID",
                table: "Purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cart_CartID",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Cart",
                newName: "Carts");

            migrationBuilder.RenameIndex(
                name: "IX_Product_PurchaseID",
                table: "Products",
                newName: "IX_Products_PurchaseID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Purchase",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PromoApplied",
                table: "Purchase",
                type: "nvarchar(max)",
                nullable: true, 
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: false 
            );


            migrationBuilder.AddColumn<int>(
                name: "CartID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Carts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CartID",
                table: "Products",
                column: "CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Carts_CartID",
                table: "Products",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Purchase_PurchaseID",
                table: "Products",
                column: "PurchaseID",
                principalTable: "Purchase",
                principalColumn: "PurchaseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_Users_UserID",
                table: "Purchase",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Carts_CartID",
                table: "Users",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "CartID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Carts_CartID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Purchase_PurchaseID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_Users_UserID",
                table: "Purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Carts_CartID",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CartID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CartID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Carts");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "Cart");

            migrationBuilder.RenameIndex(
                name: "IX_Products_PurchaseID",
                table: "Product",
                newName: "IX_Product_PurchaseID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Purchase",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                column: "CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Purchase_PurchaseID",
                table: "Product",
                column: "PurchaseID",
                principalTable: "Purchase",
                principalColumn: "PurchaseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_Users_UserID",
                table: "Purchase",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

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
