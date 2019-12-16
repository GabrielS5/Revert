using API.Services;
using Application.Services;
using Core.Entities;
using Core.Services;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class RecordsServiceShould
    {
        private readonly IRecordsService recordsService;
        protected readonly Mock<ITranslateService> badTranslateService;
        protected readonly Mock<IKeywordsService> badKeywordsService;

        public RecordsServiceShould()
        {
            var goodMockSettings = new Mock<IOptions<ExternalApisSettings>>();
            goodMockSettings.SetupGet(g => g.Value).Returns(new ExternalApisSettings
            {
                TranslateApiKey = "QQdBqnzEGB_yWgh_xx9lTNYDptLBwiPpSudeSrEbn_rH",
                TranslateApiUri = "https://gateway-lon.watsonplatform.net/language-translator/api/v3/translate?version=2018-05-01",
                KeywordsApiKey = "ORjN9p5UXZf8RlHBc7pSmkWM5xra3XWZha82GQrgs84",
                KeywordsApiUri = "https://apis.paralleldots.com/v4/keywords",
                UseAlgorithmiaKeywords = false
            });

            badTranslateService = new Mock<ITranslateService>();
            badTranslateService.Setup(s => s.Translate(It.IsAny<string>())).ReturnsAsync(string.Empty);
            badKeywordsService = new Mock<IKeywordsService>();
            badKeywordsService.Setup(s => s.CollectKeywords(It.IsAny<string>())).ReturnsAsync(new List<Keyword>());

            recordsService = new RecordsService(null, new TranslateService(goodMockSettings.Object), new KeywordsService(goodMockSettings.Object));
        }

        [Fact]
        public async Task ReturnEmptyListWhenGivenNull()
        {
            // Arrange
            var record = new Core.Entities.Record();

            // Act
            var result = await recordsService.GetKeywords(record);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task ReturnEmptyListWhenGivenBadServices()
        {
            // Arrange
            var service = new RecordsService(null, badTranslateService.Object, badKeywordsService.Object);

            // Act
            var result = await recordsService.GetKeywords(new Core.Entities.Record());

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task ReturnNegativeKeywordsWhenGivenNegativeInput()
        {
            // Arrange
            var record = new Core.Entities.Record
            {
                Nutritie = "Nu mananca carne. Nu mananca legume."
            };

            // Act
            var result = await recordsService.GetKeywords(record);

            // Assert
            Assert.All(result, r => Assert.False(r.Positive));
        }

        [Fact]
        public async Task ReturnPositiveKeywordsWhenGivenPositiveInput()
        {
            // Arrange
            var record = new Core.Entities.Record
            {
                Nutritie = "El mananca carne. El mananca legume."
            };

            // Act
            var result = await recordsService.GetKeywords(record);

            // Assert
            Assert.All(result, r => Assert.True(r.Positive));
        }

        [Fact]
        public async Task ReturnGoodOutputWhenGivenSmallInput()
        {
            // Arrange
            var record = new Core.Entities.Record
            {
                Nutritie = "Prea mult colesterol"
            };

            // Act
            var result = await recordsService.GetKeywords(record);

            // Assert
            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public async Task ReturnGoodOutputWhenGivenVerySmallInput()
        {
            // Arrange
            var record = new Core.Entities.Record
            {
                Nutritie = "Colesterol"
            };

            // Act
            var result = await recordsService.GetKeywords(record);

            // Assert
            Assert.Single(result.ToList());
        }

        [Fact]
        public async Task ReturnGoodOutputWhenGivenBigInput()
        {
            // Arrange
            var record = new Core.Entities.Record
            {
                Nutritie = "Persoanele care nu mananca dimineata,"
                + "nu stiu insa ca pierd nutrienti importanti,"
                + "cum ar fi fibre si calciu,"
                + "si ca cei care iau micul dejun sunt cu aproximativ 30 % mai putin expusi obezitatii,"
                + "au o alimentatie mai sanatoasa,"
                + "incluzand mai des in dieta fructe si legume."
            };

            // Act
            var result = await recordsService.GetKeywords(record);

            // Assert
            Assert.Equal(12, result.ToList().Count);
        }

        [Fact]
        public async Task ReturnGoodOutputWhenGivenOnlyWeight()
        {
            // Arrange
            var record = new Core.Entities.Record
            {
                Greutate = 80
            };

            // Act
            var result = await recordsService.GetKeywords(record);

            // Assert
            Assert.Empty(result.ToList());
        }

        [Fact]
        public async Task ReturnGoodOutputWhenGivenOnlyWaist()
        {
            // Arrange
            var record = new Core.Entities.Record
            {
                Talie = 75
            };

            // Act
            var result = await recordsService.GetKeywords(record);

            // Assert
            Assert.Empty(result.ToList());
        }

        [Fact]
        public async Task ReturnGoodOutputWhenGivenOnlyWeightAndWaist()
        {
            // Arrange
            var record = new Core.Entities.Record
            {
                Greutate = 80,
                Talie = 70
            };

            // Act
            var result = await recordsService.GetKeywords(record);

            // Assert
            Assert.Empty(result.ToList());
        }

        [Fact]
        public async Task ReturnGoodOutputWhenGivenComplexRecord()
        {
            // Arrange
            var record = new Core.Entities.Record
            {
                Greutate = 80,
                Talie = 70,
                Nutritie = "Colesterol",
                StareaGenerala = "Este obosit"
            };

            // Act
            var result = await recordsService.GetKeywords(record);

            // Assert
            Assert.Equal(2, result.ToList().Count);
        }
    }
}

