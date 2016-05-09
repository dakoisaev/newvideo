using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PluralsightDownloader
{
    public class Requestor
    {
        private Cookie _cookie;
        public string DocumentCookie { get; set; }

        public Cookie GetPSMCookie(string documentCookie)
        {
            Cookie cookie = null;

            var cookies = DocumentCookie.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var ck in cookies)
            {
                var ckr = ck.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                var name = ckr[0].Trim();
                var value = ckr.Length == 1 ? "" : ckr[1].Trim();
                if (name != "PSM") continue;
                cookie = new Cookie(name, value) { Domain = Urls.DOMAIN };
                break;
            }

            return cookie;
        }

        public async Task<string> GetResponseAsync(string json, string url, string method = "POST")
        {
            string result;
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.ContentType = "application/json;charset=UTF-8";
            req.Method = method;
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36";
            req.Accept = "application/json, text/plain, */*";

            req.CookieContainer = new CookieContainer();
            if (_cookie == null && !string.IsNullOrEmpty(DocumentCookie))
            {
                _cookie = GetPSMCookie(DocumentCookie);
            }
            if (_cookie == null) return null;
            req.CookieContainer.Add(_cookie);

            if (!string.IsNullOrEmpty(json))
            {
                var res = await req.GetRequestStreamAsync();
                using (var streamWriter = new StreamWriter(res))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
            }

            var httpResponse = (HttpWebResponse)await req.GetResponseAsync();
            var rs = httpResponse.GetResponseStream();
            if (rs == null) return null;
            using (var sr = new StreamReader(rs))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }
    }
}
