namespace Application.Features.Tags.Queries.GetTagDetail
{
    using System;
    using MediatR;

    public class GetTagDetailQuery : IRequest<TagDetailVm>
    {
        public string Id { get; set; }
    }
}
