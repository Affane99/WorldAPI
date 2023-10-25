using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Domain;

namespace World.Persistence.Configurations.Entities
{
    public class ContinentConfiguration : IEntityTypeConfiguration<Continent>
    {
        public void Configure(EntityTypeBuilder<Continent> builder)
        {
            builder.HasData(
                new Continent
                {
                    Id = Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3301"),
                    Name = "Afrique",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "1"
                }
                );
        }
    }
}
