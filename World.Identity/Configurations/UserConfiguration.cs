using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Identity.Models;

namespace World.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                    new ApplicationUser
                    {
                        Id = "d8db6c69-c13d-49fc-a558-04ddbbf9ccf3",
                        Email = "admin@localhost.com",
                        NormalizedEmail = "ADMIN@LOCALHOST.COM",
                        FirstName = "System",
                        LastName = "Admin",
                        UserName = "admin@localhost.com",
                        NormalizedUserName = "ADMIN@LOCALHOST.COM",
                        PasswordHash = hasher.HashPassword(null,"P@assword1"),
                        EmailConfirmed = true,
                    },
                    new ApplicationUser
                    {
                        Id = "27e6159f-293f-4f8d-862a-450b7432a14c",
                        Email = "user@localhost.com",
                        NormalizedEmail = "USER@LOCALHOST.COM",
                        FirstName = "System",
                        LastName = "User",
                        UserName = "user@localhost.com",
                        NormalizedUserName = "USER@LOCALHOST.COM",
                        PasswordHash = hasher.HashPassword(null,"P@assword1"),
                        EmailConfirmed = true,
                    }
                );
        }
    }
}
