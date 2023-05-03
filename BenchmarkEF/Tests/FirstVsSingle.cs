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
    public class FirstVsSingle
    {
        private BookieDirectAppContext _db;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _db = new BookieDirectAppContext();
        }

        [Benchmark]
        public void FindBook_First()
        {
            var first = _db.Books.FirstOrDefault(b => b.Title.Equals("Ruby in Practice"));
        }

        [Benchmark]
        public void FindBook_Single()
        {
            var single = _db.Books.SingleOrDefault(b => b.Title.Equals("Ruby in Practice"));
        }
    }
}
