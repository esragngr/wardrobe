<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
using Wardrobe.Data;
using Wardrobe.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Wardrobe.Helpers; 
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; 

=======
﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wardrobe.Data;
using Wardrobe.Models;
using Wardrobe.Helpers;
using System.Security.Claims;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Net.Http;
>>>>>>> 1b9bee640ac8e0c8aff756158b1d76e51c38e248

namespace Wardrobe.Controllers
{
    public class OutfitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OutfitsController(ApplicationDbContext context)
        {
            _context = context;
        }

<<<<<<< HEAD
        // Listeleme + Arama + Filtreleme
        public IActionResult Index(string search, string type, string color, string style)
        {
            try
            {
                var query = _context.Outfits.AsQueryable();

                if (!string.IsNullOrEmpty(search))
                    query = query.Where(o => o.Description != null && o.Description.Contains(search));

                if (!string.IsNullOrEmpty(type))
                    query = query.Where(o => o.Type == type);

                if (!string.IsNullOrEmpty(color))
                    query = query.Where(o => o.Color == color);

                if (!string.IsNullOrEmpty(style))
                    query = query.Where(o => o.Style == style);

                var outfits = query.ToList();
                return View(outfits);
            }
            catch (Exception ex)
            {
                return Content("HATA: " + ex.Message);
            }
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Outfit outfit, IFormFile image)
        {
            if (image != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadsFolder);
                var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                // 1. Dosyayı kaydet
=======
        // 1. Herkesin görebileceği kombin listesi (Anasayfa)
        public IActionResult Index(string search, string type, string color, string style)
        {
            var query = _context.Outfits.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(o => o.Description.Contains(search));

            if (!string.IsNullOrEmpty(type))
                query = query.Where(o => o.Type == type);

            if (!string.IsNullOrEmpty(color))
                query = query.Where(o => o.Color == color);

            if (!string.IsNullOrEmpty(style))
                query = query.Where(o => o.Style == style);

            return View(query.ToList());
        }

        // 2. Sadece giriş yapan kullanıcının kendi kombinleri
        [Authorize]
        public IActionResult MyOutfits(string search, string type, string color, string style)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = _context.Outfits.Where(o => o.UserId == userId);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(o => o.Description.Contains(search));

            if (!string.IsNullOrEmpty(type))
                query = query.Where(o => o.Type == type);

            if (!string.IsNullOrEmpty(color))
                query = query.Where(o => o.Color == color);

            if (!string.IsNullOrEmpty(style))
                query = query.Where(o => o.Style == style);

            return View(query.ToList());
        }

        // 3. Kombin Ekle - GET
        [Authorize]
        public IActionResult Create() => View();

        // 4. Kombin Ekle - POST
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Outfit outfit, IFormFile image, string imageUrl)
        {
            if (image == null && string.IsNullOrWhiteSpace(imageUrl))
            {
                ModelState.AddModelError("image", "Lütfen bir resim dosyası seçin ya da URL girin.");
                return View(outfit);
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
            string filePath = null;

            // Bilgisayardan yükleme varsa
            if (image != null)
            {
                var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("image", "Sadece JPG, JPEG, PNG, GIF, BMP ve WEBP formatları kabul edilmektedir.");
                    return View(outfit);
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + extension;
                filePath = Path.Combine(uploadsFolder, fileName);

>>>>>>> 1b9bee640ac8e0c8aff756158b1d76e51c38e248
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
<<<<<<< HEAD

                // 2. Arka planı temizle
                var helper = new RemoveBackgroundHelper(); // Parametre gerekmez artık
                var cleanedPath = await helper.RemoveBackgroundAsync(filePath);

                // 3. Yeni yol modeli ata
                outfit.ImagePath = "/uploads/" + Path.GetFileName(cleanedPath);
            }

            if (!ModelState.IsValid)
                return View(outfit);

            _context.Outfits.Add(outfit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // EDIT GET
        public async Task<IActionResult> Edit(int id)
        {
            var outfit = await _context.Outfits.FindAsync(id);
            if (outfit == null) return NotFound();
            return View(outfit);
        }

        // EDIT POST
        [HttpPost]
=======
            }

            // URL'den yükleme varsa
            else if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                try
                {
                    using var httpClient = new HttpClient();
                    var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);

                    var uri = new Uri(imageUrl);
                    var extension = Path.GetExtension(uri.AbsolutePath).ToLowerInvariant();

                    if (!allowedExtensions.Contains(extension))
                        extension = ".jpg"; // Geçersizse jpg olarak varsay

                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    var fileName = Guid.NewGuid() + extension;
                    filePath = Path.Combine(uploadsFolder, fileName);

                    await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("image", "URL'den görsel alınamadı: " + ex.Message);
                    return View(outfit);
                }
            }

            // Arka plan temizleme işlemi
            var helper = new RemoveBackgroundHelper();
            var cleanedPath = await helper.RemoveBackgroundAsync(filePath);
            outfit.ImagePath = "/uploads/" + Path.GetFileName(cleanedPath);

            // Otomatik tür belirleme
            outfit.Type ??= ClothingClassifierHelper.ClassifyType(outfit.Description ?? "");
            outfit.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid) return View(outfit);

            _context.Outfits.Add(outfit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MyOutfits));
        }


        // 5. Kombin Düzenle - GET
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var outfit = await _context.Outfits.FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);
            return outfit == null ? NotFound() : View(outfit);
        }

        // 6. Kombin Düzenle - POST
        [HttpPost]
        [Authorize]
>>>>>>> 1b9bee640ac8e0c8aff756158b1d76e51c38e248
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Outfit outfit, IFormFile image)
        {
            if (id != outfit.Id) return BadRequest();

<<<<<<< HEAD
            if (image != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
=======
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var outfitToUpdate = await _context.Outfits.FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);
            if (outfitToUpdate == null) return NotFound();

            if (image != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
                var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("image", "Sadece JPG, JPEG, PNG, GIF ve BMP formatları kabul edilmektedir.");
                    return View(outfit);
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + extension;
>>>>>>> 1b9bee640ac8e0c8aff756158b1d76e51c38e248
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                var helper = new RemoveBackgroundHelper();
                var cleanedPath = await helper.RemoveBackgroundAsync(filePath);

<<<<<<< HEAD

                outfit.ImagePath = "/uploads/" + Path.GetFileName(cleanedPath);
=======
                // Eski görseli sil
                if (!string.IsNullOrEmpty(outfitToUpdate.ImagePath))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", outfitToUpdate.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }

                outfitToUpdate.ImagePath = "/uploads/" + Path.GetFileName(cleanedPath);
>>>>>>> 1b9bee640ac8e0c8aff756158b1d76e51c38e248
            }

            if (!ModelState.IsValid) return View(outfit);

<<<<<<< HEAD
            _context.Update(outfit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DELETE GET
        public async Task<IActionResult> Delete(int id)
        {
            var outfit = await _context.Outfits.FindAsync(id);
            if (outfit == null) return NotFound();
            return View(outfit);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var outfit = await _context.Outfits.FindAsync(id);
            if (outfit != null)
            {
                _context.Outfits.Remove(outfit);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Kombin öneri sayfası
        public IActionResult Suggest()
        {
            var upper = _context.Outfits
                .Where(o => o.Type == "Tişört" || o.Type == "Gömlek")
                .ToList();

            var lower = _context.Outfits
                .Where(o => o.Type == "Pantolon" || o.Type == "Etek")
                .ToList();

            var combinations = new List<(Outfit Upper, Outfit Lower)>();

            foreach (var u in upper)
            {
                foreach (var l in lower)
                {
                    if (u.Color == l.Color || u.Style == l.Style)
                    {
                        combinations.Add((u, l));
                    }
                }
            }

            return View(combinations);
=======
            outfitToUpdate.Description = outfit.Description;
            outfitToUpdate.Type = outfit.Type;
            outfitToUpdate.Color = outfit.Color;
            outfitToUpdate.Style = outfit.Style;

            _context.Update(outfitToUpdate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MyOutfits));
        }


        // 7. Kombin Sil
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var outfit = await _context.Outfits.FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);

            if (outfit != null)
            {
                if (!string.IsNullOrEmpty(outfit.ImagePath))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", outfit.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                }

                _context.Outfits.Remove(outfit);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(MyOutfits));
        }

        // Yardımcı algoritmalar
        private int ColorToInt(string color) => color switch
        {
            "Kırmızı" => 1,
            "Mavi" => 2,
            "Siyah" => 3,
            "Beyaz" => 4,
            _ => 0
        };

        private int StyleToInt(string style) => style switch
        {
            "Spor" => 1,
            "Resmi" => 2,
            "Günlük" => 3,
            _ => 0
        };

        private double CalculateSimilarity(Outfit upper, Outfit lower)
        {
            int colorDiff = Math.Abs(ColorToInt(upper.Color) - ColorToInt(lower.Color));
            int styleDiff = Math.Abs(StyleToInt(upper.Style) - StyleToInt(lower.Style));
            return 1.0 / (1 + colorDiff + styleDiff);
        }

        // 8. Önerilen kombinleri listeleme
        [Authorize]
        public IActionResult Suggest()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var uppers = _context.Outfits.Where(o => (o.Type == "Tişört" || o.Type == "Gömlek") && o.UserId == userId).ToList();
            var lowers = _context.Outfits.Where(o => (o.Type == "Pantolon" || o.Type == "Etek") && o.UserId == userId).ToList();

            var scoredCombinations = new List<(Outfit upper, Outfit lower, double score)>();

            foreach (var upper in uppers)
            {
                foreach (var lower in lowers)
                {
                    double score = CalculateSimilarity(upper, lower);
                    if (score > 0.3) scoredCombinations.Add((upper, lower, score));
                }
            }

            var topCombinations = scoredCombinations.OrderByDescending(c => c.score).Take(10).ToList();
            return View(topCombinations);
>>>>>>> 1b9bee640ac8e0c8aff756158b1d76e51c38e248
        }
    }
}
