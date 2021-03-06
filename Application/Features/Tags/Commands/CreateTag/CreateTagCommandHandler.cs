namespace Application.Features.Tags.Commands.CreateTag
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Contracts.Persistence;
    using AutoMapper;
    using Domain.Entities;
    using MediatR;

    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, CreateTagCommandResponse>
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public CreateTagCommandHandler(IMapper mapper, IBaseService<Tag> service, ITagService tagService)
        {
            this._mapper = mapper;
            this._tagService = tagService;
        }

        public async Task<CreateTagCommandResponse> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateTagCommandResponse();

            var validator = new CreateTagCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (response.Success)
            {
                var tag = new Tag()
                {
                    Name = request.Name,
                    Description = request.Description,
                    SelfAssign = request.SelfAssign,
                    RequiresOnboarding = request.RequiresOnboarding
                };

                tag = await this._tagService.CreateTag(tag);
                response.Tag = this._mapper.Map<CreateTagDto>(tag);
            }

            return response;
        }
    }
}
