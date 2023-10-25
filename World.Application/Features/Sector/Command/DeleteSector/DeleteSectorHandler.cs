using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Exceptions;

namespace World.Application.Features.Sector.Command.DeleteSector
{
    public class DeleteSectorHandler : IRequestHandler<DeleteSectorCommand, Unit>
    {
        private readonly ISectorRepository _sectorRepository;

        public DeleteSectorHandler(ISectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }
        public async Task<Unit> Handle(DeleteSectorCommand request, CancellationToken cancellationToken)
        {
            var validation = await new DeleteSectorValidator(_sectorRepository).ValidateAsync(request);
            if (!validation.IsValid)
            {
                throw new BadRequestException("Failed to delete", validation);
            }
            var sector = await _sectorRepository.FindByIdAsync(request.Id);
            if (sector != null)
            {
                await _sectorRepository.DeleteAsync(sector);
            }
            return Unit.Value;
        }
    }
}
