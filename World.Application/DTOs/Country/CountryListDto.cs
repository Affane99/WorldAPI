using World.Application.DTOs.Common;
using World.Application.DTOs.Continent;

namespace World.Application.DTOs.Country
{
    public class CountryListDto : BaseDto
    {
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string Capitale { get; set; }
        public ContinentListDto Continent { get; set; }
    }
}
