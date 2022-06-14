using FootballPlayersDirectory.Data;
using FootballPlayersDirectory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FootballPlayersDirectory.Pages
{
    public class IndexModel : PageModel
    {
        private readonly FootballPlayersDbContext _contextFootballPlayers;

        private readonly TeamsDbContext _contextTeams;

        public IndexModel(FootballPlayersDbContext contextFootballPlayers, TeamsDbContext contextTeams)
        {
            _contextFootballPlayers = contextFootballPlayers;
            _contextTeams = contextTeams;
        }

        public SelectList GetTeamsNames() => new SelectList(_contextTeams.Teams.ToList()
                                                                               .ConvertAll(i => i.Name));

        public string GetCurrentTeamName(int TeamId)
        {
            var currentTeam = _contextTeams.Teams.Where(t => t.Id == TeamId)
                                                 .FirstOrDefault();
            return currentTeam.Name;
        }

        [HttpDelete("{footballPlayerId}")]
        public async Task<IActionResult> OnPostDelete(int footballPlayerId)
        {

            var currentPlayer = _contextFootballPlayers.FootballPlayers.Where(fp => fp.Id == footballPlayerId)
                                                                       .FirstOrDefault();

            var currentTeamPlayers = _contextFootballPlayers.FootballPlayers.Where(fp => fp.TeamId == currentPlayer.TeamId)
                                                                            .ToList();

            if (currentTeamPlayers.Count == 1)
            {
                var teamToDelete = _contextTeams.Teams.Where(t => t.Id == currentPlayer.TeamId)
                                                      .FirstOrDefault();
                _contextTeams.Entry(teamToDelete).State = EntityState.Deleted;
                await _contextTeams.SaveChangesAsync();
            }

            _contextFootballPlayers.Entry(currentPlayer).State = EntityState.Deleted;
            await _contextFootballPlayers.SaveChangesAsync();
            return new RedirectToPageResult("/Index");
        }

        [HttpPost]
        public async Task<IActionResult> OnPost()
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

            FootballPlayer.Id = CurrentFootballPlayerId;

            _contextFootballPlayers.Update(FootballPlayer);

            await _contextFootballPlayers.SaveChangesAsync();

            return new RedirectToPageResult("/Index");
        }

        [HttpGet]
        public async void OnGet()
        {
            FootballPlayers = await _contextFootballPlayers.FootballPlayers.ToListAsync();
        }

        [BindProperty]
        public int CurrentFootballPlayerId { get; set; }

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

        [BindProperty]
        public IEnumerable<Team> Teams { get; set; } = Enumerable.Empty<Team>();
    }
}
