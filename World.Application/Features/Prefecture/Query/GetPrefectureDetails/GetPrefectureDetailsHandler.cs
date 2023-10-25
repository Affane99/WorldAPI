using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.DTOs.Prefecture;
using World.Application.Exceptions;

namespace World.Application.Features.Prefecture.Query.GetPrefectureDetails
{
    public class GetPrefectureDetailsHandler : IRequestHandler<GetPrefectureDetailsQuery, PrefectureDto>
    {
        private readonly IPrefectureRepository _prefectureRepository;
        private readonly IMapper _mapper;

        public GetPrefectureDetailsHandler(IPrefectureRepository prefectureRepository, IMapper mapper)
        {
            _prefectureRepository = prefectureRepository;
            _mapper = mapper;
        }
        public async Task<PrefectureDto> Handle(GetPrefectureDetailsQuery request, CancellationToken cancellationToken)
        {
            var validation = await new GetPrefectureDetailsValidator(_prefectureRepository).ValidateAsync(request, cancellationToken);
            if(!validation.IsValid)
            {
                throw new BadRequestException("Getting details Failed", validation);
            }
            var prefecture = _prefectureRepository.GetQuery("Region.Country.Continent").Where(x => x.Id.Equals(request.Id)).FirstOrDefault();
            PrefectureDto prefectureDto = new PrefectureDto();
            _mapper.Map(prefecture, prefectureDto);
            return prefectureDto;
        }
    }
}
