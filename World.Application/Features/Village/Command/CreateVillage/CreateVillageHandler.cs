using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Exceptions;
using World.Application.Responses;

namespace World.Application.Features.Village.Command.CreateVillage
{
    public class CreateVillageHandler : IRequestHandler<CreateVillageCommand, BaseCommandResponse>
    {
        private readonly IVillageRepository _villageRepository;
        private readonly ISectorRepository _sectorRepository;
        private readonly IMapper _mapper;

        public CreateVillageHandler(IVillageRepository villageRepository, ISectorRepository sectorRepository, IMapper mapper)
        {
            _villageRepository = villageRepository;
            _sectorRepository = sectorRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateVillageCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateVillageValidator(_villageRepository, _sectorRepository);
            var validation = await validator.ValidateAsync(request);
            if(validation.IsValid == false)
            {
                throw new BadRequestException("Creation Failed", validation);
            }
            var village = _mapper.Map<Domain.Village>(request);
            village = await _villageRepository.CreateAsync(village);
            var response = new BaseCommandResponse
            {
                Success = true,
                Message = "Creation Successful",
                Id = village.Id
            };
            return response;
        }
    }
}
