using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KampüsSepeti.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPhoneVisible",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 15, 55, 1, 289, DateTimeKind.Local).AddTicks(8402));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 15, 55, 1, 289, DateTimeKind.Local).AddTicks(8404));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 15, 55, 1, 289, DateTimeKind.Local).AddTicks(8405));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 15, 55, 1, 289, DateTimeKind.Local).AddTicks(8406));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 15, 55, 1, 289, DateTimeKind.Local).AddTicks(9855));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 15, 55, 1, 289, DateTimeKind.Local).AddTicks(9856));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 15, 55, 1, 289, DateTimeKind.Local).AddTicks(9858));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 15, 55, 1, 289, DateTimeKind.Local).AddTicks(9859));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 15, 55, 1, 289, DateTimeKind.Local).AddTicks(9860));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 15, 55, 1, 290, DateTimeKind.Local).AddTicks(885));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 15, 55, 1, 290, DateTimeKind.Local).AddTicks(886));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 15, 55, 1, 290, DateTimeKind.Local).AddTicks(887));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 29, 15, 55, 1, 290, DateTimeKind.Local).AddTicks(888));

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2025, 4, 29, 15, 55, 1, 292, DateTimeKind.Local).AddTicks(6734), "Outdoors, Jewelery & Jewelery" });

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2025, 4, 29, 15, 55, 1, 292, DateTimeKind.Local).AddTicks(6753), "Electronics, Grocery & Grocery" });

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2025, 4, 29, 15, 55, 1, 292, DateTimeKind.Local).AddTicks(6762), "Electronics & Shoes" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPhoneVisible",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 21, 21, 0, 17, 13, DateTimeKind.Local).AddTicks(1101));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 21, 21, 0, 17, 13, DateTimeKind.Local).AddTicks(1103));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 21, 21, 0, 17, 13, DateTimeKind.Local).AddTicks(1105));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 21, 21, 0, 17, 13, DateTimeKind.Local).AddTicks(1106));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 21, 21, 0, 17, 13, DateTimeKind.Local).AddTicks(2548));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 21, 21, 0, 17, 13, DateTimeKind.Local).AddTicks(2550));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 21, 21, 0, 17, 13, DateTimeKind.Local).AddTicks(2551));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 21, 21, 0, 17, 13, DateTimeKind.Local).AddTicks(2553));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 21, 21, 0, 17, 13, DateTimeKind.Local).AddTicks(2554));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 21, 21, 0, 17, 13, DateTimeKind.Local).AddTicks(3574));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 21, 21, 0, 17, 13, DateTimeKind.Local).AddTicks(3575));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 21, 21, 0, 17, 13, DateTimeKind.Local).AddTicks(3576));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 21, 21, 0, 17, 13, DateTimeKind.Local).AddTicks(3578));

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2025, 4, 21, 21, 0, 17, 15, DateTimeKind.Local).AddTicks(7167), "Clothing & Grocery" });

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2025, 4, 21, 21, 0, 17, 15, DateTimeKind.Local).AddTicks(7184), "Computers & Home" });

            migrationBuilder.UpdateData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2025, 4, 21, 21, 0, 17, 15, DateTimeKind.Local).AddTicks(7200), "Shoes, Computers & Electronics" });
        }
    }
}
