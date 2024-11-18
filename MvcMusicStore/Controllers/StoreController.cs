using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;
using System.Web;
namespace Wrox.ProMvc5.Security.Authorize.Controllers
{
    namespace MvcMusicStore.Controllers
    {
        public class StoreController : Controller
        {
            public ActionResult Index()
            {
                var albums = GetAlbums();
                return View(albums);
            }
            public ActionResult Buy(int id)
            {
                var album = GetAlbums().Single(a => a.AlbumId == id);
                //Charge the user and ship the album!!!
                return View(album);
            }
            // A simple music catalog
            private static List<Album> GetAlbums()
            {
                var albums = new List<Album>{
 new Album { AlbumId = 1, Title = "The Fall of Math",
 Price = 8.99M},
 new Album { AlbumId = 2, Title = "The Blue Notebooks",
 Price = 8.99M},
 new Album { AlbumId = 3, Title = "Lost in Translation",
 Price = 9.99M },
 new Album { AlbumId = 4, Title = "Permutation",
 Price = 10.99M },
 };
                return albums;
            }
        }
    }
}