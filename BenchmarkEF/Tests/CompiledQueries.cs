using BenchmarkDotNet.Attributes;
using Bookie.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkEF.Tests
{
    [MemoryDiagnoser]
    //[Config(typeof(BenchConfig))]
    //[RPlotExporter]
    public class CompiledQueries
    {
        private BookieDirectAppContext _db;
        private string[] Top5Publishers = new string[] { 
            "Manning", "Nemira", "O'Reilly Media", "Penguin", "Little, Brown and Company" };

        [GlobalSetup]
        public void GlobalSetup()
        {
            _db = new BookieDirectAppContext();
        }

        [Benchmark]
        public void TopFivePublishersCompiledQuery()
        {
            var books = _db.GetBooksByPublisher(Top5Publishers[0]);
            books = _db.GetBooksByPublisher(Top5Publishers[1]);
            //books = _db.GetBooksByPublisher(Top5Publishers[2]);
            //books = _db.GetBooksByPublisher(Top5Publishers[3]);
            //books = _db.GetBooksByPublisher(Top5Publishers[4]);
        }

        [Benchmark]
        public void TopFivePublishersIndependentQuery()
        {
            var books = _db.Books.Where(b => b.Publisher == Top5Publishers[0]);
            books = _db.Books.Where(b => b.Publisher == Top5Publishers[1]);
            //books = _db.Books.Where(b => b.Publisher == Top5Publishers[2]);
            //books = _db.Books.Where(b => b.Publisher == Top5Publishers[3]);
            //books = _db.Books.Where(b => b.Publisher == Top5Publishers[4]);
        }
    }
}
