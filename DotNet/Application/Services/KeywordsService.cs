using Core.Entities;
using Core.Entities.Models;
using Core.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Application.Services
{
    public class KeywordsService : IKeywordsService
    {
        private HttpClient _client;
        private string _apiKey;

        public KeywordsService(IOptions<ExternalApisSettings> settings)
        {
            try
            {
                _apiKey = settings.Value.KeywordsApiKey;
                _client = new HttpClient();
                _client.BaseAddress = new Uri(settings.Value.KeywordsApiUri);
                _client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            catch
            {

            }
        }

        public async Task<IEnumerable<Keyword>> CollectKeywords(string text)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("text", text),
                new KeyValuePair<string, string>("api_key", _apiKey)
            });
                var response = await _client.PostAsync("", formContent);

                if (response.IsSuccessStatusCode)
                {
                    var dadao = await response.Content.ReadAsStringAsync();
                    var keywordsResponse = await response.Content.ReadAsAsync<KeywordsResponse>();

                    return keywordsResponse.Keywords.Select(k => new Keyword
                    {
                        Value = k.Name,
                        Significance = k.Significance
                    });
                }
                else
                {
                    return new List<Keyword>();
                }
            }
            catch
            {
                return new List<Keyword>();
            }
        }
    }
}
