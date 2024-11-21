using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Net;
using System.Net.Mail;
using Google.Apis.Gmail;

//SMTP GOOGLE DOCS: https://developers.google.com/gmail/api/guides/sending 
//https://stackoverflow.com/questions/73482823/how-to-start-with-the-google-api-for-gmail
//https://www.daimto.com/how-to-access-gmail-with-c-net/

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

            string domain = this.Configuration.GetValue<string>("Smtp:Domain");

            using (MailMessage mm = new MailMessage(fromAddress, userName))
            {
                mm.Subject = model.Subject;
                mm.Body = "Name: " + model.Name + "<br /><br />Company: " + model.Company + "<br /><br />Email: " + model.Email + "<br />" + model.Body;
                mm.IsBodyHtml = true;

                if (model.Attachment != null)
                {
                    string fileName = Path.GetFileName(model.Attachment.FileName);
                    mm.Attachments.Add(new Attachment(model.Attachment.OpenReadStream(), fileName));
                }

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = host;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    NetworkCredential NetworkCred = new NetworkCredential(userName, password);
                    NetworkCred.Domain = domain;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = port;
                    
                    try
                    {
                        smtp.Send(mm);
                        this.Message = "Email sent sucessfully.";
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
            }
        }
    }
}

