using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace World.Application.Features.Continents.Requests.Commands
{
    public class DeleteContinentCommandRequest : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
