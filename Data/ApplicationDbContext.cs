<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wardrobe.Models;
using Microsoft.EntityFrameworkCore.Metadata;
=======
﻿// Data/ApplicationDbContext.cs
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wardrobe.Models;
>>>>>>> 1b9bee640ac8e0c8aff756158b1d76e51c38e248

namespace Wardrobe.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Outfit> Outfits { get; set; }
<<<<<<< HEAD

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Identity tablolarındaki tüm string sütunları PostgreSQL 'text' olarak ayarla
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(string))
                    {
                        // Eğer sütunun tipi "nvarchar" gibi SQL Server tipiyse PostgreSQL uyumlu 'text' olarak değiştir
                        var columnType = property.GetColumnType();
                        if (string.IsNullOrEmpty(columnType) || columnType.Contains("nvarchar"))
                        {
                            property.SetColumnType("text");
                        }
                    }

                    // Eğer Id Guid ise postgres için uuid yapabilirsin (bizim IdentityUser Id string olduğu için gerek yok)
                }
            }
        }
=======
>>>>>>> 1b9bee640ac8e0c8aff756158b1d76e51c38e248
    }
}
