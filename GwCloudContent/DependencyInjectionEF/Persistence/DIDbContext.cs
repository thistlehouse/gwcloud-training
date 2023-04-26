using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DI.Domain;
using DIEF.Models;
using Microsoft.EntityFrameworkCore;

namespace DI.Persistence
{
    public class DIDbContext : DbContext
    {
        public DbSet<OperationResult> OperationResults { get; set; }
        public DbSet<Operation> Operations { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=postgres;Uid=postgres;Pwd=r00t");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operation>()
                .HasKey(e => e.Id);
                // .HasOne(e => e.OperationResult)
                // .WithOne(e => e.Operation);

            modelBuilder.Entity<OperationResult>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<OperationResult>()
                .HasOne(e => e.Operation)
                .WithOne(e => e.OperationResult)
                .HasForeignKey<OperationResult>(e => e.OperationId);
        }
    }
}