using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KampüsSepeti.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class userimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "AspNetUsers");

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
    }
}
