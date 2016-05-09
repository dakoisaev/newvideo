using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PluralsightDownloader
{
    public class Course
    {
        [JsonProperty("modules")]
        public List<Module> Modules { get; set; }

        [JsonIgnore]
        public string Title { get; set; }

        [JsonIgnore]
        public string OriginalTitle { get; set; }

        public static Course CreateCourseFromJson(string json)
        {
            try
            {
                json = json.Replace(@"\n", "");
                var j = JObject.Parse(@"{""modules"": " + json + "}");
                var c = j.ToObject<Course>();
                return c;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
            return null;
        }
    }
}
