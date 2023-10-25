using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.DTOs.SubPrefecture;
using World.Application.Exceptions;

namespace World.Application.Features.SubPrefecture.Query.GetSubPrefectureDetails
{
    public class GetSubPrefectureDetailsHandler : IRequestHandler<GetSubPrefectureDetailsQuery, SubPrefectureDto>
    {
        private readonly ISubPrefectureRepository _subPrefectureRepository;
        private readonly IMapper _mapper;

        public GetSubPrefectureDetailsHandler(ISubPrefectureRepository subPrefectureRepository, IMapper mapper)
        {
            _subPrefectureRepository = subPrefectureRepository;
            _mapper = mapper;
        }
        public async Task<SubPrefectureDto> Handle(GetSubPrefectureDetailsQuery request, CancellationToken cancellationToken)
        {
            var validation = await new GetSubPrefectureDetailsValidator(_subPrefectureRepository).ValidateAsync(request, cancellationToken);
            if(!validation.IsValid)
            {
                throw new BadRequestException("Getting details Failed", validation);
            }
            var subPrefecture = _subPrefectureRepository.GetQuery("Prefecture.Region.Country.Continent").Where(x => x.Id.Equals(request.Id)).FirstOrDefault();
            SubPrefectureDto subPrefectureDto = new SubPrefectureDto();
            _mapper.Map(subPrefecture, subPrefectureDto);
            return subPrefectureDto;
        }
    }
}
