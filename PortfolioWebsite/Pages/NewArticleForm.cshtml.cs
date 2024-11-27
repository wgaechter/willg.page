using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Pages
{
    public class NewArticleFormModel : PageModel
    {
        //https://www.learnrazorpages.com/razor-pages/forms
        //https://stackoverflow.com/questions/30502423/how-to-upload-an-image-on-one-server-and-save-it-on-another-using-asp-net
        //https://www.learnrazorpages.com/razor-pages/forms/file-upload

        private readonly SQLiteContext _context;
        private readonly IConfiguration _configuration;

        public NewArticleFormModel(SQLiteContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task OnPostAsync()
        {
            var title = Request.Form["Title"];
            var subtitle = Request.Form["Subtitle"];
            var content = Request.Form["Content"];
            
            long uploadTime = DateTime.Now.Ticks;

            ArticleModel newArticle = new ArticleModel(null, title, subtitle, content, uploadTime, null);
            DatabaseUpdateService databaseUpdater = new DatabaseUpdateService(_context, _configuration);
            await databaseUpdater.SendNewArticleAsync(newArticle);
        }

        public void OnGet()
        {
        }
    }
}
