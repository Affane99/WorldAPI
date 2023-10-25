using MediatR;
using Microsoft.AspNetCore.Mvc;
using World.Application.DTOs.SubPrefecture;
using World.Application.DTOs.Search;
using World.Application.Features.SubPrefecture.Command.CreateSubPrefecture;
using World.Application.Features.SubPrefecture.Command.DeleteSubPrefecture;
using World.Application.Features.SubPrefecture.Command.UpdateSubPrefecture;
using World.Application.Features.SubPrefecture.Query.GetSubPrefecture;
using World.Application.Features.SubPrefecture.Query.GetSubPrefectureDetails;
using World.Application.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace World.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubPrefectureController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubPrefectureController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<SubPrefectureController>
        [HttpPost]
        public async Task<ActionResult<SearchResult<SubPrefectureDto>>> GetSubPrefectureListPage([FromBody] SearchDTO search)
        {
            try
            {
                var SubPrefectures = await _mediator.Send(new GetSubPrefectureQuery() { Search = search });
                return Ok(SubPrefectures);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET api/<SubPrefectureController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubPrefectureDto>> GetSubPrefecture(Guid id)
        {
            try
            {
                var SubPrefecture = await _mediator.Send(new GetSubPrefectureDetailsQuery { Id = id });
                return Ok(SubPrefecture);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST api/<SubPrefectureController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseCommandResponse>> AddSubPrefecture([FromBody] CreateSubPrefectureCommand command)
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

        // PUT api/<SubPrefectureController>/5
        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateSubPrefecture([FromBody] UpdateSubPrefectureCommand command)
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

        // DELETE api/<SubPrefectureController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubPrefecture(Guid id)
        {
            try
            {
                var command = new DeleteSubPrefectureCommand { Id = id };
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
