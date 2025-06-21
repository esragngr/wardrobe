<<<<<<< HEAD
﻿namespace Wardrobe.Models
=======
﻿using System.ComponentModel.DataAnnotations.Schema; // Gerekirse

namespace Wardrobe.Models
>>>>>>> 1b9bee640ac8e0c8aff756158b1d76e51c38e248
{
    public class Outfit
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public string? ImagePath { get; set; }

        public string? Type { get; set; }

        public string? Color { get; set; }

        public string? Style { get; set; }
<<<<<<< HEAD
    }
}

=======

        public string? UserId { get; set; }  // Kullanıcıya bağlamak için

        // İstersen buraya ilişki ekleyebilirsin:
        //[ForeignKey("UserId")]
        //public virtual ApplicationUser? User { get; set; }
    }
}
>>>>>>> 1b9bee640ac8e0c8aff756158b1d76e51c38e248
