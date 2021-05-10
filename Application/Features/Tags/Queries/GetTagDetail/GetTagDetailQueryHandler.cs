namespace Application.Features.Tags.Queries.GetTagDetail
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Contracts.Persistence;
    using AutoMapper;
    using Domain.Entities;
    using MediatR;

    public class GetTagDetailQueryHandler : IRequestHandler<GetTagDetailQuery, TagDetailVm>
    {
        private readonly IBaseService<Tag> _service;
        private readonly IMapper _mapper;

        public GetTagDetailQueryHandler(IMapper mapper, IBaseService<Tag> service)
        {
            this._mapper = mapper;
            this._service = service;
        }

        public async Task<TagDetailVm> Handle(GetTagDetailQuery request, CancellationToken cancellationToken)
        {
            var tag = await this._service.Get(request.Id);
            var tagDetailDto = this._mapper.Map<TagDetailVm>(tag);
            
            return tagDetailDto;
        }
    }
}
