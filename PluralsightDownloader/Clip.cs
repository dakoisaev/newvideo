using System;
using System.Collections;
using Newtonsoft.Json;

namespace PluralsightDownloader
{
    public class Clip
    {
        private string _playerParametersText;

        [JsonProperty("clipIndex")]
        public int ClipIndex { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("hasBeenViewed")]
        public string HasBeenViewed { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("playerParameters")]
        public string PlayerParametersText
        {
            get { return _playerParametersText; }
            set
            {
                _playerParametersText = value;
                SetPlayerParameters();
            }
        }

        [JsonProperty("userMayViewClip")]
        public bool UserMayViewClip { get; set; }

        [JsonProperty("isHighlighted")]
        public bool IsHighlighted { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isBookMarked")]
        public bool IsBookMarked { get; set; }

        [JsonIgnore]
        public PlayerParameters PlayerParameters { get; set; }

        [JsonIgnore]
        public string VideoUrl { get; set; }

        [JsonIgnore]
        public string VideoName { get; set; }

        [JsonIgnore]
        public string Position { get; set; }

        [JsonIgnore]
        public int DownloadProgressPercent { get; set; }

        [JsonIgnore]
        public long DownloadProgress { get; set; }

        [JsonIgnore]
        public long DownloadTotal { get; set; }

        [JsonIgnore]
        public bool IsLastInModule { get; set; }


        public Clip()
        {
            PlayerParameters = new PlayerParameters
            {
                Language = "en",
                Cap = false,
                Quality = "1024x768",
                Extension = "mp4"
            };
        }

        private void SetPlayerParameters()
        {
            var values = PlayerParametersText
                .Split(new[] { "&" }, StringSplitOptions.RemoveEmptyEntries);

            var ht = new Hashtable();

            foreach (var val in values)
            {
                var o = val.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                ht.Add(o[0], o[1]);
            }
            PlayerParameters.Author = ht["author"].ToString();
            PlayerParameters.Name = ht["name"].ToString();
            PlayerParameters.Course = ht["course"].ToString();
            PlayerParameters.Index = Convert.ToInt32(ht["clip"]);
        }


        public string ToJson()
        {
            return JsonConvert.SerializeObject(PlayerParameters);
        }

    }
}
