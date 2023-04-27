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
            
            modelBuilder.Entity<Operation>()
                .HasOne(e => e.OperationResult)
                .WithOne()
                .HasForeignKey<OperationResult>(e => e.OperationId);

            modelBuilder.Entity<OperationResult>()
                .HasKey(e => e.Id);
        }
    }
}