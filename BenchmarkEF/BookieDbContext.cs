using Bookie.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkEF
{
    public class BookieDbContext : DbContext
    {
        public void SetLazyLoading(bool lazyLoading = true)
        {
            this.ChangeTracker.LazyLoadingEnabled = lazyLoading;
        }

        public BookieDbContext() { }

        public BookieDbContext(DbContextOptions<BookieDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var sqlConnectionString = "Data Source=bookie-server.database.windows.net;Initial Catalog=bookieDB;User ID=alex-bookie;Password=1-q-a-z-;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            options.UseSqlServer(sqlConnectionString
                 //, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                 )
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                .EnableSensitiveDataLogging();
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
        public DbSet<ReviewUnindexed> ReviewUnindexed { get; set; } = default!;

        public DbSet<Favorite> Favorites { get; set; } = default!;
    }
}
