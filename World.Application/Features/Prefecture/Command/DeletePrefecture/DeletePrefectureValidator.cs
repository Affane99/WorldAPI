using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.Prefecture.Command.DeletePrefecture
{
    public class DeletePrefectureValidator : AbstractValidator<DeletePrefectureCommand>
    {
        private readonly IPrefectureRepository _prefectureRepository;

        public DeletePrefectureValidator(IPrefectureRepository prefectureRepository) 
        {
            _prefectureRepository = prefectureRepository;

            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .MustAsync(async (id, token) =>
                {
                    return await _prefectureRepository.ExistsAsync(x => x.Id == id);
                })
                .WithMessage("The {PropertyName} with value {PropertyValue} does not exist.");
        }
    }
}
