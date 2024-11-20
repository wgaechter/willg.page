using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace PortfolioWebsite.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; }

        private IConfiguration Configuration;
        public ContactModel(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public void OnGet()
        {

        }

        public void OnPostSubmit(PortfolioWebsite.Models.ContactFormModel model)
        {
            //Read SMTP settings from AppSettings.json.
            string host = this.Configuration.GetValue<string>("Smtp:Server");
            int port = this.Configuration.GetValue<int>("Smtp:Port");
            string fromAddress = this.Configuration.GetValue<string>("Smtp:FromAddress");
            string userName = this.Configuration.GetValue<string>("Smtp:UserName");
            string password = this.Configuration.GetValue<string>("Smtp:Password");

            using (MailMessage mm = new MailMessage(fromAddress, "admin@aspsnippets.com"))
            {
                mm.Subject = model.Subject;
                mm.Body = "Name: " + model.Name + "<br /><br />Company: " + model.Company + "<br /><br />Email: " + model.Email + "<br />" + model.Body;
                mm.IsBodyHtml = true;

                if (model.Attachment.Length > 0)
                {
                    string fileName = Path.GetFileName(model.Attachment.FileName);
                    mm.Attachments.Add(new Attachment(model.Attachment.OpenReadStream(), fileName));
                }

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = host;
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(userName, password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = port;
                    smtp.Send(mm);
                    this.Message = "Email sent sucessfully.";
                }
            }
        }
    }
}
