using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using World.Domain.Common;

namespace World.Domain
{
    public class SubPrefecture : BaseDomainEntity
    {
        public string Name { get; set; }
        public Guid PrefectureId { get; set; }
        [ForeignKey(nameof(PrefectureId))]
        public Prefecture Prefecture { get; set; }
    }
}
