using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace PluralsightDownloader
{
    public class CourseDownloader
    {
        private Clip _currentDownloadingClip;
        private readonly Requestor _requestor;

        public int DownloadDelay { get; set; } = 10000;
        public bool IsFolderOrganized { get; set; }

        public string DocumentCookie
        {
            get { return _requestor.DocumentCookie; }
            set { _requestor.DocumentCookie = value; }
        }

        public string DownloadPath { get; set; }

        public event EventHandler<Clip> ClipDownloading;
        public event EventHandler<Clip> ClipDownloaded;
        public event EventHandler<Clip> ClipDownloadProgress;

        public CourseDownloader()
        {
            _requestor = new Requestor();
        }

        public bool GetCourseTitle(string courseUrl, out string title)
        {
            //http://www.pluralsight.com/training/Player?author=dan-appleman&name=technology-careers-dark-side-m2&clip=0&course=technology-careers-dark-side
            if (courseUrl.Contains("&course="))
            {
                title = Regex.Match(courseUrl, "course=(.+)$").Groups[1].Value;
                return true;
            }
            title = courseUrl.Replace(Urls.CourseUrl, "").Replace("/table-of-contents", "");
            return courseUrl.StartsWith(Urls.CourseUrl);
        }

        public async Task<Course> GetCourseInfoAsync(string title)
        {
            var json = await _requestor.GetResponseAsync(null, $"{Urls.CourseDataUrl}{title}", "GET");
            var c = Course.CreateCourseFromJson(json);
            c.OriginalTitle = title;
            c.Title = title.Replace("-", " ");
            return c;
        }

        public void EnqueueCourse(Course course, Queue<DownloadItem> queue)
        {
            foreach (var module in course.Modules)
            {
                EnqueueModule(module, queue);
            }
        }

        public void EnqueueModule(Module module, Queue<DownloadItem> queue)
        {
            foreach (var clip in module.Clips)
            {
                EnqueueClip(clip, module, queue);
            }
        }

        public void EnqueueClip(Clip clip, Module module, Queue<DownloadItem> queue)
        {
            queue.Enqueue(new DownloadItem(clip, module));
        }

        public void DownloadClip(Clip clip, Module module)
        {
            var path = GetDownloadPath(clip, module);
            if (IsFileAlreadyDownloaded(clip, path)) return;
            OnClipDownloading(clip);
            _currentDownloadingClip = clip;
            Task.Run(() =>
            {
                Thread.Sleep(DownloadDelay);
                using (var wc = new WebClient())
                {
                    wc.DownloadFileCompleted += WcOnDownloadFileCompleted;
                    wc.DownloadProgressChanged += Wc_DownloadProgressChanged;
                    wc.DownloadFileAsync(new Uri(clip.VideoUrl), path);
                }
            });

        }

        private void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            _currentDownloadingClip.DownloadProgress = e.BytesReceived;
            _currentDownloadingClip.DownloadProgressPercent = e.ProgressPercentage;
            _currentDownloadingClip.DownloadTotal = e.TotalBytesToReceive;
            ClipDownloadProgress?.Invoke(sender, _currentDownloadingClip);
        }

        private void WcOnDownloadFileCompleted(object sender, AsyncCompletedEventArgs asyncCompletedEventArgs)
        {
            OnClipDownloaded(_currentDownloadingClip);
            _currentDownloadingClip = null;
            if (sender == null) return;
            var wc = (WebClient) sender;
            wc.DownloadFileCompleted -= WcOnDownloadFileCompleted;
            wc.DownloadProgressChanged -= Wc_DownloadProgressChanged;
        }

        private bool IsFileAlreadyDownloaded(Clip clip, string path)
        {
            if (!File.Exists(path)) return false;
            var filesizeLocal = new FileInfo(path).Length;
            long filesizeServer;
            bool isOk;

            var req = WebRequest.Create(clip.VideoUrl);
            req.Method = "HEAD";
            using (var res = req.GetResponse())
            {
                isOk = long.TryParse(res.Headers.Get("Content-Length"), out filesizeServer);              
            }

            if (isOk && filesizeLocal == filesizeServer)
            {
                OnClipDownloaded(clip);
                Thread.Sleep(1000);
                return true;
            }
            File.Delete(path);
            return false;
        }

        private string Sanitize(string str)
        {
            return str.Replace(" ", "_")
                    .Replace("?", "")
                    .Replace("-", "_")
                    .Replace("!", "")
                    .Replace(":", "")
                    .Replace(";", "")
                    .Replace(",", "_")
                    .Replace("/", "_")
                    .Replace("\"", "_")
                    .Replace("'", "_")
                    .Replace("\"", "")
                    .Replace("<", "")
                    .Replace(">", "")
                    .Replace("*", "");
        }

        private string GetDownloadPath(Clip clip, Module module)
        {
            string path;

            if (IsFolderOrganized)
            {
                var modFolderTitle = $"{module.Position}_{Sanitize(module.Title)}";
                var modPath = Path.Combine(DownloadPath, modFolderTitle);

                if (!Directory.Exists(modPath))
                    Directory.CreateDirectory(modPath);

                path = Path.Combine(modPath, Sanitize($"{clip.Position}_{Sanitize(clip.Title)}.mp4"));
            }
            else
            {
                var modPos = clip.Name.LastIndexOf("-m", StringComparison.InvariantCulture);
                if (modPos == -1) modPos = clip.Name.LastIndexOf("-", StringComparison.InvariantCulture);

                var courseName = modPos == -1 ? clip.Name : clip.Name.Substring(0, modPos);
                clip.VideoName = Sanitize($"{module.Position}{clip.Position}_{courseName}_{module.Title}_{clip.Title}.mp4");
                path = Path.Combine(DownloadPath, clip.VideoName);
            }
            return path;
        }

        protected virtual void OnClipDownloaded(Clip e)
        {
            ClipDownloaded?.Invoke(this, e);
        }

        protected virtual void OnClipDownloading(Clip e)
        {
            ClipDownloading?.Invoke(this, e);
        }
    }
}
