using Octokit;
using PortfolioWebsite.Models;

public interface IDatabaseUpdateService { 
    Task UpdateDB_Projects(); 
    Task SendReposAsync(List<RepoModel> repos); 
    List<RepoModel> RepoModelFactory(List<Repository> repositories, Dictionary<string, IReadOnlyList<RepositoryLanguage>> langDict); 
}

public class DatabaseUpdateService : IDatabaseUpdateService
{
    private readonly SQLiteContext _context;
    private readonly IConfiguration _configuration;
    private readonly GitHubClient _githubClient;
    public string api_key { get; private set; }


    public DatabaseUpdateService(SQLiteContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
        api_key = Environment.GetEnvironmentVariable("GITHUB_API_KEY") ?? throw new InvalidOperationException("API Key not found in environment variables.");
        _githubClient = EstablishClient(api_key);
    }

    public GitHubClient EstablishClient(string api_key)
    {
        GitHubClient client = new GitHubClient(new ProductHeaderValue("PortfolioWebsite"));
        client.Credentials = new Credentials(api_key);

        return client;
    }

    public async Task UpdateDB_Projects()
    {
        try
        {
            //Pull All Repos
            IReadOnlyCollection<Repository> repos = await _githubClient.Repository.GetAllForUser("wgaechter");
            //Repos is filled now, parse and create in page

            List<Repository> raw_list = new List<Repository>(repos);
            var repo_list = raw_list.OrderByDescending(r => r.UpdatedAt).ToList();

            //Get the languages for each repo
            Dictionary<string, IReadOnlyList<RepositoryLanguage>> allLaguagesForRepos = new Dictionary<string, IReadOnlyList<RepositoryLanguage>>();
            foreach (Repository repo in repo_list)
            {
                var languages = await _githubClient.Repository.GetAllLanguages("wgaechter", repo.Name);

                allLaguagesForRepos.Add(repo.Name, languages);
            }

            List<RepoModel> repoModels = RepoModelFactory(repo_list, allLaguagesForRepos);
            await SendReposAsync(repoModels);

        }
        catch (Exception e) { System.Diagnostics.Debug.WriteLine(e.Message); };
    }

    public async Task SendReposAsync(List<RepoModel> repos)
    {
        foreach (var repo in repos)
        {
            var existingRepo = await _context.Projects.FindAsync(repo.ProjectId);

            if (existingRepo != null)
            {
                existingRepo.Name = repo.Name;
                existingRepo.Description = repo.Description;
                existingRepo.LanguageString = repo.LanguageString;
                existingRepo.HtmlUrl = repo.HtmlUrl;
                existingRepo.LastUpdated = repo.LastUpdated;

                _context.Projects.Update(existingRepo);
            }
            else
            {
                await _context.Projects.AddAsync(repo);
            }
        }

        await _context.SaveChangesAsync();
    }

    public List<RepoModel> RepoModelFactory(List<Repository> repositories, Dictionary<string, IReadOnlyList<RepositoryLanguage>> langDict)
    {
        List<RepoModel> RepoModels = new List<RepoModel>();

        foreach (var repo in repositories)
        {
            long id = repo.Id;
            string name = repo.Name;
            string desc = repo.Description;
            string htmlUrl = repo.HtmlUrl;
            long lastUpdated = repo.UpdatedAt.DateTime.Ticks;

            List<string> languageList = new List<string>();
            string languageString = "";

            if (langDict.ContainsKey(name))
            {
                foreach (var lang in langDict[name])
                {
                    languageList.Add(lang.Name);
                }

                languageString = string.Join(", ", languageList.ToArray());
            }

            RepoModel repoModel = new RepoModel(id, name, desc, languageString, htmlUrl, lastUpdated);

            RepoModels.Add(repoModel);
        }

        return RepoModels;
    }

    public async Task SendNewArticleAsync(ArticleModel newArticle)
    {
        var existingArticle = await _context.Articles.FindAsync(newArticle.Title);

        if (existingArticle != null)
        {
            existingArticle.Title = newArticle.Title;
            existingArticle.Subtitle = newArticle.Subtitle;
            existingArticle.Content = newArticle.Content;

            _context.Articles.Update(existingArticle);
        }
        else
        {
            await _context.Articles.AddAsync(newArticle);
        }

        await _context.SaveChangesAsync();
    }
}
