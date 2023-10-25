using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "f7b1d6b5-ee9b-4f87-9a36-03d9f5c0dfc1",
                    Name = "ReadOnly",
                    NormalizedName = "READONLY"
                },
                new IdentityRole
                {
                    Id = "8eb37384-5962-45b5-8dbd-38311f5723a3",
                    Name = "Read And Write",
                    NormalizedName = "READANDWRITE"
                }
            );
        }
    }
}
