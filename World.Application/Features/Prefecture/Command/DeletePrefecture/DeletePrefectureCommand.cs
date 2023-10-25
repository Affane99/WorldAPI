using MediatR;
using System;

namespace World.Application.Features.Prefecture.Command.DeletePrefecture
{
    public class DeletePrefectureCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
