using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Exceptions;

namespace World.Application.Features.Sector.Command.UpdateSector
{
    public class UpdateSectorHandler : IRequestHandler<UpdateSectorCommand, Unit>
    {
        private readonly ISectorRepository _sectorRepository;
        private readonly ISubPrefectureRepository _subPrefectureRepository;
        private readonly IMapper _mapper;

        public UpdateSectorHandler(ISectorRepository sectorRepository, ISubPrefectureRepository subPrefectureRepository, IMapper mapper)
        {
            _sectorRepository = sectorRepository;
            _subPrefectureRepository = subPrefectureRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSectorCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSectorValidator(_sectorRepository, _subPrefectureRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.IsValid == false)
            {
                throw new BadRequestException("Failed to Update", validation);
            }
            var sector = await _sectorRepository.FindByIdAsync(request.Id);
            _mapper.Map(request, sector);
            await _sectorRepository.UpdateAsync(sector);

            return Unit.Value;
        }
    }
}
