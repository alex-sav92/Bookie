// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using BenchmarkEF;
using Bookie.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
Console.WriteLine("Hello, World!");


var summary = BenchmarkRunner.Run(typeof(CompiledQueries)); 
//TestsAverageBookPrice //ContextPooling //Projections //TestsIQueryable

// N+1
//var options = new DbContextOptionsBuilder<BookieDbContext>()
//    .UseSqlServer("Data Source=bookie-server.database.windows.net;Initial Catalog=bookieDB;User ID=alex-bookie;Password=1-q-a-z-;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
//    .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
//    .Options;
//var db = new BookieDbContext(options);

//var a1 = db.NPlus1Problem();
//var a2 = db.NPlus1Solution1();
//var a3 = db.NPlus1Solution2();

//IQueryable vs IEnumerable
//IEnumerable<Book> books = db.Books;
//var filter = books.Where(x => x.BookId >= 2);
//var results = filter.ToList(); 

//var qBooks = db.Books.AsQueryable();
//var qFilter = books.Where(b => b.BookId >= 2);
//var qResults = filter.ToList();


//Query splitting - Carthesian explosion
//Console.WriteLine("-----------Split queries - Carthesian explosion-----------");

//Stopwatch s = new Stopwatch();
//s.Start();
//var books = context.Books
//    .Include(b => b.Reviews)
//    .Include(b => b.AuthorsLink)
//    .ToList();
//s.Stop();

//Console.WriteLine($"[Left full joins] Elapsed: {s.Elapsed.Milliseconds} ms");

//Console.WriteLine("-----------Split queries-----------");
//s.Start();
//var booksSplit = context.Books
//    .AsSplitQuery()
//    .Include(b => b.Reviews)
//    .Include(b => b.AuthorsLink)
//    //.Where(b => b.BookId == 5587)
//    .ToList();
//s.Stop();
//Console.WriteLine($"[Split queries] Elapsed: {s.Elapsed.Milliseconds} ms");

//Console.WriteLine("-----------Single Queries-----------");
//s.Start();
//var booksSingle = context.Books
//    .AsSingleQuery()
//    .Include(b => b.Reviews)
//    .Include(b => b.AuthorsLink)
//    .ToList();
//s.Stop();
//Console.WriteLine($"Elapsed: {s.Elapsed.Milliseconds} ms");

// Disabling lazy loading: 
//var options = new DbContextOptionsBuilder<BookieDbContext>()
//    .UseSqlServer("Data Source=bookie-server.database.windows.net;Initial Catalog=bookieDB;User ID=alex-bookie;Password=1-q-a-z-;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
//    .Options;
//var db = new BookieDbContext(options);
//db.SetLazyLoading(false);

//// Disabling lazy loading - load needed data With explicit load
//var book1 = db.Books.Find(1);
//db.Entry(book1).Collection(b => b.AuthorsLink).Load();
//var firstAuthorId = book1.AuthorsLink.First().AuthorId;
//var author1 = db.Author.First(a => a.AuthorId == firstAuthorId);

//Console.WriteLine("1 Author:" + author1.Name);

//// Disabling lazy loading - load needed data With with Include
//book1 = db.Books.TagWith("---------------AUTHOR 1 - with .Include")
//    .Include(b => b.AuthorsLink)
//    .First(b => b.BookId == 1);
//firstAuthorId = book1.AuthorsLink.First().AuthorId;
//author1 = db.Author.First(a => a.AuthorId == firstAuthorId);
//Console.WriteLine("1 Author:" + author1.Name);


//var options = new DbContextOptionsBuilder<BookieDbContext>()
//    .UseSqlServer("Data Source=bookie-server.database.windows.net;Initial Catalog=bookieDB;User ID=alex-bookie;Password=1-q-a-z-;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
//    .Options;
//var db = new BookieDbContext(options);
//db.SetLazyLoading(false);

//var count = db.Books.Count();  // count done on SQL DB
//var count2 = db.Books.ToList().Count(); // downloads all data + tracking, uses up CPU+memory, count done on app