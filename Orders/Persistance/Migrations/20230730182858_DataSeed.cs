using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.Persistance.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IntegrationId" },
                values: new object[] { 1, new Guid("95464765-cf3f-4ed7-b353-5d2f810dcc33") });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "IntegrationId", "Total", "UserId" },
                values: new object[] { 1, new Guid("76e33392-3fa9-44f4-93e0-cf09c299c7ca"), 2000, 1 });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "Id", "OrderId", "ProductIntegrationId", "Quantity", "Total" },
                values: new object[] { 1, 1, new Guid("55ccee28-e15d-4644-a7be-2f8a93568d6f"), 1, 2000 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
