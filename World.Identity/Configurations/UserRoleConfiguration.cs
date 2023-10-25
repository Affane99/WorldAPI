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
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                    new IdentityUserRole<string>
                    {
                        RoleId = "8eb37384-5962-45b5-8dbd-38311f5723a3",
                        UserId = "d8db6c69-c13d-49fc-a558-04ddbbf9ccf3"
                    },
                    new IdentityUserRole<string>
                    {
                        RoleId = "f7b1d6b5-ee9b-4f87-9a36-03d9f5c0dfc1",
                        UserId = "27e6159f-293f-4f8d-862a-450b7432a14c"
                    }
                );
        }
    }
}
