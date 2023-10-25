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
    public class PrefectureConfiguration : IEntityTypeConfiguration<Prefecture>
    {
        public void Configure(EntityTypeBuilder<Prefecture> builder)
        {
        }
    }
}
