using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Octokit;
using System.Net.WebSockets;
using System.Xml.Serialization;

namespace PortfolioWebsite.Pages
{
    public class ProjectsModel : PageModel
    {
        private readonly GitHubClient _client;

        public ProjectsModel()
        {
            _client = EstablishClient();
        }

        public GitHubClient EstablishClient()
        {
            GitHubClient client = new GitHubClient(new ProductHeaderValue("PortfolioWebsite"));
            return client;
        }

        public async Task<IReadOnlyCollection<Repository>> GetPublicRepos()
        {
            try
            {
                IReadOnlyCollection<Repository> repos = await _client.Repository.GetAllForUser("wgaechter");
                //Repos is filled now, parse and create in page

                return repos;
            }
            catch (Exception e) { System.Diagnostics.Debug.WriteLine(e.Message); return null; };
        }

        public void OnGet()
        {

        }
    }

    
}
