using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using World.Domain.Common;

namespace World.Domain
{
    public class Prefecture : BaseDomainEntity
    {
        public string Name { get; set; }
        public Guid RegionId { get; set; }
        [ForeignKey(nameof(RegionId))]
        public Region Region { get; set; }
    }
}
