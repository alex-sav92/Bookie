using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bookie.DirectApp.Models;

namespace Bookie.DirectApp.Data
{
    public class BookieDirectAppContext : DbContext
    {
        private readonly IConfiguration _config;

        public BookieDirectAppContext (DbContextOptions<BookieDirectAppContext> options, IConfiguration config)
            : base(options)
        { 
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var sqlConnectionString = _config.GetValue<string>("ConnectionStrings:BookieDirectAppContext");
            
            options.UseSqlServer(sqlConnectionString);
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

        public DbSet<Author> Authors { get;set; } = default!;

        public DbSet<PriceOffer> PriceOffers { get; set; } = default!;
    }
}
