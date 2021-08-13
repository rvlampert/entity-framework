using System;
using System.Linq;
namespace entityframework
{
    public class Populate
    {
        public static void InsertData()
        {
            
            var rockstar = new Publisher {
                Name = "Rockstar Games",
                Country = "USA"
            };

            var twok = new Publisher {
                Name = "2K Games",
                Country = "USA"
            };

            var bully = new Game {
                Title = "Bully",
                Year = 2005
            };

            var rdr = new Game {
                Title = "Red Dead Redemption",
                Year = 2010
            };

            var gtav = new Game {
                Title = "Grand Theft Auto V",
                Year = 2013
            };

            var rdr2 = new Game {
                Title = "Red Dead Redemption 2",
                Year = 2018
            };

            var bioshock = new Game {
                Title = "Bioshock",
                Year = 2007
            };

            var bioshock2 = new Game {
                Title = "Bioshock 2",
                Year = 2010
            };

            var bioshockInfinite = new Game {
                Title = "Bioshock Infinite",
                Year = 2013
            };

            rockstar.Games.Add(bully);
            rockstar.Games.Add(rdr);
            rockstar.Games.Add(gtav);
            rockstar.Games.Add(rdr2);

            twok.Games.Add(bioshock);
            twok.Games.Add(bioshock2);
            twok.Games.Add(bioshockInfinite);
            
            using (var db = new GamesDbContext())
            {
                db.Publishers.Add(rockstar);
                db.Publishers.Add(twok);

                var count = db.SaveChanges();
                Console.WriteLine("{0} records(s) saved to the database", count);
            }
        }
    }
}