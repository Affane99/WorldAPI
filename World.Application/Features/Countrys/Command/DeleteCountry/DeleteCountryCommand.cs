using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace World.Application.Features.Countries.Command.DeleteCountry
{
    public class DeleteCountryCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
