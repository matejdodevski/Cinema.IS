using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Web.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int MovieId { get; set; }
        public DateTime DateOfReservation { get; set; }
        public int? ShoppingCartId { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual MovieModel Movie { get; set; }
        public int NumberOfTickets { get; set; }
        public bool Payed { get; set; }
    }

}