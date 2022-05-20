using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using KosovoTeam.Models;
using KosovoTeam.Data.Base;

namespace KosovoTeam.Data.Services
{
    public interface ITeamsService : IEntityBaseRepository<Team>
    {
    }
}
