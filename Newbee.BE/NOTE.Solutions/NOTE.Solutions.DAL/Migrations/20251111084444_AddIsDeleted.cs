using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NOTE.Solutions.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Category",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Category",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Category",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMTAnoY81nOXAJPVcFAoACRgG3U9Ltt+6cRgrjIf7BjtN6G2ZWcK7PlRQz1eUZra3Q==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENswf+PqMIfbfrkwPnbYjHPDT4tg71nXiuqW+BQvS+L5zfkhM2rphC9sBl/n2YKZNQ==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENZqnNi89REogIZRhHlQZuY/DC20WdYz1uT/bKcLhJ4sB/u65M3EnnG4UPtGhRUtYw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEPA2XesDGqI6EpnTkmjYLGGzN70AQfupjemUQK12iaMjgeSwNKqIs2RdJj9Dzu1q4g==");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CreatedById",
                table: "Category",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Category_UpdatedById",
                table: "Category",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_AspNetUsers_CreatedById",
                table: "Category",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_AspNetUsers_UpdatedById",
                table: "Category",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_AspNetUsers_CreatedById",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_AspNetUsers_UpdatedById",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_CreatedById",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_UpdatedById",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Category");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEO1HTEoFCtJgo/f1r5Rv5cNxijhtnF97YM2E8eS/GSXaurbjoRxZ4R88H+PXRtHRzg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOhjyq+ffeIj6HMKij8G0UNUoswjkjcfD9KVqt/WhWuhwzOEyklAtfDdCGHVIK4wCw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAECjURvb89elG2HK5UNGzsuxGXrbwvLcNdCcIAfLr+MYelYz3XEd96G9tChp3U7SRLA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDQe4sQB1ZGqnH1kumhISFjKAG56q8U2Dgdu0ypuXhnWlGdMXKSFDADcev3TG5exnw==");
        }
    }
}
