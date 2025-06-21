<<<<<<< HEAD
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Wardrobe.Models;
=======
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wardrobe.Data;
using Wardrobe.Models;
using System.Diagnostics;
using System.Linq;
>>>>>>> 1b9bee640ac8e0c8aff756158b1d76e51c38e248

namespace Wardrobe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
<<<<<<< HEAD

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
=======
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
>>>>>>> 1b9bee640ac8e0c8aff756158b1d76e51c38e248
        }

        public IActionResult Index()
        {
<<<<<<< HEAD
            return View();
=======
            var outfits = _context.Outfits.ToList(); // Tüm kombinler
            return View(outfits);
>>>>>>> 1b9bee640ac8e0c8aff756158b1d76e51c38e248
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
