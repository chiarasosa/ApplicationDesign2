using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Obligatorio1.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ConfigMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Carts_CartID",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Purchase_PurchaseID",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_Users_UserID",
                table: "Purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Carts_CartID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CartID",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchase",
                table: "Purchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CartID",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Purchase",
                newName: "Purchases");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Sessions",
                newName: "SessionID");

            migrationBuilder.RenameIndex(
                name: "IX_Purchase_UserID",
                table: "Purchases",
                newName: "IX_Purchases_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Product_PurchaseID",
                table: "Products",
                newName: "IX_Products_PurchaseID");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CartID",
                table: "Products",
                newName: "IX_Products_CartID");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseID",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                column: "PurchaseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserID",
                table: "Carts",
                column: "UserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserID",
                table: "Carts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Carts_CartID",
                table: "Products",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Purchases_PurchaseID",
                table: "Products",
                column: "PurchaseID",
                principalTable: "Purchases",
                principalColumn: "PurchaseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Users_UserID",
                table: "Purchases",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UserID",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Carts_CartID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Purchases_PurchaseID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Users_UserID",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserID",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Carts");

            migrationBuilder.RenameTable(
                name: "Purchases",
                newName: "Purchase");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameColumn(
                name: "SessionID",
                table: "Sessions",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_UserID",
                table: "Purchase",
                newName: "IX_Purchase_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_PurchaseID",
                table: "Product",
                newName: "IX_Product_PurchaseID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CartID",
                table: "Product",
                newName: "IX_Product_CartID");

            migrationBuilder.AddColumn<int>(
                name: "CartID",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseID",
                table: "Product",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchase",
                table: "Purchase",
                column: "PurchaseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CartID",
                table: "Users",
                column: "CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Carts_CartID",
                table: "Product",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "CartID");

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
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Carts_CartID",
                table: "Users",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "CartID");
        }
    }
}
