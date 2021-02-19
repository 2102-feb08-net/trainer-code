using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using EfDbFirstDemo.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EfDbFirstDemo.ConsoleApp
{
    class Program
    {
        // Entity Framework Core
        // database-first approach steps...
        /*
         * 1. recommended: have a separate data access library project.
         * 2. install Microsoft.EntityFrameworkCore.Design and Microsoft.EntityFrameworkCore.SqlServer
         *    to the project you'll put the EF model in.
         * 3. using Git Bash / terminal, from the project folder run (split into several lines for clarity):
         *    dotnet ef dbcontext scaffold <connection-string-in-quotes>
         *      Microsoft.EntityFrameworkCore.SqlServer
         *      --force
         *      --no-onconfiguring
         *    https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet#dotnet-ef-dbcontext-scaffold
         *    (if you don't have dotnet ef installed, run: "dotnet tool install --global dotnet-ef")
         *    (this will fail if your projects do not compile)
         * 4. any time you change the structure of the tables (DDL), go to step 3.
         */

        static DbContextOptions<ChinookContext> s_dbContextOptions;

        static void Main(string[] args)
        {
            // what we just did is called "reverse engineering"
            // the overall goal is: to have c# code that can represent the DB structure & interact with it.
            // that code is called the EF model.
            //   - a class derived from DbContext.

            using var logStream = new StreamWriter("ef-logs.txt", append: false) { AutoFlush = true };
            string connectionString = File.ReadAllText("C:/revature/chinook-connection-string.txt");
            s_dbContextOptions = new DbContextOptionsBuilder<ChinookContext>()
                .UseSqlServer(connectionString)
                .LogTo(logStream.WriteLine, minimumLevel: LogLevel.Information)
                .LogTo(s => Debug.WriteLine(s), minimumLevel: LogLevel.Debug)
                .Options;

            Display5Tracks();

            EditOneOfThoseTracks();

            Display5Tracks();

            InsertANewTrack();

            Display5Tracks();

            DeleteTheNewTrack();

            Display5Tracks();

            // implement some CRUD (create, read, update, delete) operations like those
            // use the reference material i've given on EF Core.

            // bonus: involve multiple tables in the operations, not just track
            // bonus: do it based on user input (Console.ReadLine)
        }

        static void Display5Tracks()
        {
            using var context = new ChinookContext(s_dbContextOptions);

            IQueryable<Track> tracks = context.Tracks
                .Include(t => t.Genre)
                .Include(t => t.Album)
                    .ThenInclude(a => a.Artist)
                .OrderBy(t => t.Name)
                .Take(5);

            // at this point, the query has not yet even been sent, let alone the results downloaded.
            // (because LINQ uses deferred execution)

            foreach (Track track in tracks)
            {
                //context.Genres.First(x => x.GenreId)
                    // ^ writing code like that totally missing the point of all the work an ORM is supposed to do for you.
                Console.WriteLine($"{track.TrackId} - {track.Name} ({track?.Genre?.Name})");
            }

            // EF by default only loads the data from one table at a time.
            //    therefore, the navigation properties will be null or empty.
            // if you need those properties to be filled in, you have to tell EF somehow.

            // there are three ways to tell EF to fill them in:
            //  1. eager loading (do this one): call Include (and maybe ThenInclude) methods
            //        (telling EF in the query itself to join with other tables)
            //  2. lazy loading (avoid this one): can be enabled in the dbcontext options...
            //       it will load each navigation property in the moment you access it.
            //       for very simple cases, minimal convenience
            //       for anything more complicated, the performance impact is too much
            //          (N+1 problem)
            //  3. explicit loading (rarely needed): given an actual object taken from the context
            //        we can tell EF to fill in individual properties

            // good practice with entity framework:
            // 1. pay attention to when the query is actually sent to the DB (e.g. ToList())
            // 2. try to get all the data you need at a given moment in one query rather than several.
            // 3. use eager loading (Include) rather than lazy or explicit.
            // 4. avoid using foreign key values when the navigation properties work instead.



            //List<string> info = context.Tracks
            //    .Include(t => t.Genre)
            //    .OrderBy(t => t.Name)
            //    .Where(track => SomeComplexMethod(track)) // this can't become sql, so, EF will fetch every row and then discard them
            //    .Take(5)
            //    .ToList();
        }

        static void EditOneOfThoseTracks()
        {
            using var context = new ChinookContext(s_dbContextOptions);

            Track track = context.Tracks.OrderBy(x => x.Name).First();

            // context.Tracks.Find(45) // get the track with primary key 45.

            // when i said, EF was "heavyweight" compared to other ORMs, this is the main way -

            // every object that you pull from the context is "tracked" by the context
            // when you call SaveChanges, the context will send all changes that have been
            // noticed automatically on the tracked entities

            track.Name += ".";

            // at this point, no SQL has run, the changes are pending inside the context object.

            //context.Tracks.Update(track); // Update method will make the context track the object you pass
            // (if it isn't already)

            context.SaveChanges(); // or, SaveChangesAsync
            // all the changes are applied as a transaction by default
            // if anything goes wrong - network problems, SQL errors - it's thrown as an exception

            // we could do more changes here and then call SaveChanges again
        }



        static void InsertANewTrack()
        {
            // for adding, you don't need to worry about foreign key values.
            // you can add/change relationships between objects via the navigation properties.

            using var context = new ChinookContext(s_dbContextOptions);

            Track firstTrack = context.Tracks.OrderBy(x => x.Name).First();
            string nameOfFirstTrack = firstTrack.Name;

            MediaType audio = context.MediaTypes.First(x => x.Name.Contains("AUDIO"));

            var random = new Random();
            var track = new Track
            {
                TrackId = random.Next(8000, 80000),
                Name = $"!{nameOfFirstTrack}",
                UnitPrice = 3.99M,
                Milliseconds = 5000,
                MediaType = audio,
                //Genre = new Genre { GenreId = random.Next(10000, 100000), Name = "abc" }
            };

            Genre rock = context.Genres
                //.Include(g => g.Tracks)
                .First(g => g.Name.Contains("rock"));

            rock.Tracks.Add(track);

            // EF frees us from having to worry about foreign key values

            //context.Tracks.Add(track);

            // this not only will see the Genre and insert it as well, with the right foreign key value on the Track...
            context.SaveChanges();
            // ... but also, once SaveChanges returns, all three places where the relationship is represented in .NET (foreign key value, navigation property, reverse navigation property)
            //    will be fixed up to be consistent.
        }

        static void DeleteTheNewTrack()
        {
            // there's actually no way to delete in EF without first fetching the object.
            // first, get the thing, then, remove it from its DbSet, then SaveChanges
        }

        static void DemoCode()
        {
            //// we've seen "LINQ to Objects", when we use the IEnumerable version of LINQ methods
            //var list = new List<Artist>() { null };
            //list.First();
            //// this is "LINQ to SQL", the IQueryable version of LINQ methods
            //// the logic of the LINQ calls is not executed in .NET at all, it's examined, translated to SQL, and executed by the SQL server
            //Artist anArtist = context.Artists.FirstOrDefault(x => x.Name.Contains("sabbath"));

            //IQueryable<string> query = context.Artists
            //    .Select(x => x.Name.ToLower())
            //    .Where(x => x.Length > 10)
            //    .Take(10);

            //List<string> results = query.ToList();s
        }
    }
}
