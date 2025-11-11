using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NOTE.Solutions.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Category",
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
                name: "IX_Category_BranchId",
                table: "Category",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Branches_BranchId",
                table: "Category",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Branches_BranchId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_BranchId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Category");

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
        }
    }
}
