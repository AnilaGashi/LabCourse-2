using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using KosovoTeam.Models;
using KosovoTeam.Data.Base;

namespace KosovoTeam.Data.Services
{
    public class PlayersService : EntityBaseRepository<Player>, IPlayersService
    {
        public PlayersService(ApplicationDbContext context) : base(context) { }
    }
}
