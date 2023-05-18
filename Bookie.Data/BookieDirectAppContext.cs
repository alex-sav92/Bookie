using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection.Emit;
using Bookie.Data.Entities;
using System.Net;

namespace Bookie.Data
{
    public class BookieDirectAppContext : DbContext
    {
        private readonly IConfiguration? _config;
        private bool IsBenchmarkMode;

        public BookieDirectAppContext()
        {
            IsBenchmarkMode = true;
            this.OnConfiguring(new DbContextOptionsBuilder<BookieDirectAppContext>());
        }
        public BookieDirectAppContext(DbContextOptions<BookieDirectAppContext> options, IConfiguration? config = null)
            : base(options)
        {  
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (IsBenchmarkMode) 
            {
                var connectionString = "Server=tcp:bookie-server.database.windows.net,1433;Initial Catalog=bookieDB;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\"";
                options.UseSqlServer(connectionString);
            }
            else
            {
                var connectionString = _config!.GetConnectionString("SQL_CONNECTIONSTRING");
                options.UseSqlServer(connectionString)
                    .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                    .EnableSensitiveDataLogging();
            } 
            
            //options.UseExceptionProcessor();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasColumnType("decimal(6, 2)");

            modelBuilder.Entity<PriceOffer>()
                .Property(p => p.NewPrice)
                .HasColumnType("decimal(6, 2)");
        }

        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<Author> Author { get; set; } = default!;

        public DbSet<BookAuthor> BookAuthor { get; set; } = default!;

        public DbSet<PriceOffer> PriceOffers { get; set; } = default!;
        public DbSet<Review> Review { get; set; } = default!;

        public DbSet<Favorite> Favorites { get; set; } = default!;

        public DbSet<ReviewUnindexed> ReviewsUnindexed { get; set; } = default!;

        public DbSet<Goat> Goats { get; set;} = default!;

        private static Func<BookieDirectAppContext, string, IEnumerable<Book>?> GetBooksByPublisherQuery =
            EF.CompileQuery(
                (BookieDirectAppContext context, string publisher) =>
                    context.Set<Book>().Where(b => b.Publisher == publisher));

        public IEnumerable<Book>? GetBooksByPublisher(string publisher)
        {
            return GetBooksByPublisherQuery(this, publisher);
        }
    }

    public class DbInitialiser
    {
        private readonly BookieDirectAppContext _context;

        public DbInitialiser(BookieDirectAppContext context)
        {
            _context = context;
        }

        public void Run()
        {
            //_context.Database.EnsureDeleted();
            //_context.Database.EnsureCreated();
            _context.Database.Migrate();

            //Seed
        }
    }
}