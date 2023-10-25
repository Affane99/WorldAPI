using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Exceptions;
using World.Application.Responses;

namespace World.Application.Features.Prefecture.Command.CreatePrefecture
{
    public class CreatePrefectureHandler : IRequestHandler<CreatePrefectureCommand, BaseCommandResponse>
    {
        private readonly IPrefectureRepository _prefectureRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public CreatePrefectureHandler(IPrefectureRepository prefectureRepository, IRegionRepository regionRepository, IMapper mapper)
        {
            _prefectureRepository = prefectureRepository;
            _regionRepository = regionRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreatePrefectureCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatePrefectureValidator(_prefectureRepository, _regionRepository);
            var validation = await validator.ValidateAsync(request);
            if(validation.IsValid == false)
            {
                throw new BadRequestException("Creation Failed", validation);
            }
            var prefecture = _mapper.Map<Domain.Prefecture>(request);
            prefecture = await _prefectureRepository.CreateAsync(prefecture);
            var response = new BaseCommandResponse
            {
                Success = true,
                Message = "Creation Successful",
                Id = prefecture.Id
            };
            return response;
        }
    }
}
