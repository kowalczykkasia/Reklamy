using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Models
{
    public class CodeFirstContext : DbContext
    {
        public CodeFirstContext() { }
    
        public DbSet<Client> Client { get; set; }
        public DbSet<Campaign> Campaign { get; set; }
        public DbSet<Building> Building { get; set; }
        public DbSet<Banner> Banner { get; set; }

        
        public CodeFirstContext(DbContextOptions<CodeFirstContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity => {
                entity.HasKey(e => e.IdClient).HasName("Client_PK");

                entity.Property(e => e.IdClient).ValueGeneratedNever();

                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Phone).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Login).HasMaxLength(100).IsRequired();
                entity.Property(e => e.salt).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Password).HasMaxLength(100).IsRequired();
                entity.Property(e => e.RefreshToken).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.HasKey(e => e.IdBuilding).HasName("Building_PK");

                entity.Property(e => e.IdBuilding).ValueGeneratedNever();

                entity.Property(e => e.Street).HasMaxLength(100).IsRequired();
                entity.Property(e => e.StreetNumber).IsRequired();
                entity.Property(e => e.City).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Height).IsRequired();
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.HasKey(e => e.IdCampaign).HasName("Campaign_PK");

                entity.Property(e => e.IdCampaign).ValueGeneratedNever();

                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired();
                entity.Property(e => e.PricePerSquareMeter).IsRequired();

                entity.HasOne(d => d.Client)
                .WithMany(p => p.Campaigns)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Client_Campaign_FK");

                entity.HasOne(d => d.BuildingFrom)
                .WithMany(p => p.CampaignsFrom)
                .HasForeignKey(d => d.FromIdBuilding)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BuildingFrom_Campaign_FK");

                entity.HasOne(d => d.BuildingTo)
               .WithMany(p => p.CampaignsTo)
               .HasForeignKey(d => d.ToIdBuilding)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("BuildingTo_Campaign_FK");
            });

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.HasKey(e => e.IdAdvertisement).HasName("Banner_PK");

                entity.Property(e => e.IdAdvertisement).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.Area).IsRequired();

                entity.HasOne(d => d.Campaign)
                .WithMany(p => p.Banners)
                .HasForeignKey(d => d.IdCampaign)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Banner_Campaign_FK");
            });
        }
    }
}
