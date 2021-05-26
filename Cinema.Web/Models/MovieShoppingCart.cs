using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Web.Models
{
    public class MovieShoppingCart
    {
        public int MovieId { get; set; }
        public MovieModel Movie { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCart shoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}