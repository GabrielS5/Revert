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
        private readonly List<string> negativeWords = new List<string>() { "not", "don't", "no", "doesn't", "shouldn't", "isn't" };
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
            catch { }
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
                    var keywordsResponse = await response.Content.ReadAsAsync<KeywordsResponse>();

                    return keywordsResponse.Keywords.Select(k => new Keyword
                    {
                        Value = k.Name,
                        Significance = k.Significance
                    });
                }
            }
            catch { }

            return new List<Keyword>();
        }

        public IEnumerable<Keyword> ProcessKeywords(IEnumerable<Keyword> unprocessedKeywords, string name, string context)
        {
            var separatedWords = context.Split(" ").Select(w => string.Concat(w.Where(c => !char.IsPunctuation(c)))).ToList();
            bool ComputeIsPositive(string keyword)
            {
                try
                {
                    var keywordIndex = separatedWords.IndexOf(keyword);
                    var surroundingWords = new List<string>();

                    if (keywordIndex > 0)
                    {
                        surroundingWords.Add(separatedWords[keywordIndex - 1].ToLower());
                    }
                    if (keywordIndex > 1)
                    {
                        surroundingWords.Add(separatedWords[keywordIndex - 2].ToLower());
                    }
                    if (keywordIndex > 2)
                    {
                        surroundingWords.Add(separatedWords[keywordIndex - 3].ToLower());
                    }

                    return surroundingWords.Intersect(negativeWords).ToList().Count == 0;
                }
                catch
                {
                    return true;
                }
            }

            var processedKeywords = unprocessedKeywords.SelectMany(k => k.Value.Split(" ").Select(word => new Keyword
            {
                Significance = k.Significance,
                Name = name,
                Value = word,
                Positive = ComputeIsPositive(word)
            }));

            return processedKeywords;
        }
    }
}
