using FluentValidation;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Village.Query.GetVillageDetails
{
    public class GetVillageDetailsValidator : AbstractValidator<GetVillageDetailsQuery>
    {
        private readonly IVillageRepository _villageRepository;

        public GetVillageDetailsValidator(IVillageRepository villageRepository)
        {
            _villageRepository = villageRepository;
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .MustAsync(async (id, token) =>
                {
                    return await _villageRepository.ExistsAsync(x => x.Id == id);
                }).WithMessage("The {PropertyName} with value \"{PropertyValue}\" does not exist.");
        }
    }
}
