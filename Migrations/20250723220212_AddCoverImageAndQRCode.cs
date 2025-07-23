using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookCatalogApp.Migrations
{
    /// <inheritdoc />
    public partial class AddCoverImageAndQRCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImageFileName",
                table: "Items",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "Items",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QRCode",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CoverImageFileName", "CoverImageUrl", "QRCode" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CoverImageFileName", "CoverImageUrl", "QRCode" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CoverImageFileName", "CoverImageUrl", "QRCode" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CoverImageFileName", "CoverImageUrl", "QRCode" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CoverImageFileName", "CoverImageUrl", "QRCode" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CoverImageFileName", "CoverImageUrl", "QRCode" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CoverImageFileName", "CoverImageUrl", "QRCode" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CoverImageFileName", "CoverImageUrl", "QRCode" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CoverImageFileName", "CoverImageUrl", "QRCode" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CoverImageFileName", "CoverImageUrl", "QRCode" },
                values: new object[] { null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageFileName",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "QRCode",
                table: "Items");
        }
    }
}
