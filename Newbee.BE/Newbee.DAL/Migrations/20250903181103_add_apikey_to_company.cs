using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Newbee.DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_apikey_to_company : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Districts_DistrictId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_DistrictId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Customers");

            migrationBuilder.AddColumn<Guid>(
                name: "ApiKey",
                table: "Companies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "CustomerDistrict",
                columns: table => new
                {
                    CustomersId = table.Column<int>(type: "int", nullable: false),
                    DistrictsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDistrict", x => new { x.CustomersId, x.DistrictsId });
                    table.ForeignKey(
                        name: "FK_CustomerDistrict_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerDistrict_Districts_DistrictsId",
                        column: x => x.DistrictsId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDistrict_DistrictsId",
                table: "CustomerDistrict",
                column: "DistrictsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerDistrict");

            migrationBuilder.DropColumn(
                name: "ApiKey",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DistrictId",
                table: "Customers",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Districts_DistrictId",
                table: "Customers",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
