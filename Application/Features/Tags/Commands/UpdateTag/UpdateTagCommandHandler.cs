namespace Application.Features.Tags.Commands.UpdateTag
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Contracts.Persistence;
    using Application.Exceptions;
    using AutoMapper;
    using Domain.Entities;
    using MediatR;

    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand>
    {
        private readonly IBaseService<Tag> _service;
        private readonly IMapper _mapper;

        public UpdateTagCommandHandler(IMapper mapper, IBaseService<Tag> service)
        {
            this._mapper = mapper;
            this._service = service;
        }

        public async Task<Unit> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var itemToUpdate = await this._service.Get(request.Id);

            if (itemToUpdate == null)
            {
                throw new NotFoundException(nameof(Tag), request.Id);
            }

            var validator = new UpdateTagCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }

            this._mapper.Map(request, itemToUpdate, typeof(UpdateTagCommand), typeof(Tag));

            await this._service.Update(itemToUpdate.Id, itemToUpdate);

            return Unit.Value;
        }
    }
}