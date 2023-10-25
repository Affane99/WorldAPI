using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Exceptions;

namespace World.Application.Features.SubPrefecture.Command.DeleteSubPrefecture
{
    public class DeleteSubPrefectureHandler : IRequestHandler<DeleteSubPrefectureCommand, Unit>
    {
        private readonly ISubPrefectureRepository _subPrefectureRepository;

        public DeleteSubPrefectureHandler(ISubPrefectureRepository subPrefectureRepository)
        {
            _subPrefectureRepository = subPrefectureRepository;
        }
        public async Task<Unit> Handle(DeleteSubPrefectureCommand request, CancellationToken cancellationToken)
        {
            var validation = await new DeleteSubPrefectureValidator(_subPrefectureRepository).ValidateAsync(request);
            if (!validation.IsValid)
            {
                throw new BadRequestException("Failed to delete", validation);
            }
            var subPrefecture = await _subPrefectureRepository.FindByIdAsync(request.Id);
            if (subPrefecture != null)
            {
                await _subPrefectureRepository.DeleteAsync(subPrefecture);
            }
            return Unit.Value;
        }
    }
}
