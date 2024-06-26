﻿using Akademik.Application;
using Akademik.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Akademik.Infrastructure.Persistence
{
    public class AkademikDbContext : IdentityDbContext
    {
        public DbSet<Resident> Residents { get; set; }
        public DbSet<ResidentDetails> ResidentsDetails { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Malfunction> Malfunctions { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public AkademikDbContext(DbContextOptions<AkademikDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Relacja jeden-do-jednego między Resident a ResidentDetails
            modelBuilder.Entity<Resident>()
                .HasOne(r => r.ResidentDetails)
                .WithOne() // Nie ma właściwości nawigacyjnej w ResidentDetails
                .HasForeignKey<Resident>(r => r.ResidentDetailsId);

            // Relacja jeden-do-wielu między Room a Resident
            modelBuilder.Entity<Room>()
                .HasMany(r => r.Residents)
                .WithOne(r => r.Room)
                .HasForeignKey(r => r.RoomNumber);

            modelBuilder.Entity<Malfunction>()
                .HasOne(m => m.Resident)
                .WithMany(r => r.Malfunctions)
                .HasForeignKey(m => m.ResidentId);

            modelBuilder.Entity<Malfunction>()
                .HasOne(m => m.Room)
                .WithMany(r => r.Malfunctions)
                .HasForeignKey(m => m.RoomNumber);

        }


    }
}
