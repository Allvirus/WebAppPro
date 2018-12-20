using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCTest.Models;

namespace MVCTest.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<VisaForm> VisaForm { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Test> Tests { get; set; }


    }
}
