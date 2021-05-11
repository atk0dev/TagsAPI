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
        private readonly IBaseService<Tag> _service;
        private readonly ITagService tagSevice;
        private readonly IMapper _mapper;

        public CreateTagCommandHandler(IMapper mapper, IBaseService<Tag> service, ITagService tagSevice)
        {
            this._mapper = mapper;
            this._service = service;
            this.tagSevice = tagSevice;
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
                var tag = new Tag() { Name = request.Name };
                tag = await this._service.Create(tag);
                response.Tag = this._mapper.Map<CreateTagDto>(tag);
            }

            return response;
        }
    }
}
