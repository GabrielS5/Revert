using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.Entities.Models
{
    public class TranslateResponse
    {
        [JsonProperty("translations")]
        public IEnumerable<TranslationItem> Translations { get; set; }
    }

    public class TranslationItem
    {
        [JsonProperty("translation")]
        public string Translation { get; set; }
    }
}
