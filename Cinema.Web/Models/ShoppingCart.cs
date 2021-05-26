using Cinema.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Web.Models
{
    public class ShoppingCart
    {
        public string OwnerId { get; set; }
        public CinemaApplicationUser Owner { get; set; }
        public virtual ICollection<MovieShoppingCart> MoviesInShoppingCart { get; set; }
    }
}