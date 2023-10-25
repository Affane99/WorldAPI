using MediatR;
using System;

namespace World.Application.Features.SubPrefecture.Command.UpdateSubPrefecture
{
    public class UpdateSubPrefectureCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid PrefectureId { get; set; }
    }
}
