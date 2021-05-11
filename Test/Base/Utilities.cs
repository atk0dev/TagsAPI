namespace IntegrationTests.Base
{
    using System;
    using System.Linq;
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
                CreatedByID = Data.TagAuthor,
                CreatedDate = DateTime.UtcNow,
                UpdatedByID = Data.TagAuthor,
                UpdatedDate = DateTime.UtcNow,
                Name = Data.Tag1Name
            })
                    .Wait();

            tagService.Create(new Tag
            {
                Id = Data.Tag2Id,
                CreatedByID = Data.TagAuthor,
                CreatedDate = DateTime.UtcNow,
                UpdatedByID = Data.TagAuthor,
                UpdatedDate = DateTime.UtcNow,
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
