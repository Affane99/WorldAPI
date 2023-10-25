using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Exceptions;
using World.Application.Responses;

namespace World.Application.Features.SubPrefecture.Command.CreateSubPrefecture
{
    public class CreateSubPrefectureHandler : IRequestHandler<CreateSubPrefectureCommand, BaseCommandResponse>
    {
        private readonly ISubPrefectureRepository _subPrefectureRepository;
        private readonly IPrefectureRepository _prefectureRepository;
        private readonly IMapper _mapper;

        public CreateSubPrefectureHandler(ISubPrefectureRepository subPrefectureRepository, IPrefectureRepository prefectureRepository, IMapper mapper)
        {
            _subPrefectureRepository = subPrefectureRepository;
            _prefectureRepository = prefectureRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateSubPrefectureCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateSubPrefectureValidator(_subPrefectureRepository, _prefectureRepository);
            var validation = await validator.ValidateAsync(request);
            if(validation.IsValid == false)
            {
                throw new BadRequestException("Creation Failed", validation);
            }
            var subPrefecture = _mapper.Map<Domain.SubPrefecture>(request);
            subPrefecture = await _subPrefectureRepository.CreateAsync(subPrefecture);
            var response = new BaseCommandResponse
            {
                Success = true,
                Message = "Creation Successful",
                Id = subPrefecture.Id
            };
            return response;
        }
    }
}
