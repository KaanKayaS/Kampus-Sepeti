using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KampüsSepeti.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_imagecanbenull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 30, 16, 24, 33, 694, DateTimeKind.Local).AddTicks(5998));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 30, 16, 24, 33, 694, DateTimeKind.Local).AddTicks(5999));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 30, 16, 24, 33, 694, DateTimeKind.Local).AddTicks(6001));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 30, 16, 24, 33, 694, DateTimeKind.Local).AddTicks(6002));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 30, 16, 24, 33, 694, DateTimeKind.Local).AddTicks(7407));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 30, 16, 24, 33, 694, DateTimeKind.Local).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 30, 16, 24, 33, 694, DateTimeKind.Local).AddTicks(7410));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 30, 16, 24, 33, 694, DateTimeKind.Local).AddTicks(7411));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 30, 16, 24, 33, 694, DateTimeKind.Local).AddTicks(7413));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 30, 16, 24, 33, 694, DateTimeKind.Local).AddTicks(8418));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 30, 16, 24, 33, 694, DateTimeKind.Local).AddTicks(8419));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 30, 16, 24, 33, 694, DateTimeKind.Local).AddTicks(8421));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 30, 16, 24, 33, 694, DateTimeKind.Local).AddTicks(8422));

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2025, 4, 30, 16, 24, 33, 697, DateTimeKind.Local).AddTicks(5048), "Movies & Clothing" });

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2025, 4, 30, 16, 24, 33, 697, DateTimeKind.Local).AddTicks(5178), "Beauty & Music" });

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2025, 4, 30, 16, 24, 33, 697, DateTimeKind.Local).AddTicks(5186), "Tools" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 21, 2, 37, 609, DateTimeKind.Local).AddTicks(875));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 21, 2, 37, 609, DateTimeKind.Local).AddTicks(877));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 21, 2, 37, 609, DateTimeKind.Local).AddTicks(878));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 21, 2, 37, 609, DateTimeKind.Local).AddTicks(879));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 21, 2, 37, 609, DateTimeKind.Local).AddTicks(2236));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 21, 2, 37, 609, DateTimeKind.Local).AddTicks(2237));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 21, 2, 37, 609, DateTimeKind.Local).AddTicks(2239));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 21, 2, 37, 609, DateTimeKind.Local).AddTicks(2240));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 21, 2, 37, 609, DateTimeKind.Local).AddTicks(2241));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 21, 2, 37, 609, DateTimeKind.Local).AddTicks(3212));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 21, 2, 37, 609, DateTimeKind.Local).AddTicks(3214));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 21, 2, 37, 609, DateTimeKind.Local).AddTicks(3215));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 21, 2, 37, 609, DateTimeKind.Local).AddTicks(3216));

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2025, 4, 29, 21, 2, 37, 611, DateTimeKind.Local).AddTicks(6358), "Industrial, Grocery & Tools" });

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2025, 4, 29, 21, 2, 37, 611, DateTimeKind.Local).AddTicks(6379), "Grocery, Health & Industrial" });

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2025, 4, 29, 21, 2, 37, 611, DateTimeKind.Local).AddTicks(6392), "Outdoors, Music & Health" });
        }
    }
}
