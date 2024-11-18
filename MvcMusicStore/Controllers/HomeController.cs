using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMusicStore.Models;
using System.Diagnostics;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MusicStoreDB _context;

        // Constructor to initialize both logger and context
        public HomeController(ILogger<HomeController> logger, MusicStoreDB context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string filterType, string filterValue)
        {
            // Retrieve albums with artist and genre data
            var albums = _context.Album
                .Include(a => a.Genre)
                .Include(a => a.Artist)
                .AsQueryable();

            // Apply filtering based on filterType and filterValue
            if (!string.IsNullOrEmpty(filterType) || !string.IsNullOrEmpty(filterValue))
            {
                if (filterType == "album")
                {
                    albums = albums.Where(a => a.Title == filterValue);
                }
                else if (filterType == "author")
                {
                    albums = albums.Where(a => a.Artist.Name == filterValue);
                }
                else if (filterType == "All Albums")
                {
                    return View(await albums.ToListAsync());
                }

            }

            return View(await albums.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult _DailyDeal()
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
