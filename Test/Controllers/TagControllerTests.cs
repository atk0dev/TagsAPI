using System;
using Api;
using IntegrationTests.Base;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Tags.Commands.CreateTag;
using Application.Features.Tags.Queries.GetTagDetail;
using Application.Features.Tags.Queries.GetTagsList;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Services;
using Xunit;

namespace IntegrationTests.Controllers
{

    public class TagControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>, IDisposable
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public TagControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;

            var baseService = _factory.Services.GetRequiredService(typeof(BaseService<Tag>)) as BaseService<Tag>;
            Utilities.InitializeDbForTests(baseService);
        }

        [Fact]
        public async Task GetAllTags_ReturnsSuccessResult()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/tag/all");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<TagListVm>>(responseString);
            
            Assert.IsType<List<TagListVm>>(result);
            Assert.NotEmpty(result);

            Assert.True(result.Count >= 2);
            Assert.True(result.FirstOrDefault(t => t.Id.Equals(Data.Tag1Id))?.Name == Data.Tag1Name);
            Assert.True(result.FirstOrDefault(t => t.Id.Equals(Data.Tag2Id))?.Name == Data.Tag2Name);
        }

        [Fact]
        public async Task GetTagById_ReturnsSuccessResult()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync($"/api/tag/{Data.Tag1Id}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<TagDetailVm>(responseString);

            Assert.IsType<TagDetailVm>(result);
            Assert.NotNull(result);

            Assert.True(result.Name.Equals(Data.Tag1Name));
        }

        [Fact]
        public async Task AddTag_ReturnsNewTag()
        {
            var client = _factory.GetAnonymousClient();

            string newTagName = "Test tag";

            var newTag = new Tag
            {
                CreatedBy = Data.TagAuthor,
                LastModifiedBy = Data.TagAuthor,
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
                Name = newTagName
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(newTag), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/tag", stringContent);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CreateTagCommandResponse>(responseString);

            Assert.IsType<CreateTagCommandResponse>(result);
            Assert.NotNull(result);

            Assert.True(result.Tag.Name.Equals(newTagName));
        }

        [Fact]
        public async Task UpdateTag_ReturnsNewTag()
        {
            var client = _factory.GetAnonymousClient();

            var updatedTag = new Tag
            {
                Id = Data.Tag1Id,
                Name = "Updated Tag Name",
                LastModifiedBy = Data.TagAuthor,
                LastModifiedDate = DateTime.UtcNow,
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(updatedTag), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/tag", stringContent);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task DeleteTag_ReturnsNewTag()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.DeleteAsync($"/api/tag/{Data.Tag2Id}");

            response.EnsureSuccessStatusCode();
        }


        public void Dispose()
        {
            var baseService = _factory.Services.GetRequiredService(typeof(BaseService<Tag>)) as BaseService<Tag>;
            Utilities.CleanDbAfterTest(baseService);
        }
    }
}
