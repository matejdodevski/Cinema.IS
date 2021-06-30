using Cinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using Syncfusion.Pdf.Grid;

namespace Cinema.Web.Services
{
    public class MovieService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<MovieModel> GetAllMovies()
        {
            try
            {
                var moviesQuery = from movies in db.MovieModels
                                  select movies;
                var moviesList = moviesQuery.ToList();

                return moviesList;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<GenreModel> GetAllGenres()
        {
            try
            {
                var query = from genres in db.GenreModels
                                  select genres;
                var genreList = query.ToList();

                return genreList;
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public bool CreateShoppingCart(string ownerId)
        {
            try
            {
                ApplicationUser user = new ApplicationUser();
                user = GetUser(ownerId);

                ShoppingCart shoppingCart = new ShoppingCart { OwnerId = ownerId, Owner = user };
                db.ShoppingCarts.Add(shoppingCart);
                db.SaveChanges();
           
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public ShoppingCart GetShoppingCartIdByUserId(string id)
        {
            try
            {
                var shoppingCartQuery = from shoppingCart in db.ShoppingCarts
                                        where shoppingCart.OwnerId == id
                                        select shoppingCart;
                var result = shoppingCartQuery.FirstOrDefault();

                return result;
                                        
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public ApplicationUser GetUser(string id)
        {
            var userQuery = from user in db.Users
                            where user.Id == id
                            select user;
            var userResult = userQuery.FirstOrDefault();
            return userResult;
        }

        public bool IncrementTicket(int ticketId)
        {
            try
            {
                var query = from ticket in db.TicketModels
                            where ticket.Id == ticketId
                            select ticket;
                var res = query.FirstOrDefault();

                res.NumberOfTickets = res.NumberOfTickets + 1;

                db.SaveChanges();
                db.Dispose();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool DecrementTicket(int ticketId)
        {
            try
            {
                var query = from ticket in db.TicketModels
                            where ticket.Id == ticketId
                            select ticket;
                var res = query.FirstOrDefault();

                if(res.NumberOfTickets > 1)
                {
                    res.NumberOfTickets = res.NumberOfTickets - 1;
                }
                else
                {
                    db.TicketModels.Remove(res);
                }
                

                db.SaveChanges();
                db.Dispose();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool DeleteTicket(int ticketId)
        {
            try
            {
                var query = from ticket in db.TicketModels
                            where ticket.Id == ticketId
                            select ticket;
                var res = query.FirstOrDefault();

              
                    db.TicketModels.Remove(res);
              

                db.SaveChanges();
                db.Dispose();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public TicketModel CheckIfTicketExistsInShoppingCart(int movieId, int shoppingCartId)
        {
            var query = from ticket in db.TicketModels
                        where ticket.MovieId == movieId 
                        && ticket.ShoppingCartId == shoppingCartId
                        && ticket.Payed == false
                        select ticket;
            var ticketResult = query.FirstOrDefault();

            if(ticketResult == null)
            {
                return null;
            }
            return ticketResult;
        }

        public bool PayForTickets(int cartId)
        {

            try
            {
                var cart = db.ShoppingCarts.Find(cartId);

                var tickets = cart.TicketsInShoppingCart;

                var emailTo = db.Users.Find(cart.OwnerId).Email;
                
                foreach(var item in tickets)
                {
                    item.Payed = true;
                }
                db.SaveChanges();

                MailMessage mail = new MailMessage();
                mail.To.Add(emailTo);
                mail.From = new MailAddress("matej.ddd@gmail.com");
                mail.Subject = "Confirmation for your order on Cinema Store";
                string Body = "You have successfully payed for the order of your tickets! Thank you!";
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("matej.ddd@gmail.com", "Matejdode00+");
                smtp.EnableSsl = true;
                smtp.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public IEnumerable<TicketModel> GetAllPayedTicketsForUser(string userId)
        {
            var query = from ticket in db.TicketModels
                        join cart in db.ShoppingCarts
                        on ticket.ShoppingCartId equals cart.Id
                        where cart.OwnerId == userId && ticket.Payed == true
                        select ticket;

            var res = query.ToList();

            return res;

        }

    


        public ShoppingCart AddTicketToShoppingCart(int movieId,string userId)
        {
            try
            {
                TicketModel ticketModel = new TicketModel();
                var movie = db.MovieModels.Find(movieId);
                var shoppingCart = GetShoppingCartIdByUserId(userId);

                var checkTicket = CheckIfTicketExistsInShoppingCart(movieId, shoppingCart.Id);
                if (checkTicket != null)
                {
                    ticketModel = checkTicket;
                    ticketModel.NumberOfTickets = ticketModel.NumberOfTickets + 1;
                }
                else
                {
                    ticketModel = new TicketModel
                    {
                        MovieId = movie.Id,
                        DateOfReservation = DateTime.Now,
                        Price = movie.Price,
                        ShoppingCartId = shoppingCart.Id,
                        ShoppingCart = shoppingCart,
                        Movie = movie,
                        NumberOfTickets = 1,
                        Payed = false
                    };
                    db.TicketModels.Add(ticketModel);
                    db.ShoppingCarts.Find(shoppingCart.Id).TicketsInShoppingCart.Add(ticketModel);
                }
               
                db.SaveChanges();
                db.Dispose();

                return shoppingCart;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public string GetUserIdByEmail(string email)
        {
            var query = from user in db.Users
                        where user.Email == email
                        select user;
            var userResult = query.FirstOrDefault();

            return userResult.Id;
        }

        public int GetCartIdByUserId(string userId)
        {
            var query = from cart in db.ShoppingCarts
                        where cart.OwnerId == userId
                        select cart;
            return query.FirstOrDefault().Id;
                        
        }

        public bool AddUserRoleToUser(string userId, int roleId)
        {
            try
            {
                var userRole = new UserRoles
                {
                    UserId = userId,
                    RoleId = roleId
                };

                db.UserRoles.Add(userRole);
                db.SaveChanges();
    

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int GetRoleForUserId(string userId)
        {
            var query = from role in db.UserRoles
                        where role.UserId == userId
                        select role;
            return query.FirstOrDefault().RoleId;
        }

        public List<TicketModel> GetAllTicketsForGenreId(int genreId)
        {
            try
            {
                var query = from tickets in db.TicketModels
                            join movies in db.MovieModels
                            on tickets.MovieId equals movies.Id
                            where movies.GenreId == genreId
                            select tickets;
                return query.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}