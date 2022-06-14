using FootballPlayersDirectory.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballPlayersDirectory.Data
{
    public class FootballPlayersDbContext : DbContext
    {
        public FootballPlayersDbContext(DbContextOptions<FootballPlayersDbContext> options) : base(options)
        {
        }

        public DbSet<FootballPlayer> FootballPlayers { get; set; }
    }
}

