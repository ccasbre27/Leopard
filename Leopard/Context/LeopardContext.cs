using Leopard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Leopard.Context
{
    public class LeopardContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }

        public DbSet<Log> Logs { get; set; }

        public LeopardContext() : base("LeopardConnectionString")
        {
            
        }

     
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

    
    }
}