using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace entityframework
{
    public class GamesDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=games.db");
        }
        
    }

    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public Publisher Publisher { get; set; }
    }

    public class Publisher
    {
        public Publisher()
        {
            Games = new List<Game>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public ICollection<Game> Games { get; }
    }
}
