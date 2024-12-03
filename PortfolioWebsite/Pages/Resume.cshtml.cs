using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace PortfolioWebsite.Pages
{
    public class ResumeModel : PageModel
    {
        public string Resume { get; set; }
        public string FilePath { get; set; }

        public ResumeModel()
        {
            Resume = "William J Gaechter - Resume - Website.pdf";
            FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/lib/resume/", Resume);
        }

        public IActionResult OnGetDownloadResume()
        {
            if (System.IO.File.Exists(FilePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(FilePath);
                return File(fileBytes, "application/pdf", Resume);
            }
            else
            {
                return NotFound("File not found.");
            }
        }
    }
}
