using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using KosovoTeam.Models;
using KosovoTeam.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace KosovoTeam.Models
{
    public class Team:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        //Relationships
        public List<Product> Products { get; set; }
    }
}