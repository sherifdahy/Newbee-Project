using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NOTE.Solutions.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Branches_BranchId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BranchId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOI+meJfz05Z0Q0eBlC/GiAu4PAs38Wr2tl9Qah6DXhQCVtBIYM8MA/c/F1vWdicFQ==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELFYSOOF7sAThVgScpKl+EbQWDslrAUGkYJRSPhar4zcv1jCIG8ptvicITv3RfAwOQ==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMlu3k1WSfAUlzwYCbckqKMHjuRbIFPa3liJRgF9XuEMVxMBcG4GGf+kb3lPKxXF+g==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEwxb2QNQw6JwsXTtCRYzqW4Tynh9VS7JN/GBQCR8s825L0r98tLwljMS9eyrsfYtQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEGFweO5hacYvzG62yyslC/Xcq06vDXlAALK4CpguDWcv/UanOUIukCEZm7bOK2xAFw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDYnWC1Jz6N23/L8j/pAQbBcAs8HCvjOZk+gS5alaVkt3jAW3DYI7LB+lKtZqvpg4Q==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAQG0sQ9k8MqC91/+/a8ymPhXytkFxL6nX5rOGyTMRqyUK9+KtocMlfY1KVqDceLSw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDzNhDa01X5nxnYZfIthJdxrQIZ8vb0piaHHyhaoAnuiW6XgQ+VtGKrNL1xrx8TaCQ==");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BranchId",
                table: "Products",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Branches_BranchId",
                table: "Products",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
