using FluentValidation;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Prefecture.Query.GetPrefectureDetails
{
    public class GetPrefectureDetailsValidator : AbstractValidator<GetPrefectureDetailsQuery>
    {
        private readonly IPrefectureRepository _prefectureRepository;

        public GetPrefectureDetailsValidator(IPrefectureRepository prefectureRepository)
        {
            _prefectureRepository = prefectureRepository;
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .MustAsync(async (id, token) =>
                {
                    return await _prefectureRepository.ExistsAsync(x => x.Id == id);
                }).WithMessage("The {PropertyName} with value \"{PropertyValue}\" does not exist.");
        }
    }
}
