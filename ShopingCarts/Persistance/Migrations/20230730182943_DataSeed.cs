using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopingCarts.Persistance.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IntegrationId", "ShoppingCartId" },
                values: new object[] { 1, new Guid("95464765-cf3f-4ed7-b353-5d2f810dcc33"), 1 });

            migrationBuilder.InsertData(
                table: "ShoppingCarts",
                columns: new[] { "Id", "IntegrationId", "Total", "UserId" },
                values: new object[] { 1, new Guid("238d6d50-a1df-4fff-831e-5a919841483e"), 3000, 1 });

            migrationBuilder.InsertData(
                table: "ShoppingCartProducts",
                columns: new[] { "Id", "ProductIntegrationId", "Quantity", "ShoppingCartId", "Total" },
                values: new object[] { 1, new Guid("0ef1268e-33d6-49cd-a4b5-8eb94494d896"), 1, 1, 3000 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShoppingCartProducts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
