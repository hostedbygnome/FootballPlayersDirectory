using FootballPlayersDirectory.Models;
using FootballPlayersDirectory.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;

namespace FootballPlayersDirectory.Pages.CreateFootballPlayer
{
    public class CreateFootballPlayerModel : PageModel
    {
        private readonly FootballPlayersDbContext _contextFootballPlayers;

        private readonly TeamsDbContext _contextTeams;

        public CreateFootballPlayerModel(FootballPlayersDbContext contextFootballPlayers, TeamsDbContext contextTeams)
        {
            _contextFootballPlayers = contextFootballPlayers;
            _contextTeams = contextTeams;
        }

        public SelectList TeamsNames()
        {
            return new SelectList(_contextTeams.Teams.ToList().ConvertAll(i => i.Name));
        }

        [HttpPost]
        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            Team currentTeam = new Team();

            if (TeamType == "new")
                currentTeam.Name = NewTeamName;
            else
                currentTeam.Name = ExistingTeamName;

            var teamList = _contextTeams.Teams.Where(t => t.Name == currentTeam.Name).ToList();

            if (teamList.Count == 0)
                await _contextTeams.Teams.AddAsync(currentTeam);
            await _contextTeams.SaveChangesAsync();

            currentTeam = _contextTeams.Teams.Where(t => t.Name == currentTeam.Name).ToList()[0];

            FootballPlayer.TeamId = currentTeam.Id;

            await _contextFootballPlayers.FootballPlayers.AddAsync(FootballPlayer);
            await _contextFootballPlayers.SaveChangesAsync();

            return new RedirectToPageResult("/Index");
        }

        [BindProperty]
        public string TeamType { get; set; }

        [BindProperty]
        public FootballPlayer FootballPlayer { get; set; }

        [BindProperty]
        public IEnumerable<FootballPlayer> FootballPlayers { get; set; } = Enumerable.Empty<FootballPlayer>();

        [BindProperty]
        public string? NewTeamName { get; set; }

        [BindProperty]
        public string? ExistingTeamName { get; set; }
    }
}