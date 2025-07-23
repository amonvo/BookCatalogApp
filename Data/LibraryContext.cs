using Microsoft.EntityFrameworkCore;
using BookCatalogApp.Models.Entities;

namespace BookCatalogApp.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Počáteční data pro typy médií
            modelBuilder.Entity<MediaType>().HasData(
                new MediaType { Id = 1, Name = "Kniha", IconClass = "fas fa-book", ColorClass = "primary" },
                new MediaType { Id = 2, Name = "CD", IconClass = "fas fa-compact-disc", ColorClass = "success" },
                new MediaType { Id = 3, Name = "DVD", IconClass = "fas fa-film", ColorClass = "warning" }
            );

            // Testovací data se statickými hodnotami
            modelBuilder.Entity<Item>().HasData(
                // Knihy
                new Item
                {
                    Id = 1,
                    Title = "Hobit aneb Cesta tam a zase zpátky",
                    Author = "J.R.R. Tolkien",
                    MediaTypeId = 1,
                    PurchaseDate = new DateTime(2023, 6, 15),
                    Description = "Klasická fantasy kniha o dobrodružství hobita Bilba Pytlíka",
                    CreatedAt = new DateTime(2023, 6, 15, 10, 30, 0)
                },
                new Item
                {
                    Id = 2,
                    Title = "1984",
                    Author = "George Orwell",
                    MediaTypeId = 1,
                    PurchaseDate = new DateTime(2024, 3, 20),
                    Description = "Dystopický román o totalitním režimu a kontrole společnosti",
                    CreatedAt = new DateTime(2024, 3, 20, 14, 15, 0)
                },
                new Item
                {
                    Id = 3,
                    Title = "Malý princ",
                    Author = "Antoine de Saint-Exupéry",
                    MediaTypeId = 1,
                    PurchaseDate = new DateTime(2023, 1, 10),
                    Description = "Poetická kniha pro děti i dospělé o přátelství a lásce",
                    CreatedAt = new DateTime(2023, 1, 10, 9, 0, 0),
                    BorrowerName = "Jana Nováková",
                    BorrowDate = new DateTime(2025, 1, 10)
                },

                // CD
                new Item
                {
                    Id = 4,
                    Title = "The Wall",
                    Author = "Pink Floyd",
                    MediaTypeId = 2,
                    PurchaseDate = new DateTime(2022, 8, 5),
                    Description = "Legendární rockové album o izolaci a společenských bariérách",
                    CreatedAt = new DateTime(2022, 8, 5, 16, 45, 0)
                },
                new Item
                {
                    Id = 5,
                    Title = "Thriller",
                    Author = "Michael Jackson",
                    MediaTypeId = 2,
                    PurchaseDate = new DateTime(2023, 11, 12),
                    Description = "Nejprodávanější album všech dob s hity jako Billie Jean",
                    CreatedAt = new DateTime(2023, 11, 12, 11, 20, 0),
                    BorrowerName = "Petr Svoboda",
                    BorrowDate = new DateTime(2025, 1, 15)
                },
                new Item
                {
                    Id = 6,
                    Title = "Bohemian Rhapsody",
                    Author = "Queen",
                    MediaTypeId = 2,
                    PurchaseDate = new DateTime(2022, 3, 25),
                    Description = "Sběratelské vydání nejlepších hitů legendární kapely Queen",
                    CreatedAt = new DateTime(2022, 3, 25, 13, 30, 0)
                },

                // DVD
                new Item
                {
                    Id = 7,
                    Title = "Forrest Gump",
                    Author = "Robert Zemeckis",
                    MediaTypeId = 3,
                    PurchaseDate = new DateTime(2023, 9, 8),
                    Description = "Dojemný příběh o muži s nízkým IQ a jeho neobyčejném životě",
                    CreatedAt = new DateTime(2023, 9, 8, 15, 10, 0)
                },
                new Item
                {
                    Id = 8,
                    Title = "Matrix",
                    Author = "Lana a Lilly Wachowski",
                    MediaTypeId = 3,
                    PurchaseDate = new DateTime(2024, 2, 14),
                    Description = "Sci-fi klasika o virtuální realitě a boji za svobodu",
                    CreatedAt = new DateTime(2024, 2, 14, 12, 0, 0)
                },
                new Item
                {
                    Id = 9,
                    Title = "Titanic",
                    Author = "James Cameron",
                    MediaTypeId = 3,
                    PurchaseDate = new DateTime(2021, 12, 1),
                    Description = "Epický romantický film o tragédii nejslavnější lodi světa",
                    CreatedAt = new DateTime(2021, 12, 1, 18, 45, 0),
                    BorrowerName = "Marie Svobodová",
                    BorrowDate = new DateTime(2025, 1, 5)
                },
                new Item
                {
                    Id = 10,
                    Title = "Pán prstenů: Společenstvo prstenu",
                    Author = "Peter Jackson",
                    MediaTypeId = 3,
                    PurchaseDate = new DateTime(2022, 7, 18),
                    Description = "První díl epické fantasy trilogie podle knihy J.R.R. Tolkiena",
                    CreatedAt = new DateTime(2022, 7, 18, 20, 15, 0)
                }
            );

            // Indexy pro lepší výkon
            modelBuilder.Entity<Item>()
                .HasIndex(i => i.Title)
                .HasDatabaseName("IX_Items_Title");

            modelBuilder.Entity<Item>()
                .HasIndex(i => i.BorrowerName)
                .HasDatabaseName("IX_Items_BorrowerName");

            // Konfigurace vztahů
            modelBuilder.Entity<Item>()
                .HasOne(i => i.MediaType)
                .WithMany(mt => mt.Items)
                .HasForeignKey(i => i.MediaTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
