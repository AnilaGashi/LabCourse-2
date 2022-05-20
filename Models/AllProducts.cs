using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KosovoTeam.Models
{
    public class AllProducts
    {
        public IEnumerable<Product> Products{ get; set; }
        public IEnumerable<Product> FiveProducts{ get; set; }
    }
}
