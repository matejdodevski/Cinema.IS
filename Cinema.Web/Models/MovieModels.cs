using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Web.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GenreId { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public virtual GenreModel genre { get; set; }
        
    }

    public class GenreModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}