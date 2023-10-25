using MediatR;
using System;
using World.Application.Responses;

namespace World.Application.Features.Prefecture.Command.CreatePrefecture
{
    public class CreatePrefectureCommand : IRequest<BaseCommandResponse>
    {
        public string Name { get; set; }
        public Guid RegionId { get; set; }
    }
}
