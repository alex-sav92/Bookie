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
        private string Top1 = "Manning";
        private string Top2 = "Nemira";
        private string Top3 = "O'Reilly Media";
        private string Top4 = "Penguin";
        private string Top5 = "Little, Brown and Company";

        [GlobalSetup]
        public void GlobalSetup()
        {
            _db = new BookieDirectAppContext();
        }

        [Benchmark]
        public void TopFivePublishersCompiledQuery()
        {
            var books = _db.GetBooksByPublisher(Top1);
            books = _db.GetBooksByPublisher(Top2);
            books = _db.GetBooksByPublisher(Top3);
            books = _db.GetBooksByPublisher(Top4);
            books = _db.GetBooksByPublisher(Top5);
        }

        [Benchmark]
        public void TopFivePublishersIndependentQuery()
        {
            var books = _db.Books.Where(b => b.Publisher == Top1);
            books = _db.Books.Where(b => b.Publisher == Top2);
            books = _db.Books.Where(b => b.Publisher == Top3);
            books = _db.Books.Where(b => b.Publisher == Top4);
            books = _db.Books.Where(b => b.Publisher == Top5);
        }
    }
}
