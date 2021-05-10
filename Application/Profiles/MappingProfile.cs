namespace Application.Profiles
{
    using Application.Features.Tags.Commands.CreateTag;
    using Application.Features.Tags.Commands.UpdateTag;
    using Application.Features.Tags.Queries.GetTagDetail;
    using Application.Features.Tags.Queries.GetTagsList;
    using AutoMapper;
    using Domain.Entities;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Tag, TagListVm>();

            this.CreateMap<Tag, CreateTagCommand>();

            this.CreateMap<Tag, CreateTagDto>();

            this.CreateMap<Tag, UpdateTagCommand>().ReverseMap();

            this.CreateMap<Tag, TagDetailVm>().ReverseMap();
        }
    }
}
