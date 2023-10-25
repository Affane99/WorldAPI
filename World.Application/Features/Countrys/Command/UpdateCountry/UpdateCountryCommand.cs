using MediatR;
using System;

namespace World.Application.Features.Countries.Command.UpdateCountry
{
    public class UpdateCountryCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string Capitale { get; set; }
        public Guid ContinentId { get; set; }
    }
}
