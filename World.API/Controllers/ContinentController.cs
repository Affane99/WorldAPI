using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using World.Application.DTOs.Continent;
using World.Application.DTOs.Search;
using World.Application.Features.Continents.Requests.Commands;
using World.Application.Features.Continents.Requests.Queries;
using World.Application.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace World.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContinentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContinentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<ContinentController>
        [HttpPost]
        public async Task<ActionResult<SearchResult<ContinentListDto>>> GetContinentListPage([FromBody] SearchDTO search)
        {
            try
            {
                var continents = await _mediator.Send(new GetContinentListRequest() { Search = search });
                return Ok(continents);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET api/<ContinentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContinentListDto>> GetContinent(Guid id)
        {
            try
            {
                var continent = await _mediator.Send(new GetContinentDetailsRequest { Id = id });
                return Ok(continent);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST api/<ContinentController>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseCommandResponse>> AddContinent([FromBody] CreateContinentDto createContinentDto)
        {
            try
            {
                var command = new CreateContinentCommandRequest { CreateContinent = createContinentDto };
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT api/<ContinentController>/5
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseCommandResponse>> UpdateContinent([FromBody] UpdateContinentDto updateContinentDto)
        {
            try
            {
                var command = new UpdateContinentComamndRequest { UpdateContinent = updateContinentDto };
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE api/<ContinentController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContinent(Guid id)
        {
            try
            {
                var command = new DeleteContinentCommandRequest { Id = id };
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
