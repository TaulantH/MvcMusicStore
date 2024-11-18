using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly MusicStoreDB _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AlbumsController(MusicStoreDB context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            var musicStoreDB = _context.Album.Include(a => a.Artist).Include(a => a.Genre);
            return View(await musicStoreDB.ToListAsync());
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Album
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(m => m.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "Name");
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Name");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album, IFormFile Image)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage); // Logs errors to console for easier debugging
                }
                // Repopulate the dropdown lists if there's an error
                ViewBag.GenreId = new SelectList(_context.Genres, "GenreId", "Name"); // Use "Name" instead of "GenreId"
                ViewBag.ArtistId = new SelectList(_context.Artists, "ArtistId", "Name"); // Use "Name" instead of "ArtistId"
                return View(album);
            }

            // Handle file upload
            if (Image != null && Image.Length > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(Image.FileName);
                var extension = Path.GetExtension(Image.FileName);
                var uniqueFileName = $"{fileName}{extension}";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", uniqueFileName);

                // Save the file to wwwroot/images
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                album.Image = $"images/{uniqueFileName}"; // Store relative path
            }

            // Add the album to context and save
            _context.Album.Add(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Album.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId", album.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", album.GenreId);
            return View(album);
        }

		// POST: Albums/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl,Image")] Album album, IFormFile Image)
		{
			if (id != album.AlbumId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					// Update the album details first (excluding the image path for now)
					_context.Update(album);
					await _context.SaveChangesAsync();

					// Handle the image file upload
					if (Image != null && Image.Length > 0)
					{
						var extension = Path.GetExtension(Image.FileName);
						var uniqueFileName = $"{Path.GetFileNameWithoutExtension(Image.FileName)}_{Guid.NewGuid()}{extension}";
						var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", uniqueFileName);

						// Save the file to wwwroot/images
						using (var stream = new FileStream(path, FileMode.Create))
						{
							await Image.CopyToAsync(stream);
						}

						// Update the album with the image path
						album.Image = $"images/{uniqueFileName}";

						// Save the updated album record with the image path
						_context.Update(album);
						await _context.SaveChangesAsync();
					}
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!AlbumExists(album.AlbumId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}

				return RedirectToAction(nameof(Index));
			}

			// Re-populate ViewData for dropdowns in case of validation errors
			ViewData["ArtistName"] = new SelectList(_context.Artists, "ArtistId", "ArtistId", album.ArtistId);
			ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", album.GenreId);

			return View(album);
		}


		// GET: Albums/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Album
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(m => m.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Album.FindAsync(id);
            if (album != null)
            {
                _context.Album.Remove(album);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
            return _context.Album.Any(e => e.AlbumId == id);
        }
    }
}
