using System.ComponentModel.DataAnnotations;

namespace BookCatalogApp.Models.Entities
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Název")]
        public string Title { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Autor/Režisér")]
        public string Author { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Typ média")]
        public int MediaTypeId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Datum pořízení")]
        public DateTime? PurchaseDate { get; set; }

        [StringLength(1000)]
        [Display(Name = "Popis")]
        public string Description { get; set; } = string.Empty;

        // NOVÉ VLASTNOSTI PRO OBRÁZKY A QR KÓDY
        [StringLength(500)]
        [Display(Name = "URL obrázku obálky")]
        [DataType(DataType.Url)]
        public string? CoverImageUrl { get; set; }

        [StringLength(100)]
        [Display(Name = "Název souboru obrázku")]
        public string? CoverImageFileName { get; set; }

        [Display(Name = "QR kód")]
        public string? QRCode { get; set; }

        [StringLength(100)]
        [Display(Name = "Půjčeno komu")]
        public string? BorrowerName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Datum půjčení")]
        public DateTime? BorrowDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual MediaType MediaType { get; set; } = null!;

        [Display(Name = "Je půjčeno")]
        public bool IsBorrowed => !string.IsNullOrEmpty(BorrowerName);

        // NOVÉ VYPOČÍTANÉ VLASTNOSTI
        [Display(Name = "Má obrázek obálky")]
        public bool HasCoverImage => !string.IsNullOrEmpty(CoverImageUrl) || !string.IsNullOrEmpty(CoverImageFileName);

        public string CoverImagePath => !string.IsNullOrEmpty(CoverImageFileName)
            ? $"/uploads/covers/{CoverImageFileName}"
            : CoverImageUrl ?? "/images/no-cover.png";
    }
}
