using MediatR;
using World.Application.DTOs.Country;
using World.Application.DTOs.Search;

namespace World.Application.Features.Countries.Query.GetCountries
{
    public class GetCountriesQuery : IRequest<SearchResult<CountryListDto>>
    {
        public SearchDTO Search { get; set; }
    }
}
