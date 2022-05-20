using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KosovoTeam.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string ProductCategory { get; set; }

        public List<Product> Products { get; set; }
    }
}
