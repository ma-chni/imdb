using System;
using IMDb.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace IMDb.Data
{
    class IMDbContext : DbContext
    {

        public DbSet<Actor> Actor { get; set; }

        public DbSet<Company> Company { get; set; }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<ActorMovie> ActorMovie { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #region
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=IMDb;User=sa;Password=Doglover420;");
            #endregion
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovie>()
               .HasKey(bc => new { bc.MovieId, bc.ActorId });

            modelBuilder.Entity<ActorMovie>()
               .HasOne(bc => bc.Actor)
               .WithMany(c => c.Movies)
               .HasForeignKey(bc => bc.ActorId);

            modelBuilder.Entity<ActorMovie>()
               .HasOne(bc => bc.Movie)
               .WithMany(c => c.Actors)
               .HasForeignKey(bc => bc.MovieId);
        }
    }
}
