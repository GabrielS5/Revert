using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.Entities.Models
{
    public class KeywordsResponse
    {
        [JsonProperty("keywords")]
        public IEnumerable<KeywordsItem> Keywords { get; set; }
    }

    public class KeywordsItem
    {
        [JsonProperty("keyword")]
        public string Name { get; set; }

        [JsonProperty("confidence_score")]
        public double Significance { get; set; }
    }
}
