using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using World.Application.Contracts.Persistence;
using World.Application.Exceptions;

namespace World.Application.Features.Countries.Command.DeleteCountry
{
    public class DeleteCountryHandler : IRequestHandler<DeleteCountryCommand, Unit>
    {
        private readonly ICountryRepository _countryRepository;

        public DeleteCountryHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var validation = await new DeleteCountryValidator(_countryRepository).ValidateAsync(request);
            if (!validation.IsValid)
            {
                throw new BadRequestException("Deletion Failed", validation);
            }
            var country = await _countryRepository.FindByIdAsync(request.Id);
            if (country != null)
            {
                await _countryRepository.DeleteAsync(country);
            }
            return Unit.Value;
        }
    }
}
