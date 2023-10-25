using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Exceptions;
using World.Application.Responses;

namespace World.Application.Features.Sector.Command.CreateSector
{
    public class CreateSectorHandler : IRequestHandler<CreateSectorCommand, BaseCommandResponse>
    {
        private readonly ISectorRepository _sectorRepository;
        private readonly ISubPrefectureRepository _subPrefectureRepository;
        private readonly IMapper _mapper;

        public CreateSectorHandler(ISectorRepository sectorRepository, ISubPrefectureRepository subPrefectureRepository, IMapper mapper)
        {
            _sectorRepository = sectorRepository;
            _subPrefectureRepository = subPrefectureRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateSectorCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateSectorValidator(_sectorRepository, _subPrefectureRepository);
            var validation = await validator.ValidateAsync(request);
            if(validation.IsValid == false)
            {
                throw new BadRequestException("Creation Failed", validation);
            }
            var sector = _mapper.Map<Domain.Sector>(request);
            sector = await _sectorRepository.CreateAsync(sector);
            var response = new BaseCommandResponse
            {
                Success = true,
                Message = "Creation Successful",
                Id = sector.Id
            };
            return response;
        }
    }
}
