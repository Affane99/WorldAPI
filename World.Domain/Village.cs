using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using World.Domain.Common;

namespace World.Domain
{
    public class Village : BaseDomainEntity
    {
        public string Name { get; set; }
        public Guid SectorId { get; set; }
        [ForeignKey(nameof(SectorId))]
        public Sector Sector { get; set; }
    }
}
