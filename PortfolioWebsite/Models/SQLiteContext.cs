using Microsoft.EntityFrameworkCore;

namespace PortfolioWebsite.Models
{
    public class SQLiteContext : DbContext
    {
        public SQLiteContext(DbContextOptions<SQLiteContext> options) : base(options) { }
        public DbSet<RepoModel> Projects { get; set; }
        public DbSet<ArticleModel> Articles { get; set; }

        //  Table Create Statements
        /*
            CREATE TABLE "Articles" (
	            "ArticleId"	INTEGER UNIQUE,
	            "Title"	TEXT NOT NULL UNIQUE,
	            "Subtitle"	TEXT,
	            "Content"	TEXT,
	            "DatePosted"	INTEGER NOT NULL,
	            "ImgLoc"	INTEGER,
	            PRIMARY KEY("ArticleId" AUTOINCREMENT)
                )
         */
        /*
            CREATE TABLE "Projects" (
                ProjectID INTEGER PRIMARY KEY,
                Name TEXT,
                Description TEXT,
                LanguageString TEXT,
                HtmlUrl TEXT, 
                LastUpdated INTEGER
                )
        */
    }
}
