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
    public class VillageConfiguration : IEntityTypeConfiguration<Village>
    {
        public void Configure(EntityTypeBuilder<Village> builder)
        {
        }
    }
}
