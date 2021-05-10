namespace Application.Features.Tags.Commands.DeleteTag
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Contracts.Persistence;
    using Application.Exceptions;
    using AutoMapper;
    using Domain.Entities;
    using MediatR;

    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
    {
        private readonly IBaseService<Tag> _servcie;
        
        public DeleteTagCommandHandler(IMapper mapper, IBaseService<Tag> servcie)
        {
            this._servcie = servcie;
        }

        public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var itemToDelete = await this._servcie.Get(request.Id);

            if (itemToDelete == null)
            {
                throw new NotFoundException(nameof(Tag), request.Id);
            }

            await this._servcie.Remove(itemToDelete.Id);

            return Unit.Value;
        }
    }
}
