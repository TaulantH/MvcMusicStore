﻿using Humanizer.Localisation;
using MvcMusicStore.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMusicStore.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        public int GenreId { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string? AlbumArtUrl { get; set; }
        public string? Image { get; set; }
        public Genre? Genre { get; set; }
        public Artist? Artist { get; set; }


    }
}