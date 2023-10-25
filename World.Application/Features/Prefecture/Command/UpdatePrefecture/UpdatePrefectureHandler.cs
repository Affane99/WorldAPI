using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Exceptions;

namespace World.Application.Features.Prefecture.Command.UpdatePrefecture
{
    public class UpdatePrefectureHandler : IRequestHandler<UpdatePrefectureCommand, Unit>
    {
        private readonly IPrefectureRepository _prefectureRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public UpdatePrefectureHandler(IPrefectureRepository prefectureRepository, IRegionRepository regionRepository, IMapper mapper)
        {
            _prefectureRepository = prefectureRepository;
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdatePrefectureCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePrefectureValidator(_prefectureRepository, _regionRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.IsValid == false)
            {
                throw new BadRequestException("Failed to Update", validation);
            }
            var prefecture = await _prefectureRepository.FindByIdAsync(request.Id);
            _mapper.Map(request, prefecture);
            await _prefectureRepository.UpdateAsync(prefecture);

            return Unit.Value;
        }
    }
}
