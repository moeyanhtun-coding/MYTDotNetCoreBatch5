namespace MYTDotNetCore.RestAPI.DataModels
{
    public class BlogDataModel
    {
        public int Id { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogAuthor { get; set; }
        public string? BlogContent { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
