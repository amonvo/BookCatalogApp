using System.ComponentModel.DataAnnotations;

namespace BookCatalogApp.Models.Entities
{
    public class MediaType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [StringLength(20)]
        public string IconClass { get; set; } = string.Empty;

        [StringLength(20)]
        public string ColorClass { get; set; } = string.Empty;

        public virtual ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
