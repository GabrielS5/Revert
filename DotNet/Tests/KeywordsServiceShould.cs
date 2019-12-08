using Application.Services;
using Core.Entities;
using Core.Services;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class KeywordsServiceShould
    {
        private readonly IKeywordsService keywordsService;
        protected readonly Mock<IOptions<ExternalApisSettings>> goodMockSettings;
        protected readonly Mock<IOptions<ExternalApisSettings>> badMockSettings;

        public KeywordsServiceShould()
        {
            goodMockSettings = new Mock<IOptions<ExternalApisSettings>>();
            goodMockSettings.SetupGet(g => g.Value).Returns(new ExternalApisSettings
            {
                KeywordsApiKey = "ORjN9p5UXZf8RlHBc7pSmkWM5xra3XWZha82GQrgs84",
                KeywordsApiUri = "https://apis.paralleldots.com/v4/keywords"
            });

            badMockSettings = new Mock<IOptions<ExternalApisSettings>>();
            badMockSettings.SetupGet(g => g.Value).Returns(new ExternalApisSettings());

            keywordsService = new KeywordsService(goodMockSettings.Object);
        }

        [Fact]
        public async Task ReturnEmptyListWhenGivenEmptyString()
        {
            // Arrange
            var input = string.Empty;

            // Act
            var result = await keywordsService.CollectKeywords(input);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task ReturnEmptyListWhenGivenBadSettings()
        {
            // Arrange
            var service = new KeywordsService(badMockSettings.Object);

            // Act
            var result = await service.CollectKeywords("Test keywords");

            // Assert
            Assert.Empty(result);
        }

        [Theory]
        [InlineData("Useless blah", 0)]
        [InlineData("Michael finished his meal in the restaurant", 2)]
        [InlineData("The train is stopping", 1)]
        public async Task ReturnGoodResult(string input, int expectedCount)
        {
            // Act
            var result = await keywordsService.CollectKeywords(input);

            // Assert
            Assert.Equal(expectedCount, result.ToList().Count);
        }
    }
}
