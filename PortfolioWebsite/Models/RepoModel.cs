using System.Reflection.Metadata;
using Octokit;

namespace PortfolioWebsite.Models
{
    public class RepoModel
    {
        public int Id { get; set; }
        public long GithubId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IReadOnlyList<RepositoryLanguage> LanguageList { get; set; }
        public string LanguageListBlob { get; set; }
        public string HtmlUrl { get; set; }

        public RepoModel(int id, long githubId, string name, string description, IReadOnlyList<RepositoryLanguage> languageList, string languageListBlob, string htmlUrl)
        {
            Id = id;
            GithubId = githubId;
            Name = name;
            Description = description;
            LanguageList = languageList;
            LanguageListBlob = languageListBlob;
            HtmlUrl = htmlUrl;
        }
    }
}
