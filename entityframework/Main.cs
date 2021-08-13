using System;
using System.Linq;
namespace entityframework
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using (var db = new GamesDbContext())
            {
                if (db.Publishers.Count() == 0)
                {
                    Console.WriteLine("The database looks empty; inserting sample data...");
                    Populate.InsertData();
                }
                
                Console.WriteLine("Listing games from 2010 on, by publisher:");
                var publishers = db.Publishers
                    .Select(p => new
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Country = p.Country,
                        Games = p.Games.Where(g => g.Year >= 2010).ToList()
                    })
                    .ToList();
                foreach (var publisher in publishers)
                {
                    Console.WriteLine($"Publisher [ID: {publisher.Id}]: {publisher.Name} ({publisher.Country})");
                    var games = publisher.Games;
                    if (games.Count == 0)
                    {
                        Console.WriteLine("  <No games>");
                        continue;
                    }
                    foreach (var game in games)
                    {
                        Console.WriteLine("  - {0} ({1})", game.Title, game.Year);
                    }
                }
            }
        }
    }
}