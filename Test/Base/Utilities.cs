using System.Linq;

namespace IntegrationTests.Base
{
    using System;
    using Domain.Entities;
    using Persistence.Services;

    public class Utilities
    {
        public static void InitializeDbForTests(BaseService<Tag> tagService)
        {
            var allTags = tagService.Get().Result.ToList();
            foreach (var tag in allTags)
            {
                tagService.Remove(tag.Id).Wait();
            }

            tagService.Create(new Tag
            {
                Id = Data.Tag1Id,
                CreatedBy = Data.TagAuthor,
                CreatedDate = DateTime.UtcNow,
                LastModifiedBy = Data.TagAuthor,
                LastModifiedDate = DateTime.UtcNow,
                Name = Data.Tag1Name
            })
                    .Wait();

            tagService.Create(new Tag
            {
                Id = Data.Tag2Id,
                CreatedBy = Data.TagAuthor,
                CreatedDate = DateTime.UtcNow,
                LastModifiedBy = Data.TagAuthor,
                LastModifiedDate = DateTime.UtcNow,
                Name = Data.Tag2Name
            })
                    .Wait();
        }

        public static void CleanDbAfterTest(BaseService<Tag> tagService)
        {
            var allTags = tagService.Get().Result.ToList();
            foreach (var tag in allTags)
            {
                tagService.Remove(tag.Id).Wait();
            }
        }
    }
}
