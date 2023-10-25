using System;
using System.Collections.Generic;
using System.Text;
using World.Application.DTOs.Region;

namespace World.Application.DTOs.Prefecture
{
    public class PrefectureDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RegionDto Region { get; set; }
    }
}
