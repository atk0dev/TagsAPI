namespace Application.Features.Tags.Commands.CreateTag
{
    using MediatR;

    public class CreateTagCommand : IRequest<CreateTagCommandResponse>
    {
        public string Name { get; set; }
    }
}
