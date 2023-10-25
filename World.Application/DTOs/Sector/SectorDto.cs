using System;
using System.Collections.Generic;
using System.Text;
using World.Application.DTOs.SubPrefecture;

namespace World.Application.DTOs.Sector
{
    public class SectorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SubPrefectureDto SubPrefecture { get; set; }
    }
}
