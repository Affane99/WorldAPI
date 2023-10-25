using System;
using World.Application.DTOs.Country;

namespace World.Application.DTOs.Region
{
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CountryListDto Country { get; set; }
    }
}
