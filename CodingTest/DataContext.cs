using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CodingTest.Models;

namespace CodingTest
{
    public class DataContext : DbContext
    {
        public DbSet<Reading> Readings { get; set; }
    }
}