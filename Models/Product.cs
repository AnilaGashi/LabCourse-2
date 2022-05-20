using KosovoTeam.Data;
using KosovoTeam.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KosovoTeam.Models
{
    public class Product : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public string TrailerURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        //Relationships
        public List<Actor_Movie> Actors_Movies { get; set; }

       

        //Cinema
        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public Team Team { get; set; }

        //Producer
        public int StaffId { get; set; }
        [ForeignKey("StaffId")]
        public Staff Staff { get; set; }


       
    }

}
