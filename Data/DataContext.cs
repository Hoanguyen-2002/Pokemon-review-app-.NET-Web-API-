using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data
{
    public class DataContext : DbContext // Mapping between Entities and Tables in Database include relations of tables
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<PokemonOwner> PokemonOwners { get; set; }
        public DbSet<PokemonCategory> PokemonCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base class implementation of OnModelCreating
            base.OnModelCreating(modelBuilder);

            // Configure the composite key for the PokemonCategory entity
            modelBuilder.Entity<PokemonCategory>()
                        .HasKey(pc => new { pc.PokemonId, pc.CategoryId });

            // Configure the relationship between PokemonCategory and Pokemon
            modelBuilder.Entity<PokemonCategory>()
                        .HasOne(p => p.Pokemon)
                        .WithMany(pc => pc.PokemonCategories)
                        .HasForeignKey(p => p.PokemonId);

            // Configure the relationship between PokemonCategory and Category
            modelBuilder.Entity<PokemonCategory>()
                        .HasOne(p => p.Category)
                        .WithMany(pc => pc.PokemonCategories)
                        .HasForeignKey(c => c.CategoryId);

            // Configure the composite key for the PokemonOwner entity
            modelBuilder.Entity<PokemonOwner>()
                        .HasKey(po => new { po.PokemonId, po.OwnerId });

            // Configure the relationship between PokemonOwner and Pokemon
            modelBuilder.Entity<PokemonOwner>()
                        .HasOne(p => p.Pokemon)
                        .WithMany(po => po.PokemonOwners)
                        .HasForeignKey(p => p.PokemonId);

            // Configure the relationship between PokemonOwner and Owner
            modelBuilder.Entity<PokemonOwner>()
                        .HasOne(p => p.Owner)
                        .WithMany(po => po.PokemonOwners)
                        .HasForeignKey(c => c.OwnerId);
        }
    }
}