using MediatR;
using System;

namespace World.Application.Features.SubPrefecture.Command.DeleteSubPrefecture
{
    public class DeleteSubPrefectureCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
