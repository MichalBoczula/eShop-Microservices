using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products.Persistance.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ImgName", "IntegrationId", "Name", "Price" },
                values: new object[] { 1, "Huawei", new Guid("55ccee28-e15d-4644-a7be-2f8a93568d6f"), "Chinese", 2000 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ImgName", "IntegrationId", "Name", "Price" },
                values: new object[] { 2, "Samsung", new Guid("0ef1268e-33d6-49cd-a4b5-8eb94494d896"), "Samsung", 3000 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ImgName", "IntegrationId", "Name", "Price" },
                values: new object[] { 3, "Iphone", new Guid("23363aff-dd71-4f3c-8381-f7e71021761e"), "Iphone", 4000 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
