using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using World.Domain.Common;

namespace World.Domain
{
    public class Country:BaseDomainEntity
    {
        public string Name { get; set; }
        public string Capitale { get; set; }
        public string CountryCode { get; set; }
        public Guid ContinentId { get; set; }
        [ForeignKey(nameof(ContinentId))]
        public Continent Continent { get; set; }
    }
}
