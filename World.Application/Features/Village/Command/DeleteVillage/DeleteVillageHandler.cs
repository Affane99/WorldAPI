using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Exceptions;

namespace World.Application.Features.Village.Command.DeleteVillage
{
    public class DeleteVillageHandler : IRequestHandler<DeleteVillageCommand, Unit>
    {
        private readonly IVillageRepository _villageRepository;

        public DeleteVillageHandler(IVillageRepository villageRepository)
        {
            _villageRepository = villageRepository;
        }
        public async Task<Unit> Handle(DeleteVillageCommand request, CancellationToken cancellationToken)
        {
            var validation = await new DeleteVillageValidator(_villageRepository).ValidateAsync(request);
            if (!validation.IsValid)
            {
                throw new BadRequestException("Failed to delete", validation);
            }
            var village = await _villageRepository.FindByIdAsync(request.Id);
            if (village != null)
            {
                await _villageRepository.DeleteAsync(village);
            }
            return Unit.Value;
        }
    }
}
