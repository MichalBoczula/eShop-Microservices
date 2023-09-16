using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopingCarts.Persistance.Migrations
{
    public partial class AlterShoppingCartProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "ShoppingCartProducts",
                newName: "Price");

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "IntegrationId",
                value: new Guid("d0e9939d-3b47-4931-9ba0-db076f6c5278"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "ShoppingCartProducts",
                newName: "Total");

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "IntegrationId",
                value: new Guid("238d6d50-a1df-4fff-831e-5a919841483e"));
        }
    }
}
