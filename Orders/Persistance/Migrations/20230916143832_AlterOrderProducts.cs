using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.Persistance.Migrations
{
    public partial class AlterOrderProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "OrderProducts",
                newName: "Price");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "IntegrationId",
                value: new Guid("17419f95-733e-4c34-99f5-8ae66d6f50ef"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderProducts",
                newName: "Total");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "IntegrationId",
                value: new Guid("76e33392-3fa9-44f4-93e0-cf09c299c7ca"));
        }
    }
}
