using Api.Context.Interfaces;
using Api.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Context
{
    public class ApplicationDbContext : DbContext,IDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Patient> Patients{ get; set; }

        public  async Task SaveChangesAsync() => await base.SaveChangesAsync();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Patient>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            base.OnModelCreating(modelBuilder);
        }

    }
}
