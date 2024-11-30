using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioWebsite.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioWebsite.Pages
{
    public class NewArticleFormModel : PageModel
    {
        //https://www.learnrazorpages.com/razor-pages/forms
        //https://stackoverflow.com/questions/30502423/how-to-upload-an-image-on-one-server-and-save-it-on-another-using-asp-net
        //https://www.learnrazorpages.com/razor-pages/forms/file-upload

        private readonly SQLiteContext _context;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        public List<ArticleModel> _articles;

        public NewArticleFormModel(SQLiteContext context, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _context = context;
            _configuration = configuration;
            _environment = environment;
            _articles = _context.Articles.OrderByDescending(r => r.DatePosted).ToList();
        }

        [BindProperty]
        public IFormFile Upload {  get; set; }

        public async Task OnPostAsync()
        {
            var title = Request.Form["Title"];
            var subtitle = Request.Form["Subtitle"];
            var content = Request.Form["Content"];
            var file = Path.Combine(_environment.ContentRootPath, "wwwroot/lib/static/img/", Upload.FileName);

            //Get Image Upload
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }

            long uploadTime = DateTime.Now.Ticks;

            ArticleModel newArticle = new ArticleModel(null, title, subtitle, content, uploadTime, Upload.FileName);
            DatabaseUpdateService databaseUpdater = new DatabaseUpdateService(_context, _configuration);
            await databaseUpdater.SendNewArticleAsync(newArticle);
        }

        public void OnPostUpdate()
        {
            
        }

        public void OnPostDelete(string article)
        {
            var query = _context.Articles.Where(a => a.Title == article).FirstOrDefault();

            if (query != null)
            {
                string img = Path.Combine(_environment.ContentRootPath, "wwwroot/lib/static/img/", query.ImgLoc ?? string.Empty);   
                _context.Articles.Remove(query);

                //remove file from img
                if (System.IO.File.Exists(img))
                {
                    System.IO.File.Delete(img);
                }

                _context.SaveChanges();
            }
            else
            {
                
            }

            RedirectToPage();
        }

        public void OnGet()
        {
        }
    }
}
