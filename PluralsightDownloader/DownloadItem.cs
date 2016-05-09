namespace PluralsightDownloader
{
    public class DownloadItem
    {
        public Clip Clip { get; set; }
        public Module Module { get; set; }

        public DownloadItem()
        {
            
        }
        public DownloadItem(Clip clip, Module module = null)
        {
            Clip = clip;
            Module = module;
        }
    }
}
