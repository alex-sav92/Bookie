using BenchmarkDotNet.Attributes;
using Bookie.Data;
using Bookie.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkEF.Tests
{
    public class AsNoTracking
    {
        private BookieDirectAppContext _db;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var goats = new Fixture().CreateGoats(100000);
            _db = new BookieDirectAppContext();
            _db.AddRange(goats);
            _db.SaveChanges();
        }

        [Benchmark(Baseline = true)]
        public void WithTracking()
        {
            var dbContext = new BookieDirectAppContext();
            dbContext.Goats.ToList();
        }

        [Benchmark]
        public void WithAsNoTracking()
        {
            var dbContext = new BookieDirectAppContext();
            dbContext.Goats.AsNoTracking().ToList();
        }
    }

    internal class Fixture
    {
        public IEnumerable<Goat> CreateGoats(int count)
        {
            List<Goat> goats = new List<Goat>();
            var random = new Random();
            var letters = "abcdefghi";
            for(int i = 0; i < count; i++)
            {
                goats.Add(new Goat { 
                    //Id = i+1, 
                    Name = letters[random.Next(0, 9)].ToString() });
            }

            return goats;
        }
    }
}
