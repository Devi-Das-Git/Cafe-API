using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Reflection.Emit;
using System;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Cafe.Domain.Models;



namespace Cafe.Infrastructure
{
    public class CafeContext : DbContext
    {
        public DbSet<Cafe.Domain.Models.Cafe> Cafes { get; set; }
        public DbSet<Cafe.Domain.Models.Employee> Employees { get; set; }


        public CafeContext(DbContextOptions<CafeContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Define relationships
            //modelBuilder.Entity<Employee>().HasOne(e => e.Cafe).WithMany(c => c.Employees).HasForeignKey(e => e.CafeId);

            modelBuilder.Entity<Cafe.Domain.Models.Cafe>().HasData(
                new Cafe.Domain.Models.Cafe
                {
                    //Id = Guid.NewGuid(),
                    Id = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"),
                    Name = "Cafe Delight",
                    Description = "A delightful cafe with amazing ambiance",
                    Location = "Downtown",
                    Logo = null,
                    Employees = 15
                },
                new Cafe.Domain.Models.Cafe
                {
                    Id = Guid.NewGuid(),
                    Name = "Java Junction",
                    Description = "The perfect spot for coffee lovers",
                    Location = "Uptown",
                    Logo = null,
                    Employees = 14
                });
            // Seed data for Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = "UI1234569",
                    Name = "Alice",
                    Email = "alice@example.com",
                    Phone = "8981234567",
                    Gender = "Female",
                    CafeId = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"),//.NewGuid(),
                    // Replace with actual GUID from Cafe Delight
                    StartDate = DateTime.UtcNow.AddDays(-100)
                    // 100 days ago
                },
                new Employee
                {
                    Id = "UI1234568",
                    Name = "Bob",
                    Email = "bob@example.com",
                    Phone = "8981234568",
                    Gender = "Male",
                    CafeId = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"),
                    // Replace with actual GUID from Java Junction
                    StartDate = DateTime.UtcNow.AddDays(-150) // 150 days ago
                });
        }

    }

}
