using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bookie.Api.Models;

namespace Bookie.Api.Data
{
    public class BookieApiContext : DbContext
    {
        public BookieApiContext (DbContextOptions<BookieApiContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var sqlConnectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=bookieDB";
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

        public DbSet<Book> Book { get; set; } = default!; 
        public DbSet<Author> Author { get; set; }
        public DbSet<PriceOffer> PriceOffers { get; set; }
    }
}