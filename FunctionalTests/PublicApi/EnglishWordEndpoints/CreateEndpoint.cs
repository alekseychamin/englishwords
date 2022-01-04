using ApplicationCore.Extensions;
using PublicApi.Endpoints.EnglishWords;
using System;
using System.Collections.Generic;
using System.Linq;
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

            StringContent content = new StringContent(request.ToJson(), Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PostAsync("api/EnglishWords", content);
            response.EnsureSuccessStatusCode();
            
            var stringResponse = await response.Content.ReadAsStringAsync();
            
            // Assert

        }
    }
}
