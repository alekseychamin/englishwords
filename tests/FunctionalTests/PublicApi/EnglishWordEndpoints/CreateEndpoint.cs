using ApplicationCore.Extensions;
using FluentAssertions;
using PublicApi.Endpoints.EnglishWords;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTests.PublicApi.EnglishWordEndpoints
{
	public class CreateEndpoint : IClassFixture<ApiTestFixture>
    {
        public HttpClient Client { get; }

        public CreateEndpoint(ApiTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnSuccessCreatedNewEnglishWord()
        {
            // Arrange
            var request = new CreateEnglishWordRequest()
            {
                Phrase = "Phrase 01",
                Transcription = "Transcription 01",
                Translation = "Translation 01",
                Example = "Example 01"
            };

            // Act
            var content = new StringContent(request.ToJson(), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("api/EnglishWords", content);
            
            var contentResponse = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var result = JsonSerializer.Deserialize<CreateEnglishWordResult>(contentResponse, options);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().BeEquivalentTo(request, options => options.ExcludingMissingMembers());
        }
    }
}
