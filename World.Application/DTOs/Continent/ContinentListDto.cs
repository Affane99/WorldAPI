using World.Application.DTOs.Common;

namespace World.Application.DTOs.Continent
{
    public class ContinentListDto : BaseDto, IContinentDto
    {
        public string Name { get; set; }
    }
}
