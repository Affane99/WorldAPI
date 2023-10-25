using System;
using System.Collections.Generic;
using System.Text;
using World.Application.DTOs.Sector;

namespace World.Application.DTOs.Village
{
    public class VillageDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SectorDto Sector { get; set; } 
    }
}
