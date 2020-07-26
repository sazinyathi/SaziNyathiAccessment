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
         
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Province
            modelBuilder.Entity<Province>()
                        .HasData(
                         new Province { Id = 1,  Name = "Eastern Cape",Description= "ZA-EC" },
                         new Province { Id = 2,  Name = "Free State", Description = "ZA-FS" },
                         new Province { Id = 3,  Name = "Gauteng", Description = "ZA-GT" },
                         new Province { Id = 4,  Name = "Kwazulu Natal", Description = "ZA-KZN" }
                         );
            //VehicleType
            modelBuilder.Entity<VehicleType>()
                     .HasData(
                      new VehicleType { Id = 1, Name = "Car", Descriptions = "Small Car" },
                      new VehicleType { Id = 2, Name = "Truck", Descriptions = "Meduim Truck" }

                      );
            //Status
            modelBuilder.Entity<Status>()
              .HasData(
               new Status { Id = 1, Name = "Pending", Description = "Awaiting to be Approved" },
               new Status { Id = 2, Name = "Disptached", Description = "The Parcel has been dispatched" }

               );

        }
        public DbSet<Branches> Branches { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Vehicle> Vechicles { get; set; }
        public DbSet<VehicleType> VehicleType { get; set; }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<WayBills> WayBills { get; set; }
    }
}
