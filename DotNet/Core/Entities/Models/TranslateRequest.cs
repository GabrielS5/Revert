using Newtonsoft.Json;

namespace Core.Entities.Models
{
    public class TranslateRequest
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("model_id")]
        public string ModelId { get; set; }
    }
}
