using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMusicStore.Models;

namespace MvcMusicStore.Models
{
    public class MusicStoreDB : DbContext
    {
        public MusicStoreDB(DbContextOptions<MusicStoreDB> options)
            : base(options)
        {
        }

        public static object Albums { get; internal set; }
        public DbSet<MvcMusicStore.Models.Album> Album { get; set; } = default!;
        public DbSet<MvcMusicStore.Models.Artist> Artists {get; set; }
        public DbSet<MvcMusicStore.Models.Genre> Genres { get; set; }
		public DbSet<MvcMusicStore.Models.Register> Register { get; set; }

	}
}
