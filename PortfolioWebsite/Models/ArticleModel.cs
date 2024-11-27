namespace PortfolioWebsite.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        public string? ImgLoc { get; set; }

        public ArticleModel(int id, string title, string subtitle, string content, DateTime datePosted, string imgLoc)
        {
            Id = id;
            Title = title;
            Subtitle = subtitle;
            Content = content;
            DatePosted = datePosted;
            ImgLoc = imgLoc;
        }

    }

}
