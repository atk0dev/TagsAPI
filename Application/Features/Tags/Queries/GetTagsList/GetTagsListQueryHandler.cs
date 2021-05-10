namespace Application.Features.Tags.Queries.GetTagsList
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Contracts.Persistence;
    using AutoMapper;
    using Domain.Entities;
    using MediatR;

    public class GetTagsListQueryHandler : IRequestHandler<GetTagsListQuery, List<TagListVm>>
    {
        private readonly IBaseService<Tag> _service;
        private readonly IMapper _mapper;

        public GetTagsListQueryHandler(IMapper mapper, IBaseService<Tag> service)
        {
            this._mapper = mapper;
            this._service = service;
        }

        public async Task<List<TagListVm>> Handle(GetTagsListQuery request, CancellationToken cancellationToken)
        {
            var all = (await this._service.Get()).OrderBy(x => x.Name);
            return this._mapper.Map<List<TagListVm>>(all);
        }
    }
}
