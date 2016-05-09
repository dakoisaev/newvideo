using Newtonsoft.Json;

namespace PluralsightDownloader
{
    public class PlayerParameters
    {
        [JsonProperty("a")]
        public string Author { get; set; }

        [JsonProperty("m")]
        public string Name { get; set; }

        [JsonProperty("course")]
        public string Course { get; set; }

        [JsonProperty("cn")]
        public int Index { get; set; }

        [JsonProperty("mt")]
        public string Extension { get; set; }

        [JsonProperty("q")]
        public string Quality { get; set; }

        [JsonProperty("cap")]
        public bool Cap { get; set; }

        [JsonProperty("ln")]
        public string Language { get; set; }
    }
}
