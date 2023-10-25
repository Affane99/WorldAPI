using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using World.Domain.Common;

namespace World.Domain
{
    public class Sector : BaseDomainEntity
    {
        public string Name { get; set; }
        public Guid SubPrefectureId { get; set; }
        [ForeignKey(nameof(SubPrefectureId))]
        public SubPrefecture SubPrefecture { get; set; }
    }
}
