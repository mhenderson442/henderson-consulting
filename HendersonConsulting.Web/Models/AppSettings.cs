namespace HendersonConsulting.Web.Models
{
    public class AppSettings
    {
        public string StorageConnectionString { get; set; }
        public string StorageAccountName { get; set; }
        public string StorageAccountKey { get; set; }
        public string BlogPostContainer { get; set; }
        public string ImagesContainer { get; set; }
        public string StaticContainer { get; set; }
    }
}
