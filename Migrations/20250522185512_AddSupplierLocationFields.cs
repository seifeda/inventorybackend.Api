using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inventorybackend.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplierLocationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Suppliers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Suppliers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Suppliers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 22, 18, 55, 12, 399, DateTimeKind.Utc).AddTicks(8251), "$2a$11$mCfa0i2hp7ecrRSOLf7QlOq6EdyiH7AgjNjwKxHNzcORLylk8hijm" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Suppliers");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 22, 8, 58, 40, 297, DateTimeKind.Utc).AddTicks(6908), "$2a$11$TNFG38TuQQhxEDHKARrKP.xAWzixce0kXOVIURst6H924Dx7UbI3a" });
        }
    }
}
