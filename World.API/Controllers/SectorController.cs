using MediatR;
using Microsoft.AspNetCore.Mvc;
using World.Application.DTOs.Sector;
using World.Application.DTOs.Search;
using World.Application.Features.Sector.Command.CreateSector;
using World.Application.Features.Sector.Command.DeleteSector;
using World.Application.Features.Sector.Command.UpdateSector;
using World.Application.Features.Sector.Query.GetSector;
using World.Application.Features.Sector.Query.GetSectorDetails;
using World.Application.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace World.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SectorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<SectorController>
        [HttpPost]
        public async Task<ActionResult<SearchResult<SectorDto>>> GetSectorListPage([FromBody] SearchDTO search)
        {
            try
            {
                var Sectors = await _mediator.Send(new GetSectorQuery() { Search = search });
                return Ok(Sectors);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET api/<SectorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SectorDto>> GetSector(Guid id)
        {
            try
            {
                var Sector = await _mediator.Send(new GetSectorDetailsQuery { Id = id });
                return Ok(Sector);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST api/<SectorController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseCommandResponse>> AddSector([FromBody] CreateSectorCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT api/<SectorController>/5
        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateSector([FromBody] UpdateSectorCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE api/<SectorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSector(Guid id)
        {
            try
            {
                var command = new DeleteSectorCommand { Id = id };
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
