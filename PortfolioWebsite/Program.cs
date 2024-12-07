using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddDbContext<SQLiteContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IDatabaseUpdateService, DatabaseUpdateService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<SQLiteContext>();

    // Ensure database file is created if it doesn't exist
    var dbPath = configuration.GetConnectionString("DefaultConnection").Split('=')[1];
    if (!File.Exists(dbPath))
    {
        dbContext.Database.EnsureCreated();
    }

    var databaseUpdateService = services.GetRequiredService<IDatabaseUpdateService>();
    await databaseUpdateService.UpdateDB_Projects();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.Run();

