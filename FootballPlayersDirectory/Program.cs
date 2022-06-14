using FootballPlayersDirectory.Models;
using FootballPlayersDirectory.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

builder.Services.AddDbContext<FootballPlayersDbContext>(dbPlayers =>
{
    dbPlayers.UseSqlite("filename=Data/FootballPlayersDatabase/FootballPlayers.db");
});

builder.Services.AddDbContext<TeamsDbContext>(dbTeams =>
{
    dbTeams.UseSqlite("filename=Data/TeamsDatabase/Teams.db");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
