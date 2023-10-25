using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Exceptions;

namespace World.Application.Features.Prefecture.Command.DeletePrefecture
{
    public class DeletePrefectureHandler : IRequestHandler<DeletePrefectureCommand, Unit>
    {
        private readonly IPrefectureRepository _prefectureRepository;

        public DeletePrefectureHandler(IPrefectureRepository prefectureRepository)
        {
            _prefectureRepository = prefectureRepository;
        }
        public async Task<Unit> Handle(DeletePrefectureCommand request, CancellationToken cancellationToken)
        {
            var validation = await new DeletePrefectureValidator(_prefectureRepository).ValidateAsync(request);
            if (!validation.IsValid)
            {
                throw new BadRequestException("Failed to delete", validation);
            }
            var prefecture = await _prefectureRepository.FindByIdAsync(request.Id);
            if (prefecture != null)
            {
                await _prefectureRepository.DeleteAsync(prefecture);
            }
            return Unit.Value;
        }
    }
}
