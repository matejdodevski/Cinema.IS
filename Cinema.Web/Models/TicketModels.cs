﻿using System;
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
    }

  
    public class UserBalance
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public int Balance { get; set; }
    }

    
    
}