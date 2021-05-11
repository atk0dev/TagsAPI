namespace Application.Features.Tags.Commands.CreateTag
{
    using MediatR;

    public class CreateTagCommand : IRequest<CreateTagCommandResponse>
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public bool SelfAssign { get; set; }
        
        public bool RequiresOnboarding { get; set; }
    }
}
