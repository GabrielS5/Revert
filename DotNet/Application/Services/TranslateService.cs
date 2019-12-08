using Core.Entities;
using Core.Entities.Models;
using Core.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TranslateService : ITranslateService
    {
        private HttpClient _client;

        public TranslateService(IOptions<ExternalApisSettings> settings)
        {
            try
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri(settings.Value.TranslateApiUri);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{"apikey"}:{settings.Value.TranslateApiKey}")));
                _client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            catch
            {

            }
        }

        public async Task<string> Translate(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    return string.Empty;
                }

                var requestModel = new TranslateRequest
                {
                    Text = text,
                    ModelId = "ro-en"
                };
                var response = await _client.PostAsync("",
                    new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var translationResponse = await response.Content.ReadAsAsync<TranslateResponse>();
                    return translationResponse.Translations.First().Translation;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
