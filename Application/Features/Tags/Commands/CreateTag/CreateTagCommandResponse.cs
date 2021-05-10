namespace Application.Features.Tags.Commands.CreateTag
{
    using Application.Responses;

    public class CreateTagCommandResponse : BaseResponse
    {
        public CreateTagCommandResponse() : base()
        {
        }

        public CreateTagDto Tag { get; set; }
    }
}