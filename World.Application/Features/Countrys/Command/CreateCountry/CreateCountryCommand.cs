using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using World.Application.DTOs.Country;
using World.Application.Responses;

namespace World.Application.Features.Countries.Command.CreateCountry
{
    public class CreateCountryCommand : IRequest<BaseCommandResponse>
    {
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string Capitale { get; set; }
        public Guid ContinentId { get; set; }
    }
}
