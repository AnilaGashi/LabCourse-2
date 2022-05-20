using KosovoTeam.Data.Base;
using KosovoTeam.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KosovoTeam.Data.Services
{
    public class StaffsService: EntityBaseRepository<Staff>, IStaffsService
    {
        public StaffsService(ApplicationDbContext context) : base(context)
        {     
        }
    }
}
