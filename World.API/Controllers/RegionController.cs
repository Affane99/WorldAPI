using MediatR;
using Microsoft.AspNetCore.Mvc;
using World.Application.DTOs.Region;
using World.Application.DTOs.Search;
using World.Application.Features.Region.Command.CreateRegion;
using World.Application.Features.Region.Command.DeleteRegion;
using World.Application.Features.Region.Command.UpdateRegion;
using World.Application.Features.Region.Query.GetRegion;
using World.Application.Features.Region.Query.GetRegionDetails;
using World.Application.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace World.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<RegionController>
        [HttpPost]
        public async Task<ActionResult<SearchResult<RegionDto>>> GetRegionListPage([FromBody] SearchDTO search)
        {
            try
            {
                var Regions = await _mediator.Send(new GetRegionQuery() { Search = search });
                return Ok(Regions);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET api/<RegionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegionDto>> GetRegion(Guid id)
        {
            try
            {
                var Region = await _mediator.Send(new GetRegionDetailsQuery { Id = id });
                return Ok(Region);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST api/<RegionController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseCommandResponse>> AddRegion([FromBody] CreateRegionCommand command)
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

        // PUT api/<RegionController>/5
        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateRegion([FromBody] UpdateRegionCommand command)
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

        // DELETE api/<RegionController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRegion(Guid id)
        {
            try
            {
                var command = new DeleteRegionCommand { Id = id };
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
