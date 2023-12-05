using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Address_AndressId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "AndressId",
                table: "Order",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_AndressId",
                table: "Order",
                newName: "IX_Order_AddressId");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductsId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Streed",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductsId",
                table: "Products",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Address_AddressId",
                table: "Order",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Order_ProductsId",
                table: "Products",
                column: "ProductsId",
                principalTable: "Order",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Address_AddressId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Order_ProductsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Streed",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Order",
                newName: "AndressId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_AddressId",
                table: "Order",
                newName: "IX_Order_AndressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Address_AndressId",
                table: "Order",
                column: "AndressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
