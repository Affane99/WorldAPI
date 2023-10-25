using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Exceptions;

namespace World.Application.Features.Village.Command.UpdateVillage
{
    public class UpdateVillageHandler : IRequestHandler<UpdateVillageCommand, Unit>
    {
        private readonly IVillageRepository _villageRepository;
        private readonly ISectorRepository _sectorRepository;
        private readonly IMapper _mapper;

        public UpdateVillageHandler(IVillageRepository villageRepository, ISectorRepository sectorRepository, IMapper mapper)
        {
            _villageRepository = villageRepository;
            _sectorRepository = sectorRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateVillageCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateVillageValidator(_villageRepository, _sectorRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.IsValid == false)
            {
                throw new BadRequestException("Failed to Update", validation);
            }
            var village = await _villageRepository.FindByIdAsync(request.Id);
            _mapper.Map(request, village);
            await _villageRepository.UpdateAsync(village);

            return Unit.Value;
        }
    }
}
