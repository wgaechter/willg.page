using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Net;
using System.Net.Mail;

//SMTP GOOGLE DOCS:
//https://developers.google.com/gmail/api/guides/sending 
//https://stackoverflow.com/questions/73482823/how-to-start-with-the-google-api-for-gmail
//https://www.daimto.com/how-to-access-gmail-with-c-net/

namespace PortfolioWebsite.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; }
        static string[] Scopes = { GmailService.Scope.GmailSend, GmailService.Scope.GmailCompose };
        static string ApplicationName = "willg-page";

        private IConfiguration Configuration;
        public ContactModel(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public void OnPostSubmit(PortfolioWebsite.Models.ContactFormModel model)
        {
            //Read SMTP settings from AppSettings.json.
            string host = this.Configuration.GetValue<string>("Smtp:Server");
            int port = this.Configuration.GetValue<int>("Smtp:Port");
            string userName = this.Configuration.GetValue<string>("Smtp:UserName");
            string password = this.Configuration.GetValue<string>("Smtp:appPassword");

            string fromAddress = model.Email;

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

                using (var smtp = new SmtpClient()) {
                    smtp.Host = host;
                    smtp.Port = port;
                    smtp.Credentials = new NetworkCredential(userName, password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = true;

                    try
                    {
                        /* Send method called below is what will send off our email 
                         * unless an exception is thrown.
                         */
                        smtp.Send(mm);
                    }
                    catch (SmtpException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }

        }
    }
}
