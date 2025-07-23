using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookCatalogApp.Data;
using BookCatalogApp.Models.Entities;
using System.Text.Json;

namespace BookCatalogApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly LibraryContext _context;
        private readonly IWebHostEnvironment _environment;

        public ItemsController(LibraryContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Items
        public async Task<IActionResult> Index(int? mediaTypeId, bool showOnlyBorrowed = false, string searchTerm = "")
        {
            var query = _context.Items.Include(i => i.MediaType).AsQueryable();

            if (mediaTypeId.HasValue)
            {
                query = query.Where(i => i.MediaTypeId == mediaTypeId.Value);
            }

            if (showOnlyBorrowed)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.BorrowerName));
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(i => i.Title.Contains(searchTerm) || i.Author.Contains(searchTerm));
            }

            var items = await query.OrderByDescending(i => i.CreatedAt).ToListAsync();

            ViewBag.MediaTypes = new SelectList(await _context.MediaTypes.ToListAsync(), "Id", "Name");
            ViewBag.CurrentMediaType = mediaTypeId;
            ViewBag.ShowOnlyBorrowed = showOnlyBorrowed;
            ViewBag.SearchTerm = searchTerm;

            return View(items);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.Items
                .Include(i => i.MediaType)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (item == null) return NotFound();
            return View(item);
        }

        // GET: Items/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.MediaTypes = new SelectList(await _context.MediaTypes.ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Author,MediaTypeId,PurchaseDate,Description,CoverImageUrl")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.CreatedAt = DateTime.Now;
                _context.Add(item);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Položka byla úspěšně přidána.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MediaTypes = new SelectList(await _context.MediaTypes.ToListAsync(), "Id", "Name", item.MediaTypeId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.Items.FindAsync(id);
            if (item == null) return NotFound();

            ViewBag.MediaTypes = new SelectList(await _context.MediaTypes.ToListAsync(), "Id", "Name", item.MediaTypeId);
            return View(item);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,MediaTypeId,PurchaseDate,Description,BorrowerName,BorrowDate,CreatedAt,CoverImageUrl,CoverImageFileName,QRCode")] Item item)
        {
            if (id != item.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Položka byla úspěšně upravena.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MediaTypes = new SelectList(await _context.MediaTypes.ToListAsync(), "Id", "Name", item.MediaTypeId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.Items
                .Include(i => i.MediaType)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (item == null) return NotFound();
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                // Smazání přiložených souborů
                if (!string.IsNullOrEmpty(item.CoverImageFileName))
                {
                    var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads", "covers");
                    var filePath = Path.Combine(uploadsPath, item.CoverImageFileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Položka byla úspěšně smazána.";
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Items/Borrow/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Borrow(int id, string borrowerName)
        {
            if (string.IsNullOrEmpty(borrowerName))
            {
                TempData["ErrorMessage"] = "Jméno půjčujícího je povinné.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null) return NotFound();

            if (item.IsBorrowed)
            {
                TempData["ErrorMessage"] = "Položka je již půjčená.";
                return RedirectToAction(nameof(Details), new { id });
            }

            item.BorrowerName = borrowerName;
            item.BorrowDate = DateTime.Now;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Položka byla úspěšně půjčena.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // POST: Items/Return/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return NotFound();

            if (!item.IsBorrowed)
            {
                TempData["ErrorMessage"] = "Položka není půjčená.";
                return RedirectToAction(nameof(Details), new { id });
            }

            item.BorrowerName = null;
            item.BorrowDate = null;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Položka byla úspěšně vrácena.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // NOVÉ METODY PRO OBRÁZKY A QR KÓDY

        // Upload Cover Image
        [HttpPost]
        public async Task<IActionResult> UploadCoverImage(int itemId, IFormFile coverImage)
        {
            if (coverImage == null || coverImage.Length == 0)
            {
                return Json(new { success = false, message = "Žádný soubor nebyl vybrán." });
            }

            var item = await _context.Items.FindAsync(itemId);
            if (item == null)
            {
                return Json(new { success = false, message = "Položka nebyla nalezena." });
            }

            // Kontrola typu souboru
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var fileExtension = Path.GetExtension(coverImage.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return Json(new { success = false, message = "Nepodporovaný formát souboru. Povolené: JPG, PNG, GIF, WebP" });
            }

            // Kontrola velikosti souboru (max 5MB)
            if (coverImage.Length > 5 * 1024 * 1024)
            {
                return Json(new { success = false, message = "Soubor je příliš velký. Maximální velikost: 5MB" });
            }

            try
            {
                // Vytvoření jedinečného názvu souboru
                var fileName = $"{itemId}_{Guid.NewGuid()}{fileExtension}";
                var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads", "covers");

                // Vytvoření složky pokud neexistuje
                Directory.CreateDirectory(uploadsPath);

                var filePath = Path.Combine(uploadsPath, fileName);

                // Smazání starého obrázku pokud existuje
                if (!string.IsNullOrEmpty(item.CoverImageFileName))
                {
                    var oldFilePath = Path.Combine(uploadsPath, item.CoverImageFileName);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Uložení nového souboru
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await coverImage.CopyToAsync(fileStream);
                }

                // Aktualizace databáze
                item.CoverImageFileName = fileName;
                item.CoverImageUrl = null; // Vymažeme URL pokud byl nastaven
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    message = "Obrázek byl úspěšně nahrán.",
                    imageUrl = $"/uploads/covers/{fileName}"
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Chyba při nahrávání: {ex.Message}" });
            }
        }

        // Set Cover Image URL
        [HttpPost]
        public async Task<IActionResult> SetCoverImageUrl(int itemId, string imageUrl)
        {
            var item = await _context.Items.FindAsync(itemId);
            if (item == null)
            {
                return Json(new { success = false, message = "Položka nebyla nalezena." });
            }

            // Validace URL
            if (!string.IsNullOrEmpty(imageUrl) && !Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute) && !imageUrl.StartsWith("/"))
            {
                return Json(new { success = false, message = "Neplatná URL adresa obrázku." });
            }

            try
            {
                // Smazání starého nahraného souboru pokud existuje
                if (!string.IsNullOrEmpty(item.CoverImageFileName))
                {
                    var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads", "covers");
                    var oldFilePath = Path.Combine(uploadsPath, item.CoverImageFileName);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                item.CoverImageUrl = imageUrl;
                item.CoverImageFileName = null;
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    message = "URL obrázku byla úspěšně nastavena.",
                    imageUrl = imageUrl
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Chyba při nastavování URL: {ex.Message}" });
            }
        }

        // Generate QR Code
        [HttpPost]
        public async Task<IActionResult> GenerateQRCode(int itemId)
        {
            var item = await _context.Items.Include(i => i.MediaType).FirstOrDefaultAsync(i => i.Id == itemId);
            if (item == null)
            {
                return Json(new { success = false, message = "Položka nebyla nalezena." });
            }

            try
            {
                // Generování QR kódu s informacemi o položce
                var qrData = $"BookCatalogApp-Item-{item.Id}|{item.Title}|{item.Author}|{item.MediaType.Name}";
                var qrCodeUrl = GenerateQRCodeUrl(qrData);

                item.QRCode = qrCodeUrl;
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    message = "QR kód byl úspěšně vygenerován.",
                    qrCode = qrCodeUrl
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Chyba při generování QR kódu: {ex.Message}" });
            }
        }

        // Helper method for QR Code generation
        private string GenerateQRCodeUrl(string data)
        {
            // Používáme online QR kód generátor pro jednoduchost
            var qrUrl = $"https://api.qrserver.com/v1/create-qr-code/?size=200x200&data={Uri.EscapeDataString(data)}";
            return qrUrl;
        }

        // QR Code Scanner Endpoint
        [HttpGet]
        public async Task<IActionResult> FindByQRCode(string qrData)
        {
            try
            {
                // Parsování QR kódu
                var parts = qrData.Split('|');
                if (parts.Length >= 2 && parts[0].StartsWith("BookCatalogApp-Item-"))
                {
                    var itemIdStr = parts[0].Replace("BookCatalogApp-Item-", "");
                    if (int.TryParse(itemIdStr, out int itemId))
                    {
                        var item = await _context.Items.Include(i => i.MediaType).FirstOrDefaultAsync(i => i.Id == itemId);
                        if (item != null)
                        {
                            TempData["SuccessMessage"] = $"Položka nalezena pomocí QR kódu: {item.Title}";
                            return RedirectToAction("Details", new { id = itemId });
                        }
                    }
                }

                TempData["ErrorMessage"] = "QR kód nebyl rozpoznán nebo položka neexistuje.";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Chyba při zpracování QR kódu.";
                return RedirectToAction("Index");
            }
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
