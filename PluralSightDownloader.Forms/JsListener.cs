using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace PluralsightDownloader.Forms
{
    public class JsListener
    {
        public Action<string> LoadAction { get; set; }

        public void ReceiveClipUrl(string url)
        {
            Debug.Write(url); 
            LoadAction?.Invoke(url);
        }

        public void Ko(string data)
        {
            Debug.Write(data);
            var obj = JsonConvert.DeserializeObject<ResponseObj>(data);
            LoadAction?.Invoke(obj.ResponseText);
        }
    }

    public class ResponseObj
    {
        [JsonProperty("responseText")]
        public string ResponseText { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }
}
