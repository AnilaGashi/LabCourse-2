using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using KosovoTeam.Models;
using KosovoTeam.Data.Base;

namespace KosovoTeam.Data.Services
{
    public class TeamsService: EntityBaseRepository<Team>, ITeamsService
    {
        public TeamsService(ApplicationDbContext context) : base(context) 
        { 
        }
    }
}
