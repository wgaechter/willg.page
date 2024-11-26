using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Octokit;

namespace PortfolioWebsite.Models
{
    public class RepoModel
    {
        [Key]
        public long ProjectId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? LanguageString { get; set; }
        public string HtmlUrl { get; set; }
        public long LastUpdated { get; set; }

        public RepoModel(long projectId, string name, string description, string languageString, string htmlUrl, long lastUpdated)
        {
            ProjectId = projectId;
            Name = name;
            Description = description;
            LanguageString = languageString;
            HtmlUrl = htmlUrl;
            LastUpdated = lastUpdated;
        }

        internal RepoModel()
        {

        }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RepoModel>().HasKey(e => e.ProjectId); // Explicitly setting the primary key }
        }
    }
}
