namespace Application.Features.Tags.Commands.DeleteTag
{
    using System;
    using MediatR;

    public class DeleteTagCommand : IRequest
    {
        public string Id { get; set; }
    }
}
