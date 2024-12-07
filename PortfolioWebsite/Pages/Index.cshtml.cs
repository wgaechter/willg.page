using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Pages
{
    public class IndexModel : PageModel
    {
        public List<ArticleModel> _articles;
        private readonly SQLiteContext _context;
        public readonly string Intro;

        public IndexModel(SQLiteContext context)
        {
            _context = context;
            _articles = GetArticles();

            Intro = System.IO.File.ReadAllText("wwwroot/lib/static/txt/Intro.txt");
        }

        public List<ArticleModel> GetArticles()
        {
            List<ArticleModel> articles = new List<ArticleModel>();
            articles = _context.Articles.OrderByDescending(r => r.DatePosted).ToList();
            return articles;
        }

        public void OnGet()
        {

        }
    }
}
