using System.ComponentModel.DataAnnotations.Schema; // Gerekirse

namespace Wardrobe.Models
{
    public class Outfit
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public string? ImagePath { get; set; }

        public string? Type { get; set; }

        public string? Color { get; set; }

        public string? Style { get; set; }

        public string? UserId { get; set; }  // Kullanıcıya bağlamak için

        // İstersen buraya ilişki ekleyebilirsin:
        //[ForeignKey("UserId")]
        //public virtual ApplicationUser? User { get; set; }
    }
}
