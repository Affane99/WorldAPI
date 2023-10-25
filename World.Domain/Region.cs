using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using World.Domain.Common;

namespace World.Domain
{
    public class Region : BaseDomainEntity
    {
        public string Name { get; set; }
        public Guid CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
    }
}
