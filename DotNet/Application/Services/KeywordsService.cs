using Algorithmia;
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
        private Algorithm _lemmatizerAlgorithm;
        private Algorithm _keywordsAlgorithm;
        private ExternalApisSettings _settings;

        public KeywordsService(IOptions<ExternalApisSettings> settings)
        {
            try
            {
                _settings = settings.Value;

                var client = new Client("sim9OHvqXL7QT5RBdTjbLXy4aTE1");
                _lemmatizerAlgorithm = client.algo("StanfordNLP/Lemmatizer/0.1.0");
                _lemmatizerAlgorithm.setOptions(timeout: 300);
                _keywordsAlgorithm = client.algo("cindyxiaoxiaoli/KeywordExtraction/0.3.0");
                _keywordsAlgorithm.setOptions(timeout: 300);

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
                var keywords = new List<Keyword>();
                var tasks = new List<Task<IEnumerable<Keyword>>>();
                tasks.Add(GetParallelDotsKeywords(text));
                tasks.Add(GetAlgorithmiaKeywords(text));

                var results = await Task.WhenAll(tasks);

                foreach (var result in results)
                {
                    keywords.AddRange(result);
                }

                return keywords;

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

            var clearWords = unprocessedKeywords
                .SelectMany(k => k.Value.Split(" "))
                .Select(s => s.Trim())
                .ToList();
            var lemmatizedWords = Lemmatize(string.Join(" . ", clearWords))
                .Split(".").Select(w => w.Trim())
                .Where(w => !string.IsNullOrEmpty(w))
                .ToList();


            var processedKeywords = unprocessedKeywords.SelectMany(k => k.Value.Split(" ")
                    .Select(word => new Keyword
                    {
                        Significance = k.Significance,
                        Name = name,
                        Value = lemmatizedWords.ElementAt(clearWords.IndexOf(word))
                                               .ToLower().Trim(),
                        Positive = ComputeIsPositive(word)
                    }))
                    .GroupBy(g => g.Value)
                    .Select(g => g.OrderBy(o => o.Significance));

            return processedKeywords.Select(p => p.First());
        }

        private async Task<IEnumerable<Keyword>> GetAlgorithmiaKeywords(string text)
        {
            var results = (string[])_keywordsAlgorithm.pipe<string[]>(text).result;

            return results.Select(word => new Keyword
            {
                Value = word,
                Significance = 1
            });
        }

        private async Task<IEnumerable<Keyword>> GetParallelDotsKeywords(string text)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("text", text),
                new KeyValuePair<string, string>("api_key", _settings.KeywordsApiKey)
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

            return new List<Keyword>();
        }

        private string Lemmatize(string text)
        {
            var response = _lemmatizerAlgorithm.pipe<string>(text);

            return response.result.ToString();
        }
    }
}
