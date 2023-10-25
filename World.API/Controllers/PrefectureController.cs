using MediatR;
using Microsoft.AspNetCore.Mvc;
using World.Application.DTOs.Prefecture;
using World.Application.DTOs.Search;
using World.Application.Features.Prefecture.Command.CreatePrefecture;
using World.Application.Features.Prefecture.Command.DeletePrefecture;
using World.Application.Features.Prefecture.Command.UpdatePrefecture;
using World.Application.Features.Prefecture.Query.GetPrefecture;
using World.Application.Features.Prefecture.Query.GetPrefectureDetails;
using World.Application.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace World.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PrefectureController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrefectureController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<PrefectureController>
        [HttpPost]
        public async Task<ActionResult<SearchResult<PrefectureDto>>> GetPrefectureListPage([FromBody] SearchDTO search)
        {
            try
            {
                var Prefectures = await _mediator.Send(new GetPrefectureQuery() { Search = search });
                return Ok(Prefectures);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET api/<PrefectureController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrefectureDto>> GetPrefecture(Guid id)
        {
            try
            {
                var Prefecture = await _mediator.Send(new GetPrefectureDetailsQuery { Id = id });
                return Ok(Prefecture);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST api/<PrefectureController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseCommandResponse>> AddPrefecture([FromBody] CreatePrefectureCommand command)
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

        // PUT api/<PrefectureController>/5
        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdatePrefecture([FromBody] UpdatePrefectureCommand command)
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

        // DELETE api/<PrefectureController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePrefecture(Guid id)
        {
            try
            {
                var command = new DeletePrefectureCommand { Id = id };
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
