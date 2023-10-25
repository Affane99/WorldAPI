using System.Collections.Generic;
using World.Application.DTOs.Common;
using World.Application.DTOs.Country;

namespace World.Application.DTOs.Continent
{
    public class ContinentAllDto : BaseDto
    {
        public string Name { get; set; }
        public List<CountryListDto> Countries { get; set; }
    }
}
