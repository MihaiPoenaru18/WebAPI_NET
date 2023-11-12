using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RelationOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_News_IdUserNewsLetter",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_IdUserNewsLetter",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_News",
                table: "News");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "News",
                newName: "Newsletters");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Newsletters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Newsletters",
                table: "Newsletters",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdUserNewsLetter",
                table: "Users",
                column: "IdUserNewsLetter",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Newsletters_IdUserNewsLetter",
                table: "Users",
                column: "IdUserNewsLetter",
                principalTable: "Newsletters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Newsletters_IdUserNewsLetter",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_IdUserNewsLetter",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Newsletters",
                table: "Newsletters");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Newsletters",
                newName: "News");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "News",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_News",
                table: "News",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdUserNewsLetter",
                table: "User",
                column: "IdUserNewsLetter");

            migrationBuilder.AddForeignKey(
                name: "FK_User_News_IdUserNewsLetter",
                table: "User",
                column: "IdUserNewsLetter",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
