using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace carwash.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }
        
        public DbSet<OtpRecord> OtpStorage { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = "a043a764-60cf-4711-9b19-236d3dead45b";
            var customerRoleId = "c47fa1c1-962d-4a5d-a619-9265883af4b8";
            var washerRoleId = "570d90ee-268f-4b7f-8124-c48cd4d5aaf6";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = customerRoleId,
                    ConcurrencyStamp = customerRoleId,
                    Name = "Customer",
                    NormalizedName = "Customer".ToUpper()
                },
                new IdentityRole
                {
                    Id = washerRoleId,
                    ConcurrencyStamp = washerRoleId,
                    Name = "Washer",
                    NormalizedName = "Washer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}