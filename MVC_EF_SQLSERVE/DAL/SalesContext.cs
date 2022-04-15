using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using MVC_EF_SQLSERVE.Models;

namespace MVC_EF_SQLSERVE.DAL
{
    public class SalesContext : DbContext
    {
        public SalesContext() : base("SalesContext")
        {
        }
        public DbSet<Product> Product { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<Cost> Cost { get; set; }
        public DbSet<Filter> Filter { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}