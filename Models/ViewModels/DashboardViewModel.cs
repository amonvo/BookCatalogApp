using BookCatalogApp.Models.Entities;

namespace BookCatalogApp.Models.ViewModels
{
    public class DashboardViewModel
    {
        // Celkové statistiky
        public int TotalItems { get; set; }
        public int TotalBooks { get; set; }
        public int TotalCDs { get; set; }
        public int TotalDVDs { get; set; }

        // Statistiky půjčování
        public int BorrowedItems { get; set; }
        public int AvailableItems { get; set; }

        // Kolekce dat
        public List<Item> MostBorrowedItems { get; set; } = new();
        public List<Item> CurrentlyBorrowedItems { get; set; } = new();

        // Data pro grafy
        public Dictionary<string, int> ItemsByTypeData { get; set; } = new();
        public Dictionary<string, object> BorrowingTrendData { get; set; } = new();

        // Vypočítané vlastnosti
        public double BorrowingRate => TotalItems > 0 ? (double)BorrowedItems / TotalItems * 100 : 0;
        public double AvailabilityRate => TotalItems > 0 ? (double)AvailableItems / TotalItems * 100 : 0;
    }
}
