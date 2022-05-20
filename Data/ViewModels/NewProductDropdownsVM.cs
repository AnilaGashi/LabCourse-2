using KosovoTeam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KosovoTeam.Data.ViewModels
{
    public class NewProductDropdownsVM
    {
        
        public NewProductDropdownsVM()
        {
            Staffs = new List<Staff>();
            Teams = new List<Team>();
            Players = new List<Player>();
            Categories = new List<Category>();
        }

        public List<Staff> Staffs { get; set; }
        public List<Team> Teams { get; set; }
        public List<Player> Players { get; set; }
        public List<Category> Categories { get; set; }
    }
}
