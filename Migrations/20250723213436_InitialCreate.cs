using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookCatalogApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IconClass = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ColorClass = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MediaTypeId = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    BorrowerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BorrowDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_MediaTypes_MediaTypeId",
                        column: x => x.MediaTypeId,
                        principalTable: "MediaTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "MediaTypes",
                columns: new[] { "Id", "ColorClass", "IconClass", "Name" },
                values: new object[,]
                {
                    { 1, "primary", "fas fa-book", "Kniha" },
                    { 2, "success", "fas fa-compact-disc", "CD" },
                    { 3, "warning", "fas fa-film", "DVD" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Author", "BorrowDate", "BorrowerName", "CreatedAt", "Description", "MediaTypeId", "PurchaseDate", "Title" },
                values: new object[,]
                {
                    { 1, "J.R.R. Tolkien", null, null, new DateTime(2023, 6, 15, 10, 30, 0, 0, DateTimeKind.Unspecified), "Klasická fantasy kniha o dobrodružství hobita Bilba Pytlíka", 1, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hobit aneb Cesta tam a zase zpátky" },
                    { 2, "George Orwell", null, null, new DateTime(2024, 3, 20, 14, 15, 0, 0, DateTimeKind.Unspecified), "Dystopický román o totalitním režimu a kontrole společnosti", 1, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "1984" },
                    { 3, "Antoine de Saint-Exupéry", new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jana Nováková", new DateTime(2023, 1, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), "Poetická kniha pro děti i dospělé o přátelství a lásce", 1, new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Malý princ" },
                    { 4, "Pink Floyd", null, null, new DateTime(2022, 8, 5, 16, 45, 0, 0, DateTimeKind.Unspecified), "Legendární rockové album o izolaci a společenských bariérách", 2, new DateTime(2022, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Wall" },
                    { 5, "Michael Jackson", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Petr Svoboda", new DateTime(2023, 11, 12, 11, 20, 0, 0, DateTimeKind.Unspecified), "Nejprodávanější album všech dob s hity jako Billie Jean", 2, new DateTime(2023, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thriller" },
                    { 6, "Queen", null, null, new DateTime(2022, 3, 25, 13, 30, 0, 0, DateTimeKind.Unspecified), "Sběratelské vydání nejlepších hitů legendární kapely Queen", 2, new DateTime(2022, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bohemian Rhapsody" },
                    { 7, "Robert Zemeckis", null, null, new DateTime(2023, 9, 8, 15, 10, 0, 0, DateTimeKind.Unspecified), "Dojemný příběh o muži s nízkým IQ a jeho neobyčejném životě", 3, new DateTime(2023, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Forrest Gump" },
                    { 8, "Lana a Lilly Wachowski", null, null, new DateTime(2024, 2, 14, 12, 0, 0, 0, DateTimeKind.Unspecified), "Sci-fi klasika o virtuální realitě a boji za svobodu", 3, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Matrix" },
                    { 9, "James Cameron", new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marie Svobodová", new DateTime(2021, 12, 1, 18, 45, 0, 0, DateTimeKind.Unspecified), "Epický romantický film o tragédii nejslavnější lodi světa", 3, new DateTime(2021, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Titanic" },
                    { 10, "Peter Jackson", null, null, new DateTime(2022, 7, 18, 20, 15, 0, 0, DateTimeKind.Unspecified), "První díl epické fantasy trilogie podle knihy J.R.R. Tolkiena", 3, new DateTime(2022, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pán prstenů: Společenstvo prstenu" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_BorrowerName",
                table: "Items",
                column: "BorrowerName");

            migrationBuilder.CreateIndex(
                name: "IX_Items_MediaTypeId",
                table: "Items",
                column: "MediaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Title",
                table: "Items",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "MediaTypes");
        }
    }
}
