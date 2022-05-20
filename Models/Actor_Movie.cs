using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KosovoTeam.Models
{
    public class Actor_Movie
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
