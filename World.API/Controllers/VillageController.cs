using MediatR;
using Microsoft.AspNetCore.Mvc;
using World.Application.DTOs.Village;
using World.Application.DTOs.Search;
using World.Application.Features.Village.Command.CreateVillage;
using World.Application.Features.Village.Command.DeleteVillage;
using World.Application.Features.Village.Command.UpdateVillage;
using World.Application.Features.Village.Query.GetVillage;
using World.Application.Features.Village.Query.GetVillageDetails;
using World.Application.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace World.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VillageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VillageController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<VillageController>
        [HttpPost]
        public async Task<ActionResult<SearchResult<VillageDto>>> GetVillageListPage([FromBody] SearchDTO search)
        {
            try
            {
                var Villages = await _mediator.Send(new GetVillageQuery() { Search = search });
                return Ok(Villages);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET api/<VillageController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VillageDto>> GetVillage(Guid id)
        {
            try
            {
                var Village = await _mediator.Send(new GetVillageDetailsQuery { Id = id });
                return Ok(Village);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST api/<VillageController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseCommandResponse>> AddVillage([FromBody] CreateVillageCommand command)
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

        // PUT api/<VillageController>/5
        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateVillage([FromBody] UpdateVillageCommand command)
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

        // DELETE api/<VillageController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVillage(Guid id)
        {
            try
            {
                var command = new DeleteVillageCommand { Id = id };
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
