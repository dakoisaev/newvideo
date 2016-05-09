namespace PluralsightDownloader
{
    public static class Urls
    {
        public const string DOMAIN = "pluralsight.com";
        public static string BaseUrl = $"http://www.{DOMAIN}/";
        //public static string LoadUrl = $"{BaseUrl}tags?pageSize=48&sortOrder=size";
        //public static string LoadUrl = $"http://app.{DOMAIN}/id?";
        public static string LoadUrl = $"http://app.{DOMAIN}/library";
        public static string VideoUrl = $"{BaseUrl}training/player/viewclip/";
        public static string CourseDataUrl = $"{BaseUrl}data/course/content/";
        public static string CourseUrl = $"https://app.{DOMAIN}/library/courses/";
    }
}
