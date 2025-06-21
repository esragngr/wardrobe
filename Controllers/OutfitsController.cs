using Microsoft.AspNetCore.Mvc;
using Wardrobe.Data;
using Wardrobe.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Wardrobe.Helpers; 
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; 


namespace Wardrobe.Controllers
{
    public class OutfitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OutfitsController(ApplicationDbContext context)
        {
            _context = context;
        }

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
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Outfit outfit, IFormFile image)
        {
            if (id != outfit.Id) return BadRequest();

            if (image != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                var helper = new RemoveBackgroundHelper();
                var cleanedPath = await helper.RemoveBackgroundAsync(filePath);


                outfit.ImagePath = "/uploads/" + Path.GetFileName(cleanedPath);
            }

            if (!ModelState.IsValid) return View(outfit);

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
        }
    }
}
