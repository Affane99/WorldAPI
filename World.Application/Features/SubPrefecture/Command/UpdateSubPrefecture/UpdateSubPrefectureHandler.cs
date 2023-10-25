using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Exceptions;

namespace World.Application.Features.SubPrefecture.Command.UpdateSubPrefecture
{
    public class UpdateSubPrefectureHandler : IRequestHandler<UpdateSubPrefectureCommand, Unit>
    {
        private readonly ISubPrefectureRepository _subPrefectureRepository;
        private readonly IPrefectureRepository _prefectureRepository;
        private readonly IMapper _mapper;

        public UpdateSubPrefectureHandler(ISubPrefectureRepository subPrefectureRepository, IPrefectureRepository prefectureRepository, IMapper mapper)
        {
            _subPrefectureRepository = subPrefectureRepository;
            _prefectureRepository = prefectureRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSubPrefectureCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSubPrefectureValidator(_subPrefectureRepository, _prefectureRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.IsValid == false)
            {
                throw new BadRequestException("Failed to Update", validation);
            }
            var subPrefecture = await _subPrefectureRepository.FindByIdAsync(request.Id);
            _mapper.Map(request, subPrefecture);
            await _subPrefectureRepository.UpdateAsync(subPrefecture);

            return Unit.Value;
        }
    }
}
