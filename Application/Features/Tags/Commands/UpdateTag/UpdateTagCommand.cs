namespace Application.Features.Tags.Commands.UpdateTag
{
    using System;
    using MediatR;

    public class UpdateTagCommand : IRequest
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
