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
    public class NewProductVM
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Poster URL")]
        [Required(ErrorMessage = "Poster URL is required")]
        public string ImageURL { get; set; }
        
        [Display(Name = "Trailer URL")]
        [Required(ErrorMessage = "Trailer URL is required")]
        public string TrailerURL { get; set; }


        [Display(Name = "Start date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Product Category is required")]
        public int CategoryId { get; set; }

        //Relationships
        [Display(Name = "Select player(s)")]
        [Required(ErrorMessage = "Player(s) is required")]
        public List<int> PlayerIds { get; set; }

        [Display(Name = "Select a team")]
        [Required(ErrorMessage = "Team is required")]
        public int TeamId { get; set; }

        [Display(Name = "Select a staff")]
        [Required(ErrorMessage = "Staff is required")]
        public int StaffId { get; set; }
    }
}
