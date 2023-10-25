using MediatR;
using System;
using World.Application.Responses;

namespace World.Application.Features.SubPrefecture.Command.CreateSubPrefecture
{
    public class CreateSubPrefectureCommand : IRequest<BaseCommandResponse>
    {
        public string Name { get; set; }
        public Guid PrefectureId { get; set; }
    }
}
