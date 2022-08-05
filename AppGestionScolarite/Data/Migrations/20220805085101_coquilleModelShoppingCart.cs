using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppGestionScolarite.Data.Migrations
{
    public partial class coquilleModelShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_ShoppingCarts_ShoppingCartId",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_ShoppingCartId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Modules");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ShoppingCartId",
                table: "CartItems",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ShoppingCarts_ShoppingCartId",
                table: "CartItems",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ShoppingCarts_ShoppingCartId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ShoppingCartId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "Modules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_ShoppingCartId",
                table: "Modules",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_ShoppingCarts_ShoppingCartId",
                table: "Modules",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");
        }
    }
}
