using MediatR;
using Microsoft.AspNetCore.Mvc;
using World.Application.DTOs.Country;
using World.Application.DTOs.Search;
using World.Application.Features.Countries.Command.CreateCountry;
using World.Application.Features.Countries.Command.DeleteCountry;
using World.Application.Features.Countries.Command.UpdateCountry;
using World.Application.Features.Countries.Query.GetCountries;
using World.Application.Features.Countrys.Query.GetCountryDetails;
using World.Application.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace World.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<CountryController>
        [HttpPost]
        public async Task<ActionResult<SearchResult<CountryListDto>>> GetCountryListPage([FromBody] SearchDTO search)
        {
            try
            {
                var Countrys = await _mediator.Send(new GetCountriesQuery() { Search = search });
                return Ok(Countrys);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET api/<CountryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryListDto>> GetCountry(Guid id)
        {
            try
            {
                var Country = await _mediator.Send(new GetCountryDetailsQuery { Id = id });
                return Ok(Country);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST api/<CountryController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseCommandResponse>> AddCountry([FromBody] CreateCountryCommand createCountry)
        {
            try
            {
                var response = await _mediator.Send(createCountry);
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT api/<CountryController>/5
        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateCountry([FromBody] UpdateCountryCommand updateCountry)
        {
            try
            {
                await _mediator.Send(updateCountry);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE api/<CountryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCountry(Guid id)
        {
            try
            {
                var command = new DeleteCountryCommand { Id = id };
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
