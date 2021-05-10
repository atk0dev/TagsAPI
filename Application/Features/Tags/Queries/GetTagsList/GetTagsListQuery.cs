namespace Application.Features.Tags.Queries.GetTagsList
{
    using System.Collections.Generic;
    using MediatR;

    public class GetTagsListQuery : IRequest<List<TagListVm>>
    {
    }
}
