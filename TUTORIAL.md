# Creating project 

In your terminal, run the following command to create the project:
```
dotnet new console -n entityframework
cd entityframework
```
# Installing dotnet-ef and adding packeges
Use the following command to install dotnet-ef tool
```
dotnet tool install --global dotnet-ef
```
Use the following command to add the necessary packages:
```
dotnet add package Microsoft.EntityFrameworkCore.Relational
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
```
# Creating makefile
Create a Makefile file in the project directory with the following commands:
```
run:
  dotnet run
```
# Creating a Sqlite database
To create a Sqlite database, we will use the DBeaver tool. it's easy to install and it's very intuitive.

Once it is installed, open it and from the File menu select New, or use the shortcut Ctrl+N, then select to create a Database Connection and click next

when the window to choose the database opens, select SQLite and click next again

In this window, you will fill the Path field with the full path, that is, the path to the root of the project plus the name (example entityframework/games.db) and create the connection

It will ask you to download the driver, just click download.

The next step is to create the tables. To do this, click on the connection in the left pane and then on SQL to create a new script. The script for creating the tables is:

```
CREATE TABLE "Publishers" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Publishers" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NULL,
    "Country" TEXT NULL);
CREATE TABLE "Games" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Games" PRIMARY KEY AUTOINCREMENT,
    "Title" TEXT NULL,
    "Year" INTEGER NOT NULL,
    "PublisherId" INTEGER NULL,
    CONSTRAINT "FK_Games_Publishers_PublisherId" FOREIGN KEY ("PublisherId") REFERENCES "Publishers" ("Id") ON DELETE RESTRICT);
```
# Model.cs
We start by adding dependencies and defining the namespace.
```
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
 
namespace entityframework
{
}
```
Then we create the class responsible for the context, which will map our database to ef, in it we set the classes corresponding to our database tables and we set which type of database we are going to use, in this case Sqlite
```
public class GamesDbContext : DbContext
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Publisher> Publishers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=games.db");
    }

}
```
That done, we create a class for each table in our database, setting each of its attributes. In our case, the games class:
```
public class Game
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public Publisher Publisher { get; set; }
}
```
And the Publishers class:
```
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
```
The complete Model.cs looks like this:
```
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
```
# Populate.cs

The Populate.cs is responsible for inserting data into the database, for this it is necessary that the database already exists with the tables, it will only, as its name says, populate the database. We start again by adding dependencies and defining the namespace
```
using System;
using System.Linq;
entityframework namespace
{
}
```
After that, we created the Populate class and inside it, the InsertData function
```
public class populate
{
    public static void InsertData()
    {
    }
}
```
In this function, we instantiate objects according to the classes we created in Model.cs and assign values ​​to the fields.
```
var rockstar = new Publisher {Name = "Rockstar Games", Country = "USA"};
var twok = new Publisher {Name = "2K Games", Country = "USA"};
var bully = new Game {Title = "Bully",Year = 2005};
var rdr = new Game {Title = "Red Dead Redemption", Year = 2010};
var gtav = new Game {Title = "Grand Theft Auto V", Year = 2013};
var rdr2 = new Game {Title = "Red Dead Redemption 2", Year = 2018};
var bioshock = new Game {Title = "Bioshock", Year = 2007};
var bioshock2 = new Game {Title = "Bioshock 2",Year = 2010};
var bioshockInfinite = new Game {Title = "Bioshock Infinite",Year = 2013};

rockstar.Games.Add(bully);
rockstar.Games.Add(rdr);
rockstar.Games.Add(gtav);
rockstar.Games.Add(rdr2);

twok.Games.Add(bioshock);
twok.Games.Add(bioshock2);
twok.Games.Add(bioshockInfinite);
```
And finally, using the context also created in Model.cs, we save it to the database.
```
using (var db = new GamesDbContext())
{
    db.Publishers.Add(rockstar);
    db.Publishers.Add(twok);
 
    var count = db.SaveChanges();
    Console.WriteLine("{0} record(s) saved to the database", count);
}
```
The complete population stays
```
using System;
using System.Linq;
namespace entityframework
{
    public class Populate
    {
        public static void InsertData()
        {
 
            var rockstar = new Publisher {Name = "Rockstar Games", Country = "USA"};
            var twok = new Publisher {Name = "2K Games", Country = "USA"};
            var bully = new Game {Title = "Bully",Year = 2005};
            var rdr = new Game {Title = "Red Dead Redemption", Year = 2010};
            var gtav = new Game {Title = "Grand Theft Auto V", Year = 2013};
            var rdr2 = new Game {Title = "Red Dead Redemption 2", Year = 2018};
            var bioshock = new Game {Title = "Bioshock", Year = 2007};
            var bioshock2 = new Game {Title = "Bioshock 2",Year = 2010};
            var bioshockInfinite = new Game {Title = "Bioshock Infinite",Year = 2013};
 
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
```

# Program.cs

First we add dependencies, set namespace and main class
```
using System;
using System.Linq;
entityframework namespace
{
    internal static class Program
    {
    }
}
```
Then, inside the class we create the main function, it will be responsible for connecting to the database, calling the insert function and performing queries
```
private static void Main(string[] args)
{
}
```
In it, using the database context created in Model.cs, we check if the database is empty, make sure the database is populated with our examples, we perform the query.
```
using (var db = new GamesDbContext())
{
    if (db.Publishers.Count() == 0)
    {
        Console.WriteLine("The database looks empty; inserting sample data...");
        InsertData();
    }
    Console.WriteLine("Listing games from 2010 on, by publisher:");
    var publishers = db.Publishers
        .Select(p => new
        {
            id = p.id,
            Name = p.Name,
            Country = p.Country,
            Games = p.Games.Where(g => g.Year >= 2010).ToList()
        })
        .ToList();
}
```
Still using the context, we display the search result
```
foreach (var publisher in publishers)
{
    Console.WriteLine($"Publisher [ID: {publisher.Id}]: {publisher.Name} ({publisher.Country})");
    var games = publisher.Games;
    if (games.Count == 0)
    {
        Console.WriteLine(" <No games>");
        continues;
    }
    foreach (var game in games)
    {
        Console.WriteLine(" - {0} ({1})", game.Title, game.Year);
    }
}
```
Program.cs should look something like:
```
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
                    InsertData();
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
```