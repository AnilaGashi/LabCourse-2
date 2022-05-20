using KosovoTeam.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KosovoTeam.Models
{
    public class Staff:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profile picture")]
        public string ProfilePictureURL { get; set; }

        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        public string Bio { get; set; }

        //Realationships
        public List<Product> Products { get; set; }
    }
}
