using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using World.Application.DTOs.Country;

namespace World.Application.Features.Countrys.Query.GetCountryDetails
{
    public class GetCountryDetailsQuery : IRequest<CountryListDto>
    {
        public Guid Id { get; set; }
    }
}
