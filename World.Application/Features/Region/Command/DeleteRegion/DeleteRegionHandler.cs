using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Exceptions;

namespace World.Application.Features.Region.Command.DeleteRegion
{
    public class DeleteRegionHandler : IRequestHandler<DeleteRegionCommand, Unit>
    {
        private readonly IRegionRepository _regionRepository;

        public DeleteRegionHandler(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }
        public async Task<Unit> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
        {
            var validation = await new DeleteRegionValidator(_regionRepository).ValidateAsync(request);
            if (!validation.IsValid)
            {
                throw new BadRequestException("Failed to delete", validation);
            }
            var region = await _regionRepository.FindByIdAsync(request.Id);
            if (region != null)
            {
                await _regionRepository.DeleteAsync(region);
            }
            return Unit.Value;
        }
    }
}
