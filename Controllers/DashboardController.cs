using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookCatalogApp.Data;
using BookCatalogApp.Models.ViewModels;

namespace BookCatalogApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly LibraryContext _context;

        public DashboardController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardViewModel
            {
                // Celkové statistiky
                TotalItems = await _context.Items.CountAsync(),
                TotalBooks = await _context.Items.CountAsync(i => i.MediaTypeId == 1),
                TotalCDs = await _context.Items.CountAsync(i => i.MediaTypeId == 2),
                TotalDVDs = await _context.Items.CountAsync(i => i.MediaTypeId == 3),

                // Statistiky půjčování
                BorrowedItems = await _context.Items.CountAsync(i => !string.IsNullOrEmpty(i.BorrowerName)),
                AvailableItems = await _context.Items.CountAsync(i => string.IsNullOrEmpty(i.BorrowerName)),

                // Nejpůjčovanější položky (simulace - v reálné aplikaci byste měli tabulku historie)
                MostBorrowedItems = await _context.Items
                    .Where(i => !string.IsNullOrEmpty(i.BorrowerName))
                    .Include(i => i.MediaType)
                    .Take(5)
                    .ToListAsync(),

                // Aktuálně půjčené položky
                CurrentlyBorrowedItems = await _context.Items
                    .Where(i => !string.IsNullOrEmpty(i.BorrowerName))
                    .Include(i => i.MediaType)
                    .OrderBy(i => i.BorrowDate)
                    .ToListAsync(),

                // Data pro grafy
                ItemsByTypeData = await GetItemsByTypeData(),
                BorrowingTrendData = await GetBorrowingTrendData()
            };

            return View(viewModel);
        }

        private async Task<Dictionary<string, int>> GetItemsByTypeData()
        {
            return await _context.Items
                .Include(i => i.MediaType)
                .GroupBy(i => i.MediaType.Name)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
        }

        private async Task<Dictionary<string, object>> GetBorrowingTrendData()
        {
            var borrowed = await _context.Items.CountAsync(i => !string.IsNullOrEmpty(i.BorrowerName));
            var available = await _context.Items.CountAsync(i => string.IsNullOrEmpty(i.BorrowerName));

            return new Dictionary<string, object>
            {
                { "Půjčené", borrowed },
                { "Dostupné", available }
            };
        }

        // API endpoint pro real-time data
        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            var stats = new
            {
                totalItems = await _context.Items.CountAsync(),
                borrowedItems = await _context.Items.CountAsync(i => !string.IsNullOrEmpty(i.BorrowerName)),
                availableItems = await _context.Items.CountAsync(i => string.IsNullOrEmpty(i.BorrowerName)),
                itemsByType = await GetItemsByTypeData()
            };

            return Json(stats);
        }
    }
}
