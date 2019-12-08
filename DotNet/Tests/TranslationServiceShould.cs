using Application.Services;
using Core.Entities;
using Core.Services;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class TranslationServiceShould
    {
        private readonly ITranslateService translateService;
        protected readonly Mock<IOptions<ExternalApisSettings>> goodMockSettings;
        protected readonly Mock<IOptions<ExternalApisSettings>> badMockSettings;

        public TranslationServiceShould()
        {
            goodMockSettings = new Mock<IOptions<ExternalApisSettings>>();
            goodMockSettings.SetupGet(g => g.Value).Returns(new ExternalApisSettings
            {
                TranslateApiKey = "QQdBqnzEGB_yWgh_xx9lTNYDptLBwiPpSudeSrEbn_rH",
                TranslateApiUri = "https://gateway-lon.watsonplatform.net/language-translator/api/v3/translate?version=2018-05-01"
            });

            badMockSettings = new Mock<IOptions<ExternalApisSettings>>();
            badMockSettings.SetupGet(g => g.Value).Returns(new ExternalApisSettings
            {
                TranslateApiKey = "",
                TranslateApiUri = ""
            });

            translateService = new TranslateService(goodMockSettings.Object);
        }

        [Fact]
        public async Task ReturnEmptyStringWhenGivenEmptyString()
        {
            // Arrange
            var input = string.Empty;

            // Act
            var result = await translateService.Translate(input);

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public async Task ReturnNullWhenGivenBadSettings()
        {
            // Arrange
            var service = new TranslateService(badMockSettings.Object);

            // Act
            var result = await service.Translate("Caine");

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("Caine", "Dog")]
        [InlineData("Pisica", "Cat")]
        [InlineData("Ananas", "Pineapple")]
        [InlineData("Salut lume!", "Hello world!")]
        [InlineData("Multumesc", "Thanks")]
        [InlineData("Pacientul se simte bine", "The patient feels good")]
        [InlineData("El mananca legume, fructe si carne", "He eats vegetables, fruit and meat")]
        public async Task ReturnGoodResult(string input, string expected)
        {
            // Act
            var result = await translateService.Translate(input);

            // Assert
            Assert.Equal(expected,result);
        }
    }
}
