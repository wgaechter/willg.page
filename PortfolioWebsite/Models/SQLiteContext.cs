using Microsoft.EntityFrameworkCore;

namespace PortfolioWebsite.Models
{
    public class SQLiteContext : DbContext
    {
        public SQLiteContext(DbContextOptions<SQLiteContext> options) : base(options) { }
        public DbSet<RepoModel> Projects { get; set; }
        public DbSet<ArticleModel> Articles { get; set; }
    }
}
