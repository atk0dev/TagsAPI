namespace Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Features.Tags.Commands.CreateTag;
    using Application.Features.Tags.Commands.DeleteTag;
    using Application.Features.Tags.Commands.UpdateTag;
    using Application.Features.Tags.Queries.GetTagDetail;
    using Application.Features.Tags.Queries.GetTagsList;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("all", Name = "GetAllTags")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TagListVm>>> GetAllTags()
        {
            var dtos = await this._mediator.Send(new GetTagsListQuery());
            return this.Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetTagById")]
        public async Task<ActionResult<TagDetailVm>> GetTagById(string id)
        {
            var getTagDetailQuery = new GetTagDetailQuery() { Id = id };
            return this.Ok(await this._mediator.Send(getTagDetailQuery));
        }

        [HttpPost(Name = "AddTag")]
        public async Task<ActionResult<CreateTagCommandResponse>> Create([FromBody] CreateTagCommand createTagCommand)
        {
            var response = await this._mediator.Send(createTagCommand);
            return this.Ok(response);
        }

        [HttpPut(Name = "UpdateTag")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateTagCommand updateTagCommand)
        {
            await this._mediator.Send(updateTagCommand);
            return this.NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteTag")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(string id)
        {
            var deleteTagCommand = new DeleteTagCommand() { Id = id };
            await this._mediator.Send(deleteTagCommand);
            return this.NoContent();
        }
    }
}
