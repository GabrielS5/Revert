using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class InMemoryGetApiKeyKeywordsQuery : IGetApiKeyQuery
    {
        private readonly IDictionary<string, ApiKey> _apiKeys;

        public InMemoryGetApiKeyKeywordsQuery()
        {
            var existingApiKeys = new List<ApiKey>
        {
            new ApiKey(1, "Finance", "C5BFF7F0-B4DF-475E-A331-F737424F013C", new DateTime(2019, 01, 01),
                new List<string>
                {
                })
        };

            _apiKeys = existingApiKeys.ToDictionary(x => x.Key, x => x);
        }

        public Task<ApiKey> Execute(string providedApiKey)
        {
            _apiKeys.TryGetValue(providedApiKey, out var key);
            return Task.FromResult(key);
        }
    }

    public class InMemoryGetApiKeyClusteringQuery : IGetApiKeyQuery
    {
        private readonly IDictionary<string, ApiKey> _apiKeys;

        public InMemoryGetApiKeyClusteringQuery()
        {
            var existingApiKeys = new List<ApiKey>
        {
            new ApiKey(1, "Finance", "C5BFF7F0-B4DF-475E-A331-F737424F013E", new DateTime(2019, 01, 01),
                new List<string>
                {
                })
        };

            _apiKeys = existingApiKeys.ToDictionary(x => x.Key, x => x);
        }

        public Task<ApiKey> Execute(string providedApiKey)
        {
            _apiKeys.TryGetValue(providedApiKey, out var key);
            return Task.FromResult(key);
        }
    }

    public class InMemoryGetApiKeyMainQuery : IGetApiKeyQuery
    {
        private readonly IDictionary<string, ApiKey> _apiKeys;

        public InMemoryGetApiKeyMainQuery()
        {
            var existingApiKeys = new List<ApiKey>
        {
            new ApiKey(1, "Finance", "C5BFF7F0-B4DF-475E-A331-F737424F013D", new DateTime(2019, 01, 01),
                new List<string>
                {
                })
        };

            _apiKeys = existingApiKeys.ToDictionary(x => x.Key, x => x);
        }

        public Task<ApiKey> Execute(string providedApiKey)
        {
            _apiKeys.TryGetValue(providedApiKey, out var key);
            return Task.FromResult(key);
        }
    }
}
