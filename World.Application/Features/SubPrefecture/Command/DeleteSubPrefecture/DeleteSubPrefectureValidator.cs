using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using World.Application.Contracts.Persistence;

namespace World.Application.Features.SubPrefecture.Command.DeleteSubPrefecture
{
    public class DeleteSubPrefectureValidator : AbstractValidator<DeleteSubPrefectureCommand>
    {
        private readonly ISubPrefectureRepository _subPrefectureRepository;

        public DeleteSubPrefectureValidator(ISubPrefectureRepository subPrefectureRepository) 
        {
            _subPrefectureRepository = subPrefectureRepository;

            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .MustAsync(async (id, token) =>
                {
                    return await _subPrefectureRepository.ExistsAsync(x => x.Id == id);
                })
                .WithMessage("The {PropertyName} with value {PropertyValue} does not exist.");
        }
    }
}
