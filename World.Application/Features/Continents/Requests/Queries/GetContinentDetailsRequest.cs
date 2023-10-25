using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using World.Application.DTOs.Continent;

namespace World.Application.Features.Continents.Requests.Queries
{
    public class GetContinentDetailsRequest : IRequest<ContinentListDto>
    {
        public Guid Id { get; set; }
    }
}
