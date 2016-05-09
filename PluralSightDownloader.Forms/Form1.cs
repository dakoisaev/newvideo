using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;


namespace PluralsightDownloader.Forms
{
    public partial class Form1 : Form
    {
        private readonly Uri _url;
        // ReSharper disable once NotAccessedField.Local
        private IntPtr _nativeView;
        private CourseDownloader _courseDownloader;
        private Action _action;
        private string _lastCheckedSource;
        private Queue<DownloadItem> _clipsToDownload;
        private int _secondsFromStart;

        const string LASTPATH = "lastpath.data";

        private ChromiumWebBrowser _wb;
        private JsListener _jsListener;
        private DownloadClick _currentClick;

        private enum DownloadClick
        {
            None,
            Course,
            Module,
            Clip
        }

        public Form1()
        {
            _url = new Uri(Urls.LoadUrl);
            Init();

        }
        public Form1(Uri url)
        {
            _url = url;
            Init();
        }

        public Form1(IntPtr nativeView)
        {
            Init();
            _nativeView = nativeView;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _wb.Dispose();
            Cef.Shutdown();
        }

        private void Init()
        {
            InitializeComponent();

            var cefsettings = new CefSettings
            {
                CachePath = Path.Combine(Environment.CurrentDirectory, "cachedata")
            };
            cefsettings.CefCommandLineArgs.Add("persist_session_cookies", "1");
            Cef.Initialize(cefsettings);

            _wb = new ChromiumWebBrowser(_url.ToString())
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 22),
                MinimumSize = new Size(20, 20),
                Size = new Size(679, 558),
                TabIndex = 8
            };


            pnlBrowser.Controls.Add(_wb);

            _courseDownloader = new CourseDownloader();
            _courseDownloader.ClipDownloading += _courseDownloader_ClipDownloading;
            _courseDownloader.ClipDownloaded += _courseDownloader_ClipDownloaded;
             _courseDownloader.ClipDownloadProgress += _courseDownloader_ClipDownloadProgress;
            _courseDownloader.DownloadDelay = Convert.ToInt32(numDelay.Value);

            _jsListener = new JsListener();
            _clipsToDownload = new Queue<DownloadItem>();

            treeView.AfterSelect += TreeView_AfterSelect;
            txtUrl.Enabled = false;
            btnGo.Enabled = false;
            _wb.AddressChanged += _wb_AddressChanged;
            btnDownload.Enabled = false;
        }

        private void InvokeUiThread(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var o = e.Node.Tag;
            var buttonText = string.Empty;

            var course = o as Course;
            if (course != null)
            {
                buttonText = $@"course: ""{course.Title.Replace("-", " ")}""";
                _action = () =>
               {
                   SetTooltipText($"Downloading {course.Title}");
                   _courseDownloader.EnqueueCourse(course, _clipsToDownload);
                   _currentClick = DownloadClick.Course;
                   StartDownloadingFromQueue();
               };
            }
            var module = o as Module;
            if (module != null)
            {
                buttonText = $@"module: ""{module.Title}""";
                _action = () =>
                {
                    SetTooltipText($"Downloading {module.Title}");
                    _courseDownloader.EnqueueModule(module, _clipsToDownload);
                    _currentClick = DownloadClick.Module;
                    StartDownloadingFromQueue();
                };
            }
            var clip = o as Clip;
            if (clip != null)
            {
                buttonText = $@"clip: ""{clip.Title}""";
                _action = () =>
                {
                    SetTooltipText($"Downloading {clip.Title}");
                    var parent = e.Node.Parent.Tag as Module;
                    _courseDownloader.EnqueueClip(clip, parent, _clipsToDownload);
                    _currentClick = DownloadClick.Clip;
                    StartDownloadingFromQueue();
                };
            }
            btnDownload.Text = $"Download {buttonText}";
        }

        private void DownloadClip(Clip clip, Module module = null)
        {
            InvokeUiThread(() => { progressBar.Visible = true; });

            _jsListener.LoadAction = url =>
            {
                clip.VideoUrl = url;
                _courseDownloader.DownloadClip(clip, module);
            };

            var script =
               "var xhttp = new XMLHttpRequest(); xhttp.onreadystatechange = function() { if (xhttp.readyState == 4 && xhttp.status == 200) { jslistener.receiveClipUrl(xhttp.responseText); } }; xhttp.open('POST', 'https://app.pluralsight.com/training/Player/ViewClip', true); xhttp.setRequestHeader('Content-type', 'application/json');" +
               "xhttp.send('" + clip.ToJson() + "');";

            _wb.ExecuteScriptAsync(script);
        }

        private void ProceedToNextDownload()
        {
            InvokeUiThread(() =>
            {
                progressBar.Visible = false;
                StartDownloadingFromQueue();
            });
        }

        private void StartDownloadingFromQueue()
        {
            if (_clipsToDownload.Count <= 0)
            {
                SetTooltipText(Resx.DownloadComplete);
                SetSecText("");
                SetProgress(0);
                if (_currentClick == DownloadClick.Course)
                {
                    InvokeUiThread(() =>
                    {
                        treeView.Nodes[0].ForeColor = Color.Green;
                    });
                }
                _currentClick = DownloadClick.None;
                return;
            }
            var downloadItem = _clipsToDownload.Dequeue();
            DownloadClip(downloadItem.Clip, downloadItem.Module);
        }

        private async void _wb_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            await SetDownloaderCookieAsync();
            InvokeUiThread(async () =>
            {
                txtUrl.Enabled = true;
                btnGo.Enabled = true;
                await CheckPage();
            });
        }

        private async Task CheckPage()
        {
            var url = _wb.GetBrowser().MainFrame.Url;
            if (_lastCheckedSource == url) return;
            _lastCheckedSource = url;
            txtUrl.Text = url;
            txtSeconds.Text = string.Empty;

            string title;
            if (_courseDownloader.GetCourseTitle(url, out title))
            {
                var course = await _courseDownloader.GetCourseInfoAsync(title);
                FillTreeNode(course);
                SetTooltipText($"Course {course.Title}");
            }
            btnDownload.Enabled = treeView.Nodes.Count > 0;
        }
        private void _courseDownloader_ClipDownloaded(object sender, Clip e)
        {
            var title = e.VideoName ?? e.Title;
            SetTooltipText($"{title} has been downloaded");
            SetSecText("");
            SetProgress(0); 
            InvokeUiThread(() =>
            {
                UpdateTreeView(e, treeView.Nodes[0]);
                ProceedToNextDownload();
            });
        }

        private bool UpdateTreeView(Clip clip, TreeNode parentNode)
        {
            foreach (TreeNode node in parentNode.Nodes)
            {
                if (node.Tag is Module)
                {
                    var result = UpdateTreeView(clip, node);
                    if (result) return true;
                }
                else if (node.Tag == clip)
                {
                    node.ForeColor = Color.Green;
                    if (clip.IsLastInModule)
                    {
                        parentNode.ForeColor = Color.Green;
                    }
                    return true;
                }
                
            }
            return false;
        }

        private void _courseDownloader_ClipDownloadProgress(object sender, Clip e)
        {
            var title = e.VideoName ?? e.Title;
            SetTooltipText($"Downloading {title}");
            SetSecText($"{e.DownloadProgressPercent} % - {e.DownloadProgress / 1024} of  {e.DownloadTotal / 1024} Kb");
            SetProgress(e.DownloadProgressPercent);
        }

        private void _courseDownloader_ClipDownloading(object sender, Clip e)
        {
            var title = e.VideoName ?? e.Title;
            SetTooltipText($"You're in delay time for '{title}'.");
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();            
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _secondsFromStart++;
            if (_secondsFromStart == _courseDownloader.DownloadDelay/1000)
            {
                SetSecText("");
                var timer = (System.Timers.Timer) sender;
                timer.Stop();
                timer.Close();
                timer.Dispose();
                _secondsFromStart = 0;
            }
            else
            {
                var timeToStart = _courseDownloader.DownloadDelay/1000 - _secondsFromStart;
                SetSecText($"Dowload will start in {timeToStart} seconds.");
            }
            
        }

        private void SetTooltipText(string text)
        {
            InvokeUiThread(() =>
            {
                txtTooltip.Text = text;
            });
        }

        private void SetSecText(string text)
        {
            InvokeUiThread(() =>
            {
                txtSeconds.Text = text;
            });
        }

        private void SetProgress(int value)
        {
            InvokeUiThread(() =>
            {
                progressBar.Value = value;
            });
        }

        private async Task SetDownloaderCookieAsync()
        {
            var response = await _wb.EvaluateScriptAsync("document.cookie");
            var cookie = response.Result.ToString();
            _courseDownloader.DocumentCookie = cookie;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetTooltipText(Resx.LoadingPluralsight);
            SetSecText("");
            _wb.RegisterJsObject("jslistener", _jsListener);
        }

        private void dwnButton_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            var lastPath = GetLastDownloadPath();
            if (!string.IsNullOrEmpty(lastPath)) fbd.SelectedPath = lastPath;
            if (fbd.ShowDialog() == DialogResult.Cancel) return;
            _courseDownloader.DownloadPath = fbd.SelectedPath;
            SaveLastDownloadPath(fbd.SelectedPath);
            if (_action == null)
            {
                treeView.SelectedNode = treeView.Nodes[0]; //this will set action
            }
            // ReSharper disable once PossibleNullReferenceException
            _action.Invoke();

        }

        private string GetLastDownloadPath()
        {
            return !File.Exists(LASTPATH) ? null : File.ReadAllText(LASTPATH);
        }

        private void SaveLastDownloadPath(string lastPath)
        {
            File.WriteAllText(LASTPATH, lastPath);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (!Uri.IsWellFormedUriString(txtUrl.Text, UriKind.Absolute)) return;
            _wb.Load(txtUrl.Text);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (_wb.CanGoBack)
            {
                _wb.Back();
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            _wb.Load(Urls.LoadUrl);
        }

        private void FillTreeNode(Course course)
        {
            treeView.Nodes.Clear();
            treeView.Nodes.Add(new TreeNode(course.Title, BuildModuleNodes(course.Modules).ToArray()) { Tag = course });
            CheckCollapsed();
        }

        private IEnumerable<TreeNode> BuildModuleNodes(IEnumerable<Module> modules)
        {
            return modules.Select((module, i) =>
            {
                var pos = (i + 1).ToString();
                if (pos.Length == 1) pos = $"0{pos}";

                module.Position = pos;
                return new TreeNode(module.Title, BuildClipNodes(module.Clips).ToArray()) {Tag = module};
            });
        }

        private IEnumerable<TreeNode> BuildClipNodes(IEnumerable<Clip> clips)
        {
            var enumerable = clips as Clip[] ?? clips.ToArray();
            var len = enumerable.Length;
            return enumerable.Select((clip, i) =>
            {
                var pos = (i + 1).ToString();
                if (pos.Length == 1) pos = $"0{pos}";

                clip.Position = pos;
                clip.IsLastInModule = i+1 == len;
                return new TreeNode(clip.Title) {Tag = clip};
            });
        }

        private void gitHubSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/codecoding/PluralSightDownloader");
        }

        private void numDelay_ValueChanged(object sender, EventArgs e)
        {
            _courseDownloader.DownloadDelay = Convert.ToInt32(numDelay.Value);
        }

        private void chkExpandTree_CheckedChanged(object sender, EventArgs e)
        {
            CheckCollapsed();
        }

        private void CheckCollapsed()
        {
            if (chkExpandTree.Checked)
            {
                treeView.ExpandAll();
            }
            else
            {
                if (treeView.Nodes.Count == 0) return;
                var first = treeView.Nodes[0];
                first.Expand();
                foreach (TreeNode node in first.Nodes)
                {
                    node.Collapse(true);
                }
            }
        }

        private void chkOrgInFolders_CheckedChanged(object sender, EventArgs e)
        {
              _courseDownloader.IsFolderOrganized = chkOrgInFolders.Checked;
        }
    }
}
