using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TritonExpress.Models;

namespace TritonExpress.Repositories
{
    public class TritonExpressDbContext : DbContext
    {
        public TritonExpressDbContext(DbContextOptions<TritonExpressDbContext> options)
                    : base(options)
        {
           // Database.EnsureCreated();
        }

        public DbSet<Branches> Branches { get; set; }
        public DbSet<Province> Provinces { get; set; }
    }
}
