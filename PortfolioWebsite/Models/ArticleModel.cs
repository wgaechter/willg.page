using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    public class ArticleModel
    {
        public int? ArticleId { get; set; }
        [Key]
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Content { get; set; }
        public long DatePosted { get; set; }
        public string? ImgLoc { get; set; }

        public ArticleModel(int? id, string title, string subtitle, string content, long datePosted, string imgLoc)
        {
            ArticleId = id;
            Title = title;
            Subtitle = subtitle;
            Content = content;
            DatePosted = datePosted;
            ImgLoc = imgLoc;
        }

        internal ArticleModel()
        {

        }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleModel>().HasKey(a => a.ArticleId); // Explicitly setting the primary key }
        }

    }

}
