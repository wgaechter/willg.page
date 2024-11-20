using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PortfolioWebsite.Models
{
    public class ContactFormModel
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public IFormFile Attachment { get; set; }
    }
}
