using System.Collections.Generic;
using Newtonsoft.Json;

namespace PluralsightDownloader
{
    public class Module
    {
        [JsonProperty("userMayViewFirstClip")]
        public bool UserMayViewFirstClip { get; set; }

        [JsonProperty("moduleRef")]
        public string ModuleRef { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("hasBeenViewed")]
        public bool HasBeenViewed { get; set; }

        [JsonProperty("isHightlighted")]
        public bool IsHightlighted { get; set; }

        [JsonProperty("isBookmarked")]
        public bool IsBookMarked { get; set; }

        [JsonProperty("clips")]
        public List<Clip> Clips { get; set; }

        [JsonIgnore]
        public string Position { get; set; }

    }
}
