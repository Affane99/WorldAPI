using FluentValidation;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Region.Query.GetRegionDetails
{
    public class GetRegionDetailsValidator : AbstractValidator<GetRegionDetailsQuery>
    {
        private readonly IRegionRepository _regionRepository;

        public GetRegionDetailsValidator(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .MustAsync(async (id, token) =>
                {
                    return await _regionRepository.ExistsAsync(x => x.Id == id);
                }).WithMessage("The {PropertyName} with value \"{PropertyValue}\" does not exist.");
        }
    }
}
