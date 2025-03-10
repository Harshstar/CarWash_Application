using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carwash.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace carwash.Data
{
    public class CarwashDbContext:DbContext
    {
        public CarwashDbContext(DbContextOptions<CarwashDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<WashRequest> WashRequests { get; set; }
        public DbSet<RatingReview> RatingReviews { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // WashRequest - Customer relationship: Use Restrict instead of Cascade to avoid cyclic deletes
            // modelBuilder.Entity<WashRequest>()
            //     .HasOne(wr => wr.Customer)
            //     .WithMany(c => c.WashRequests)
            //     .HasForeignKey(wr => wr.CustId)
            //     .OnDelete(DeleteBehavior.Restrict);  // Changed to Restrict

            // modelBuilder.Entity<WashRequest>()
            //     .HasOne(wr => wr.Car)
            //     .WithMany(c => c.WashRequests)
            //     .HasForeignKey(wr => wr.CarId)
            //     .OnDelete(DeleteBehavior.Restrict);  // Changed to Restrict

            // modelBuilder.Entity<WashRequest>()
            //     .HasOne(wr => wr.PackageNavigation)
            //     .WithMany(p => p.WashRequests)
            //     .HasForeignKey(wr => wr.PackageId)
            //     .OnDelete(DeleteBehavior.Restrict);  // Keep Restrict

            // modelBuilder.Entity<WashRequest>()
            //     .HasOne(wr => wr.Washer)
            //     .WithMany(w => w.WashRequests)
            //     .HasForeignKey(wr => wr.WasherId)
            //     .OnDelete(DeleteBehavior.Restrict);  // Changed to Restrict

            

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Washer)
                .WithMany(w => w.Payments)
                .HasForeignKey(p => p.WasherId)
                .OnDelete(DeleteBehavior.Restrict);  // Changed to Restrict

            

            modelBuilder.Entity<RatingReview>()
                .HasOne(rr => rr.Customer)
                .WithMany(c => c.RatingReviews)
                .HasForeignKey(rr => rr.CustId)
                .OnDelete(DeleteBehavior.Restrict);  // Changed to Restrict

            modelBuilder.Entity<PromoCode>()
                .HasMany(pc => pc.Payments)
                .WithOne(p => p.PromoCode)
                .HasForeignKey(p => p.PromoId)
                .OnDelete(DeleteBehavior.Restrict);  // Keep Restrict


            modelBuilder.Entity<Car>()
                .HasOne(c => c.Customer)
                .WithMany(cu => cu.Cars)
                .HasForeignKey(c => c.CustId)
                .OnDelete(DeleteBehavior.Restrict);  // Changed to Restrict

        



                modelBuilder.Entity<WashRequest>()
        .HasOne(wr => wr.Customer)
        .WithMany(u => u.WashRequests)
        .HasForeignKey(wr => wr.CustId)
        .OnDelete(DeleteBehavior.Restrict);

    // WashRequest -> User (Washer)
    modelBuilder.Entity<WashRequest>()
        .HasOne(wr => wr.Washer)
        .WithMany()
        .HasForeignKey(wr => wr.WasherId)
        .OnDelete(DeleteBehavior.Restrict);

    // WashRequest -> Car
    modelBuilder.Entity<WashRequest>()
        .HasOne(wr => wr.Car)
        .WithMany(c => c.WashRequests)
        .HasForeignKey(wr => wr.CarId)
        .OnDelete(DeleteBehavior.Restrict);

    // WashRequest -> Package
    modelBuilder.Entity<WashRequest>()
        .HasOne(wr => wr.PackageNavigation)
        .WithMany(p => p.WashRequests)
        .HasForeignKey(wr => wr.PackageId)
        .OnDelete(DeleteBehavior.Restrict);

    // WashRequest -> Address
    modelBuilder.Entity<WashRequest>()
        .HasOne(wr => wr.Address)
        .WithMany()
        .HasForeignKey(wr => wr.AddressId)
        .OnDelete(DeleteBehavior.Restrict);




        }

    }   
}